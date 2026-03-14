using UnityEngine;


public class player_movement : MonoBehaviour
{
    private Rigidbody2D body;
    private SpriteRenderer sr;
    public int playerid = 1;
    public float speed = 10f;
    public float jumpforce = 14f;

    public LayerMask groundLayer;
    public LayerMask pillerlayer;
    public LayerMask playerlayer;
    public Transform groundCheck;
    private Animator anim;//to get the 2D animator 

    private bool isground;
    public bool istouchingpiller;
    public bool isstandingonplayer;

    private float jumpBuffer = 0f;
    public bool isDead = false; // for function : if oneplayer is dead he will lose all control and ither will play
    public bool isStunned = false;//function : for getting hit by the eagle
    public AudioSource jumpSound; //for players jumping sound


    void Awake()
    {
        body = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        body.freezeRotation = true;
    }

    void Update()
    {

        if (isDead) return; // If dead, stop everything in this script!
                            // Movement - ONLY apply this if we are NOT stunned!


        // If stunned, we exit the logic that processes inputs and forces, 
        // but we let the Sprite Flip continue so they don't look weirdly frozen.
        // 2. Handle Stunned state
        if (isStunned)
        {
            // Still allow sprite flipping while stunned
            if (body.linearVelocity.x > 0.1f) sr.flipX = false;
            else if (body.linearVelocity.x < -0.1f) sr.flipX = true;

            // IMPORTANT: We return here so the rest of the code (input/movement) doesn't run!
            return;
        }
        float moveplayer = 0f;
        //Input

        if (playerid == 1)
        {
            if (Input.GetKey(KeyCode.A)) moveplayer = -1f;
            if (Input.GetKey(KeyCode.D)) moveplayer = 1f;
            if (Input.GetKeyDown(KeyCode.W)) jumpBuffer = 0.2f; // Remember jump for 0.2s
        }
        else
        {
            if (Input.GetKey(KeyCode.LeftArrow)) moveplayer = -1f;
            if (Input.GetKey(KeyCode.RightArrow)) moveplayer = 1f;
            if (Input.GetKeyDown(KeyCode.Space)) jumpBuffer = 0.2f; // Remember jump for 0.2s
        }

        // 2. Reduce the timer
        jumpBuffer -= Time.deltaTime;

        // 3. Jump logic using the buffer
        if ((isground || istouchingpiller || isstandingonplayer) && jumpBuffer > 0 && body.linearVelocity.y <= 0.1f)
        {
            body.linearVelocity = new Vector2(body.linearVelocity.x, jumpforce);
            jumpBuffer = 0; // Clear the buffer

            // 2. Play the sound exactly when the jump happens
            if (jumpSound != null) 
            {
                jumpSound.Stop(); // Optional: Stop previous sound to avoid overlap
                jumpSound.Play();
            }
        }


        // Detection
        isground = Physics2D.OverlapCircle(groundCheck.position, 0.3f, groundLayer);
        istouchingpiller = Physics2D.OverlapCircle(groundCheck.position, 0.3f, pillerlayer);


        // Only look for Player layer when specifically checking if standing on another player
        isstandingonplayer = Physics2D.OverlapCircle(groundCheck.position, 0.1f, playerlayer);

        // Movement
        body.linearVelocity = new Vector2(moveplayer * speed, body.linearVelocity.y);



        // Sprite Flip
        if (moveplayer > 0) sr.flipX = false;
        else if (moveplayer < 0) sr.flipX = true;

        // Updated Animator Logic for Multiple Controllers
        if (anim != null)
        {
            if (playerid == 1)
            {
                // Player 1 uses your original parameter names
                anim.SetBool("Player_1_Ground", (isground || istouchingpiller || isstandingonplayer));
                anim.SetBool("Player_1_Run", Mathf.Abs(moveplayer) > 0.1f);
            }
            else
            {

                // Ensure these match the names in your Animator Parameters tab exactly!
                anim.SetBool("Player_2_Jump", (isground || istouchingpiller || isstandingonplayer));
                anim.SetBool("Player_2_Walk", Mathf.Abs(moveplayer) > 0.1f);


            }
        }



    }


}