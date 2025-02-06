using UnityEngine;

public class BallController : MonoBehaviour
{
    public bool moving = false;
    private Vector2 direction;
    private float speed = 5f;
    private float yScale;
    private float xScale;
    private GameController gameController;

    void Start()
    {
        yScale = transform.localScale.y;
        xScale = transform.localScale.x;
        gameController = FindObjectOfType<GameController>();
    }

    void Update()
    {
        if (moving)
        {
            transform.position += (Vector3)direction * speed * Time.deltaTime;
            CheckScreenBounds();
        }
    }

    public void SetDirection(Vector2 newDirection)
    {
        float angle = Random.Range(-20f, 20f);
        direction = Quaternion.Euler(0, 0, angle) * newDirection;
        direction = direction.normalized;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            PlayerController player = collision.gameObject.GetComponent<PlayerController>();
            if (player != null)
            {
                float hitPosition = (transform.position.y - player.transform.position.y) / (player.transform.localScale.y / 2);
                float angle = hitPosition * 45f;
                float speedMultiplier = player.IsMoving ? 1.2f : 1.1f;
                
                direction = Quaternion.Euler(0, 0, angle) * new Vector2(-direction.x, direction.y);
                direction = direction.normalized;
                speed *= speedMultiplier;
            }
        }
    }

    void CheckScreenBounds()
    {
        Camera cam = Camera.main;
        if (cam == null) return;

        float screenLeft = cam.ViewportToWorldPoint(new Vector3(0, 0, 0)).x;
        float screenRight = cam.ViewportToWorldPoint(new Vector3(1, 0, 0)).x;
        float screenTop = cam.ViewportToWorldPoint(new Vector3(0, 1, 0)).y;
        float screenBottom = cam.ViewportToWorldPoint(new Vector3(0, 0, 0)).y;

        if (transform.position.x + xScale / 2 < screenLeft || transform.position.x - xScale / 2 > screenRight)
        {
            ResetBall();
        }

        if (transform.position.y + yScale / 2 >= screenTop || transform.position.y - yScale / 2 <= screenBottom)
        {
            direction.y = -direction.y;
        }
    }

    void ResetBall()
    {
        moving = false;
        transform.position = Vector3.zero;
        speed = 5f;
        gameController.UpdateScore();
    }
}