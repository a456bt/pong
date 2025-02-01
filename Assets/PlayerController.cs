using UnityEngine;

public enum PlayerNumber { One, Two }

public class PlayerController : MonoBehaviour
{
    private enum ScreenEdge { Left, Right }
    
    [SerializeField] private PlayerNumber number;
    private ScreenEdge edge;
    private int prevScreenWidth;
    private int prevScreenHeight;
    private float xScale;
    private float yScale;
    private float speed = 5f;

    void Start()
    {
        MoveToEdge();
        xScale = transform.localScale.x;
        yScale = transform.localScale.y;
    }

    void Update()
    {
        if (Screen.width != prevScreenWidth || Screen.height != prevScreenHeight) MoveToEdge();
        prevScreenWidth = Screen.width;
        prevScreenHeight = Screen.height;

        HandleMovement();
    }

    void MoveToEdge()
    {
        if (Camera.main == null)
        {
            Debug.LogError("Main camera not found!");
            return;
        }

        if (number == PlayerNumber.One) edge = ScreenEdge.Left;
        else if (number == PlayerNumber.Two) edge = ScreenEdge.Right;
        else
        {
            Debug.LogError("edge not found");
            return;
        }

        Vector3 screenPoint = Vector3.zero;

        // Get the position in Viewport space
        switch (edge)
        {
            case ScreenEdge.Left:
                screenPoint = new Vector3(0, 0.5f, Camera.main.nearClipPlane);
                break;
            case ScreenEdge.Right:
                screenPoint = new Vector3(1, 0.5f, Camera.main.nearClipPlane);
                break;
        }

        // Convert to world position
        Vector3 targetPosition = Camera.main.ViewportToWorldPoint(screenPoint);
        
        // Offset to body size, which is already in world units
        if (edge is ScreenEdge.Left) targetPosition.x += xScale;
        else targetPosition.x -= xScale;
        
        // Keep the object's z position unchanged
        targetPosition.z = transform.position.z;

        // Move the object
        transform.position = targetPosition;
    }

    void HandleMovement()
    {
        float moveDirection = 0;
        if (number == PlayerNumber.One)
        {
            if (Input.GetKey(KeyCode.W)) moveDirection = 1;
            else if (Input.GetKey(KeyCode.S)) moveDirection = -1;
        }
        else if (number == PlayerNumber.Two)
        {
            if (Input.GetKey(KeyCode.UpArrow)) moveDirection = 1;
            else if (Input.GetKey(KeyCode.DownArrow)) moveDirection = -1;
        }

        Vector3 newPosition = transform.position + new Vector3(0, moveDirection * speed * Time.deltaTime, 0);

        // Clamp movement within screen boundaries
        float halfBodyScale = yScale / 2f;
        float minY = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, Camera.main.nearClipPlane)).y + halfBodyScale;
        float maxY = Camera.main.ViewportToWorldPoint(new Vector3(0, 1, Camera.main.nearClipPlane)).y - halfBodyScale;

        newPosition.y = Mathf.Clamp(newPosition.y, minY, maxY);
        transform.position = newPosition;
    }

    public void SetPlayerNumber(PlayerNumber num)
    {
        number = num;
        MoveToEdge();
    }
}
