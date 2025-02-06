using UnityEngine;

public class GameController : MonoBehaviour
{
    public GameObject playerPrefab;
    public GameObject ballPrefab;
    public GameObject serveBanner;
    
    private BallController ballScript;
    private bool gameStarted = false;
    private int playerOneScore = 0;
    private int playerTwoScore = 0;

    void Start()
    {
        SpawnPlayers();
        SpawnBall();
    }

    void Update()
    {
        if (!gameStarted)
        {
            if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                StartGame(Vector2.left);
            }
            else if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                StartGame(Vector2.right);
            }
        }
    }

    void SpawnPlayers()
    {
        GameObject playerOne = Instantiate(playerPrefab, Vector3.zero, Quaternion.identity);
        GameObject playerTwo = Instantiate(playerPrefab, Vector3.zero, Quaternion.identity);

        PlayerController playerOneScript = playerOne.GetComponent<PlayerController>();
        PlayerController playerTwoScript = playerTwo.GetComponent<PlayerController>();

        playerOneScript.number = PlayerNumber.One;
        playerTwoScript.number = PlayerNumber.Two;
        playerOneScript.MoveToEdge();
        playerTwoScript.MoveToEdge();
    }

    void SpawnBall()
    {
        GameObject ball = Instantiate(ballPrefab, Vector3.zero, Quaternion.identity);
        ballScript = ball.GetComponent<BallController>();
    }

    void StartGame(Vector2 direction)
    {
        if (ballScript != null)
        {
            ballScript.SetDirection(direction);
            ballScript.moving = true;
            gameStarted = true;
            if (serveBanner != null)
            {
                serveBanner.SetActive(false);
            }
        }
    }

    public void UpdateScore()
    {
        if (ballScript.transform.position.x < 0)
        {
            playerTwoScore++;
        }
        else
        {
            playerOneScore++;
        }
        Debug.Log("Score: Player 1 - " + playerOneScore + " | Player 2 - " + playerTwoScore);
    }
}
