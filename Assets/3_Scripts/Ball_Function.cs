using System;
using JMRSDK;
using UnityEngine;
using JMRSDK.InputModule;
using System.Collections;

public class Ball_Function : MonoBehaviour
{
    Rigidbody player;

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

    [Header("Direction inputs")]
    [SerializeField] bool isLeft = false;
    [SerializeField] bool isRight = false;
    [SerializeField] bool isUp = false;
    [SerializeField] bool isDown = false;


    private void Awake()
    {
        player = GetComponent<Rigidbody>();
    }

    private void OnEnable()
    {
        Main_Menu_UI_Manager.Reset += Reset;
    }

    private void OnDisable()
    {
        Main_Menu_UI_Manager.Reset -= Reset;
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

    IEnumerator BallMovement ()
    {
        while(true)
        {
            if (isForward)
            {
                ball_Position.x = player_Side_Speed;
                ball_Position.y = player_Up_Speed;
                ball_Position.z = player_Speed;

                player.velocity = ball_Position;
            }

            else if (!isForward)
            {
                ball_Position.x = player_Side_Speed;
                ball_Position.y = player_Up_Speed;
                ball_Position.z = -player_Speed;

                player.velocity = ball_Position;
            }
            yield return null;
        }
    }

    IEnumerator XMovement()
    {
        int X = UnityEngine.Random.Range(-1, 2);

        player_Side_Speed = 0.3f * X;

        yield return new WaitForSeconds(0.3f);
        player_Side_Speed = player.velocity.x;
    }

    IEnumerator YMovement()
    {
        int X = UnityEngine.Random.Range(-1, 2);

        player_Up_Speed = 0.2f * X;

        yield return new WaitForSeconds(0.3f);
        player_Up_Speed = player.velocity.y;
    }

    private void OnCollisionEnter(Collision info)
    {
        if (info.collider.CompareTag (racket_Tag))
        {
            isForward = true;
            ApplyRandomForce();

        }

        else if (info.collider.CompareTag (brick_Tag))
        {
            isForward = false;
            ApplyRandomForce();
        }
    
        else if (info.collider.CompareTag (backWallTag))
        {
            isForward = true;
            ApplyRandomForce();
            BallMissed?.Invoke();
        }
    }

    void ApplyRandomForce()
    {
        StartCoroutine(nameof(XMovement));
        StartCoroutine(nameof(YMovement));
    }


    void SetDirection(bool desireDirection)
    {
        isLeft = false;
        isRight = false;
        isUp = false;
        isDown = false;

        desireDirection = true;
    }


    public void Update()
    {
        isRight = JMRInteraction.GetSwipeRight(out float val);
    }
}
