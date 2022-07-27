using System;
using UnityEngine;

public class Racket_Animation : MonoBehaviour
{
    public static Action RacketGotHit;

    private void OnTriggerEnter(Collider info)
    {
        if (info.CompareTag("PlayerBall"))
        {
            //RacketGotHit?.Invoke();
            Debug.Log("Racket Hit");
        }
    }
}
