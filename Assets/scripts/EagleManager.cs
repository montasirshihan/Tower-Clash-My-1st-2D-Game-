using UnityEngine;
using System.Collections;
public class EagleManager : MonoBehaviour
{
    public GameObject eagleprefab;
    public AudioSource warningsound;
    private bool spawnFromRight = true; // Toggle direction


    void Start()
    {

        StartCoroutine(SpawnLoop());

    }

    IEnumerator SpawnLoop()
    {
        while (true) // This makes it loop forever
        {
            // 1. Pick a random wait time between 15 and 25 seconds
            float randomWait = Random.Range(15f, 25f);
            yield return new WaitForSeconds(randomWait);

            // 2. Play warning sound
            if (warningsound != null) warningsound.Play();
            yield return new WaitForSeconds(2.5f);

            // Calculate spawn position and direction based on the toggle
            float camY = Camera.main.transform.position.y;
            float camX = Camera.main.transform.position.x;

            Vector3 spawnPos;
            Vector2 direction;

            if (spawnFromRight)
            {
                // Top-Right to Bottom-Left
                spawnPos = new Vector3(camX + 15f, camY + 15f, 0f);//can fix the Y position of the eagle by changing camY + higher value
                direction = new Vector2(-1f, -0.5f).normalized;
            }
            else
            {
                // Top-Left to Bottom-Right
                spawnPos = new Vector3(camX - 15f, camY + 15f, 0f);//can fix the Y position of the eagle by changing camY + higher value
                direction = new Vector2(1f, -0.5f).normalized;
            }

            // Spawn the eagle
            GameObject eagle = Instantiate(eagleprefab, spawnPos, Quaternion.identity);

            // Pass the direction to the eagle's script
            EagleHazard hazard = eagle.GetComponent<EagleHazard>();
            if (hazard != null)
            {
                hazard.SetDirection(direction);
            }

            // Flip the toggle for the next time
            spawnFromRight = !spawnFromRight;

        }
    }


}
//Vector3 spawnPos = new Vector3(camX + 15f, camY + 12f, 0f);

// Instantiate(eagleprefab, spawnPos, eagleprefab.transform.rotation);