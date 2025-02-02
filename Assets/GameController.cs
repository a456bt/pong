using UnityEngine;
public class GameController : MonoBehaviour
{
    public GameObject playerPrefab; // Assign this in the Inspector
    public GameObject ballPrefab;
    
    void Start()
    {
        SpawnPlayers();
        SpawnBall();
    }

    void SpawnPlayers()
    {
        if (playerPrefab == null)
        {
            Debug.LogError("Player prefab is not assigned!");
            return;
        }

        GameObject playerOne = Instantiate(playerPrefab, Vector3.zero, Quaternion.identity);
        GameObject playerTwo = Instantiate(playerPrefab, Vector3.zero, Quaternion.identity);

        PlayerController playerOneScript = playerOne.GetComponent<PlayerController>();
        PlayerController playerTwoScript = playerTwo.GetComponent<PlayerController>();

        if (playerOneScript != null && playerTwoScript != null)
        {
            playerOneScript.number = PlayerNumber.One;
            playerTwoScript.number = PlayerNumber.Two;
            playerOneScript.MoveToEdge();
            playerTwoScript.MoveToEdge();
        }
        else
        {
            Debug.LogError("Player script not found on prefab!");
        }
    }
    void SpawnBall()
    {
        if (ballPrefab == null)
        {
            Debug.LogError("Ball prefab is not assigned!");
            return;
        }

        GameObject ball = Instantiate(ballPrefab, Vector3.zero, Quaternion.identity);

        BallController ballScript = ball.GetComponent<BallController>();

        if (ballScript == null) Debug.LogError("Ball script not found on prefab!");

        ballScript.moving = true;
    }
}
