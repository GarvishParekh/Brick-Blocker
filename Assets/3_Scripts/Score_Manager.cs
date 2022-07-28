using TMPro;
using System;
using UnityEngine;

public class Score_Manager : MonoBehaviour
{
    public static Action PlayerWon;
    public static Action<int> UpdateLeaderboards;

    [SerializeField] TMP_Text bricksReaminingText;
    [SerializeField] TMP_Text totalScoreText;
    [SerializeField] TMP_Text totalScoreText_GameComplete;
    [SerializeField] int totalScore;
    [SerializeField] string TotalScoreID;
    [Space]
    [SerializeField] int brickRemainingCount;
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
        // updating bricks data
        brickRemainingCount--;
        bricksReaminingText.text = $"Remaining bricks: {brickRemainingCount}";

        // addind score and updating on leaderboards
        totalScore = PlayerPrefs.GetInt(TotalScoreID, 0);
        totalScore += 100;
        totalScoreText.text = $"Total Score: {totalScore}";
        totalScoreText_GameComplete.text = $"Total Score: {totalScore}";
        PlayerPrefs.SetInt(TotalScoreID, totalScore);
        // updating on lootlocker
        UpdateLeaderboards?.Invoke(totalScore);

        if (brickRemainingCount <= 0)
        {
            // if player clear out all the bricks then sent action for player win
            PlayerWon?.Invoke();        
        }
    }

    // add the brick to the pool when spawned in order to keep track on it
    void AddBrick ()
    {
        brickRemainingCount++;
        bricksReaminingText.text = $"Remaining bricks: {brickRemainingCount}";
    }

    // reset the score when player get's on main menu or lose the game 
    void ResetScore()
    {
        brickRemainingCount = 0;    
        bricksReaminingText.text = $"Remaining bricks: {brickRemainingCount}";
    }
}
