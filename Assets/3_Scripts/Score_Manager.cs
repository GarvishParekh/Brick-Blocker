using TMPro;
using System;
using UnityEngine;

public class Score_Manager : MonoBehaviour
{
    public static Action PlayerWon;

    [SerializeField] TMP_Text scoreText;
    [SerializeField] int scoreCount;

    private void OnEnable()
    {
        Brick_Function.Score += score;
        Brick_Function.AddBrick += AddBrick;
        Main_Menu_UI_Manager.Reset += ResetScore;
    }

    private void OnDisable()
    {
        Brick_Function.Score -= score;
        Brick_Function.AddBrick -= AddBrick;
        Main_Menu_UI_Manager.Reset -= ResetScore;
    }

    // keep track on how many bricks player need to break in order to complete the level
    void score ()
    {
        scoreCount--;
        scoreText.text = $"Remaining bricks: {scoreCount}";

        if (scoreCount <= 0)
        {
            // if player clear out all the bricks then sent action for player win
            PlayerWon?.Invoke();        
        }
    }

    // add the brick to the pool when spawned in order to keep track on it
    void AddBrick ()
    {
        scoreCount++;
        scoreText.text = $"Remaining bricks: {scoreCount}";
    }

    // reset the score when player get's on main menu or lose the game 
    void ResetScore()
    {
        scoreCount = 0;    
        scoreText.text = $"Remaining bricks: {scoreCount}";
    }
}
