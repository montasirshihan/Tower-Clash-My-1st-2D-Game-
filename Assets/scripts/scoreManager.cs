using UnityEngine;
using TMPro; // Use this if you are using TextMeshPro
  public class ScoreManager : MonoBehaviour
    {
        public static ScoreManager instance;
        public TextMeshProUGUI ScoreTextP1;
        public TextMeshProUGUI ScoreTextP2;

        public int scoreP1=0;
        public int scoreP2=0;

        void Awake()
        {
            instance=this;         
        }

        public void AddScore(int PlayerId, int Point)
        {
            if (PlayerId == 1)
            {
                scoreP1+=Point;
                ScoreTextP1.text="Green Player :" + scoreP1;
                Debug.Log("ScoreManager P1 is now: " + scoreP1); // ADD THIS
            }
            else
            {
                scoreP2+=Point;
                ScoreTextP2.text="Purple Player :" + scoreP2;
                Debug.Log("ScoreManager P2 is now: " + scoreP2); // ADD THIS
                
            }
        }

    }
