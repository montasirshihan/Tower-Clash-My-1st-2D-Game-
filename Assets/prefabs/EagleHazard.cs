using UnityEngine;
using System.Collections;

public class EagleHazard : MonoBehaviour
{
    public float speed = 10f;
    private bool hasHit = false;

    private Vector2 moveDirection = new Vector2(-1f, -0.5f); // Default
    // Called by the Manager when spawning
    public void SetDirection(Vector2 dir)
    {
        moveDirection = dir;
    }

    void Start()
    {



        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = 0;
        rb.linearVelocity = moveDirection * speed;

        // Get the SpriteRenderer to handle the flipping
        SpriteRenderer sr = GetComponent<SpriteRenderer>();

        // 1. Calculate the base angle for the flight path
        float angle = Mathf.Atan2(moveDirection.y, moveDirection.x) * Mathf.Rad2Deg;

        // 2. Set the rotation for the path
        transform.rotation = Quaternion.Euler(0, 0, angle);

        // 3. Flip the sprite based on direction to fix the head/tail orientation
        if (moveDirection.x < 0)
        {
            // Moving Left: Flip the sprite so the head points Left
            sr.flipY = true;
        }
        else
        {
            // Moving Right: Normal orientation
            sr.flipY = false;
        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && !hasHit)
        {
            var player = collision.GetComponent<player_movement>();
            Rigidbody2D playerRb = collision.GetComponent<Rigidbody2D>();

            if (player != null && playerRb != null)
            {
                hasHit = true;
                StartCoroutine(DragAndRelease(player, playerRb));
            }
        }
    }

    IEnumerator DragAndRelease(player_movement player, Rigidbody2D playerRb)
    {
        player.isStunned = true; // Lock player input

        float timer = 0f;
        while (timer < 1f)
        {
            // The Eagle "carries" the player
            playerRb.linearVelocity = new Vector2(-speed, -speed * 0.5f);
            timer += Time.deltaTime;
            yield return null;
        }

        // --- RELEASE PHASE ---
        player.isStunned = false; // Give control back

        // Optional: Give a small "pop" velocity so they don't just drop dead
        playerRb.linearVelocity = new Vector2(-2f, 2f);

        // After this, the eagle continues its path and ignores the player
    }
}