using UnityEngine;
public class GameController : MonoBehaviour
{
    public GameObject playerPrefab; // Assign this in the Inspector
    
    void Start()
    {
        SpawnPlayers();
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
            playerOneScript.SetPlayerNumber(PlayerNumber.One);
            playerTwoScript.SetPlayerNumber(PlayerNumber.Two);
        }
        else
        {
            Debug.LogError("Player script not found on prefab!");
        }
    }
}
