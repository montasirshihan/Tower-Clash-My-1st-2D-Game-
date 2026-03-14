using UnityEngine;

public class GameOverTrigger : MonoBehaviour
{
    public AudioSource audioSource; //for thr faahh sound
    public AudioSource bgmSource; //for the bgm sound
    public GameObject p1_text;
    public GameObject p2_text;
    public GameObject tie_text; 


//for continuing the game if one is dead and other is alive
    public static bool greenFinished = false;
    public static bool purpleFinished = false;
    public static int greenScoreAtDeath = 0;
    public static int purpleScoreAtDeath = 0;

    void Start()
    {
        greenFinished = false;
        purpleFinished = false;
        if (p1_text != null) p1_text.SetActive(false);
        if (p2_text != null) p2_text.SetActive(false);
        if (tie_text != null) tie_text.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (Time.timeScale == 0) return;

        if (collision.gameObject.name == "player1(green)" && !greenFinished)
        {
            PlayEndSound();
            greenFinished = true;
            greenScoreAtDeath = ScoreManager.instance.scoreP1;

            // DISABLE P1 MOVEMENT
        collision.GetComponent<player_movement>().isDead = true;
            CheckWinCondition(); 
        }
        else if (collision.gameObject.name == "player2(purple)" && !purpleFinished)
        {
            PlayEndSound();
            purpleFinished = true;
            purpleScoreAtDeath = ScoreManager.instance.scoreP2;

            // DISABLE P1 MOVEMENT
        collision.GetComponent<player_movement>().isDead = true;
            CheckWinCondition();
        }
    }

    void PlayEndSound()
    {
        if (audioSource != null && audioSource.clip != null)
            audioSource.PlayOneShot(audioSource.clip);
    }

    void CheckWinCondition()
    {
        int currentGreen = ScoreManager.instance.scoreP1;
        int currentPurple = ScoreManager.instance.scoreP2;

        // SCENARIO: Green finished, Purple is still playing
        if (greenFinished && !purpleFinished)
        {
            // 1. If Purple takes the lead, Purple wins immediately!
        if (currentPurple > greenScoreAtDeath) 
        {
           //////////////// EndGame(p2_text); //so that he can play if he dare to reach the final piller
        }

        }

        // SCENARIO: Purple finished, Green is still playing
        if (purpleFinished && !greenFinished)
        {
            // 1. If Green takes the lead, Green wins immediately!
        if (currentGreen > purpleScoreAtDeath) 
        {
            //////////////////////////EndGame(p1_text);//so that he can play if he dare to reach the final piller
        }

        }

        // SCENARIO: Both finished
        if (greenFinished && purpleFinished)
        {
            if (greenScoreAtDeath > purpleScoreAtDeath) EndGame(p1_text);
            else if (purpleScoreAtDeath > greenScoreAtDeath) EndGame(p2_text);
            else EndGame(tie_text);
        }
    }

    void EndGame(GameObject winnerText)
    {
        Time.timeScale = 0;
        if (bgmSource != null) bgmSource.Stop();
        if (winnerText != null) winnerText.SetActive(true);
    }

    void Update()
{
    // Only check if someone is dead and the game is still running
    if ((greenFinished || purpleFinished) && Time.timeScale != 0)
    {
        CheckWinCondition();
    }
}
}