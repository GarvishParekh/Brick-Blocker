using System;
using UnityEngine;

public class Brick_Function : MonoBehaviour
{
    public static Action Score;
    public static Action AddBrick;

    Animator brick_Animation;
    [SerializeField] string player_Ball;

    private void Awake() =>
        brick_Animation = GetComponent<Animator>();

    private void Start()
    {
        brick_Animation.enabled = false;
    }

    private void OnEnable()
    {
        AddBrick?.Invoke();
    }

    private void OnTriggerEnter(Collider info)
    {
        if (info.CompareTag ("AnimationTrigger"))
        {
            brick_Animation.enabled = true;
        }

        else if (info.CompareTag (player_Ball))
        {
            Score?.Invoke();
            Destroy(gameObject);
        }
    }
}
