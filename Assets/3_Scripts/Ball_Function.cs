using TMPro;
using System;
using UnityEngine;
using System.Collections;

public class Ball_Function : MonoBehaviour
{
    Rigidbody player;
    [SerializeField] Animator racket_Animation;

    public static Action BallMissed;

    [Header ("Ball information")]
    [SerializeField] Vector3 ball_Position;
    [SerializeField] float player_Speed = 10f;
    [SerializeField] float player_Side_Speed;
    [SerializeField] float player_Up_Speed;
    [SerializeField] bool isForward = true;

    [Header ("Tags")]
    [SerializeField] string racket_Tag;
    [SerializeField] string brick_Tag;
    [SerializeField] string backWallTag;

    [Header ("Swipe inputs")]
    // swipe controlls
    [SerializeField] bool swipeUp = false;
    [SerializeField] bool swipeDown = false;
    [SerializeField] bool swipeLeft = false;
    [SerializeField] bool swipeRight = false;
    [Space]
    float swipeValue;
    [Space]
    [SerializeField] TMP_Text direction_Text;


    private void Awake()
    {
        player = GetComponent<Rigidbody>();
        direction_Text = GameObject.Find("Directioh_Text").GetComponent<TMP_Text>();
        racket_Animation = GameObject.Find("Racket_Model").GetComponent<Animator>();
    }

    private void OnEnable()
    {
        Main_Menu_UI_Manager.Reset += Reset;
        Racket_Animation.RacketGotHit += RacketAnimation;
    }

    private void OnDisable()
    {
        Main_Menu_UI_Manager.Reset -= Reset;
        Racket_Animation.RacketGotHit -= RacketAnimation;
    }

    private void Reset()
    {
        Destroy(gameObject);
    }

    private void Start()
    {
        player_Side_Speed = player.velocity.x;
        player_Side_Speed = player.velocity.y;
        Invoke(nameof(StartBallMovement), 2);
    } void StartBallMovement() => StartCoroutine(nameof(BallMovement));

    // logic for ball movement in the box according to applied force 
    IEnumerator BallMovement ()
    {
        while(true)
        {
            if (isForward)
            {
                // moving ball towards the bircks
                ball_Position.x = player_Side_Speed;
                ball_Position.y = player_Up_Speed;
                ball_Position.z = player_Speed;

                player.velocity = ball_Position;
            }

            else if (!isForward)
            {
                // moving ball towards racket
                ball_Position.x = player_Side_Speed;
                ball_Position.y = player_Up_Speed;
                ball_Position.z = -player_Speed;

                player.velocity = ball_Position;
            }
            yield return null;
        }
    }

    // add force on the ball in left direction 
    IEnumerator LeftDirection()
    {
        player_Side_Speed = -0.1f;

        yield return new WaitForSeconds(0.3f);
        player_Side_Speed = player.velocity.x;
    }

    // add force on the ball in right direction 
    IEnumerator RightDirection()
    {
        player_Side_Speed = 0.1f;

        yield return new WaitForSeconds(0.1f);
        player_Side_Speed = player.velocity.x;
    }

    // add upwards force on the ball
    IEnumerator UpDirection()
    {
        player_Up_Speed = 0.08f;

        yield return new WaitForSeconds(0.1f);
        player_Up_Speed = player.velocity.y;
    }

    // add dowawards force on the ball
    IEnumerator DownDirection()
    {
        player_Up_Speed = -0.08f;

        yield return new WaitForSeconds(0.3f);
        player_Up_Speed = player.velocity.y;
    }

    private void OnCollisionEnter(Collision info)
    {
        // if the ball hit the racket 
        if (info.collider.CompareTag (racket_Tag))
        {
            // make them move towards bricks 
            isForward = true;   
            GiveDirection();            // change their direction according to the inputs player gave 

        }

        // if the ball hits any brick
        else if (info.collider.CompareTag (brick_Tag))
        {
            isForward = false;
        }

        // if the player is enable to shoot the ball and hit the back wall
        else if (info.collider.CompareTag (backWallTag))
        {
            isForward = true;
            BallMissed?.Invoke();
        }
    }

    // give direction to the ball according to the inputs player gave 
    void GiveDirection()
    {
        if (swipeLeft)
            StartCoroutine(nameof(LeftDirection));
        else if (swipeRight)
            StartCoroutine(nameof(RightDirection));
        else if (swipeUp)
            StartCoroutine(nameof(UpDirection));
        else if (swipeDown)
            StartCoroutine(nameof(DownDirection));
        else return;
    }

    // get the input from the controller via swiping on the joystick
    void GetInputs ()
    {
        bool isSwipingRight;
        bool isSwipingLeft;
        bool isSwipingUp;
        bool isSwipingDown;

        isSwipingRight = JMRInteraction.GetSwipeRight(out swipeValue);
        if (isSwipingRight)
        {
            direction_Text.text = ($"Direction: right");
            NoDirection();
            swipeRight = true;
        }

        isSwipingLeft = JMRInteraction.GetSwipeLeft(out swipeValue);
        if (isSwipingLeft)
        {
            direction_Text.text = ($"Direction: Left");
            NoDirection();
            swipeLeft = true;
        }

        isSwipingUp = JMRInteraction.GetSwipeUp(out swipeValue);
        if (isSwipingUp)
        {
            direction_Text.text = ($"Direction: Up");
            NoDirection();
            swipeUp = true;
        }

        isSwipingDown = JMRInteraction.GetSwipeDown(out swipeValue);
        if (isSwipingDown)
        {
            direction_Text.text = ($"Direction: Down");
            NoDirection();
            swipeDown = true;
        }
    }
    private void Update()
    {
        GetInputs();
    }

    void NoDirection()
    {
        swipeRight = false;
        swipeLeft = false;
        swipeUp = false;
        swipeDown = false;
    }

    void RacketAnimation ()
    {
        if (swipeUp)
            racket_Animation.SetTrigger("isUp");
        else if (swipeDown)
            racket_Animation.SetTrigger("isDown");
        else if (swipeLeft)
            racket_Animation.SetTrigger("isLeft");
    }
}
