using UnityEngine;

public enum PlayerNumber { One, Two }

public class PlayerController : MonoBehaviour
{
    private enum ScreenEdge { Left, Right }
    
    [SerializeField] private PlayerNumber number;
    private ScreenEdge edge;
    private int prevScreenWidth;
    private int prevScreenHeight;
    void Start()
    {
        MoveToEdge();
    }

    void Update()
    {
        if (Screen.width != prevScreenWidth || Screen.height != prevScreenHeight) MoveToEdge();
        prevScreenWidth = Screen.width;
        prevScreenHeight = Screen.height;
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
        
        // Keep the object's z position unchanged
        targetPosition.z = transform.position.z;

        // Move the object
        transform.position = targetPosition;
    }

    public void SetPlayerNumber(PlayerNumber num)
    {
        number = num;
        MoveToEdge();
    }
}
