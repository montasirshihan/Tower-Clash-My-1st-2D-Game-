using UnityEngine;

public class Collector_Coins : MonoBehaviour
{
    public int pointvalue = 1;

    public void OnTriggerEnter2D(Collider2D other)
    {
        // 1. Check if the object that touched the coin has the "Player" tag
        // 2. Also get the movement script to check the player ID

        Debug.Log("TRIGGER FIRED! Hit by: " + other.name);

        player_movement player = other.GetComponent<player_movement>();

        if (player != null && other.CompareTag("Player"))
        {
            // Tell the ScoreManager to add points
            ScoreManager.instance.AddScore(player.playerid, pointvalue);

            // Destroy the coin so it can't be collected twice
            Destroy(gameObject);
        }
    }
}
