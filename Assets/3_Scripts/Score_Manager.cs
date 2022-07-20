using TMPro;
using UnityEngine;

public class Score_Manager : MonoBehaviour
{
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

    void score ()
    {
        scoreCount--;
        scoreText.text = $"Remaining bricks: {scoreCount}";
    }

    void AddBrick ()
    {
        scoreCount++;
        scoreText.text = $"Remaining bricks: {scoreCount}";
    }

    void ResetScore()
    {
        scoreCount = 0;    
        scoreText.text = $"Remaining bricks: {scoreCount}";
    }
}
