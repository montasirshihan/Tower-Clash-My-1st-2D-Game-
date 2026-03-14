using UnityEngine;

public class GoalTrigger : MonoBehaviour
{
    public AudioSource audioSource;
    public GameObject p1_win_text;
    public GameObject p2_win_text;
    public GameObject tie_text;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (audioSource != null)
                audioSource.PlayOneShot(audioSource.clip);

            // STOP THE GAME
            Time.timeScale = 0; 
            
            // COMPARE SCORES
            CompareScores();
        }
    }

    void CompareScores()
    {
        int p1 = ScoreManager.instance.scoreP1;
        int p2 = ScoreManager.instance.scoreP2;

        if (p1 > p2)
        {
            p1_win_text.SetActive(true);
        }
        else if (p2 > p1)
        {
            p2_win_text.SetActive(true);
        }
        else
        {
            tie_text.SetActive(true);
        }
    }
}