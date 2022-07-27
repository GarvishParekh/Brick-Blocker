using UnityEngine;

public class Game_Status : MonoBehaviour
{
    public enum GameState
    {
        onMenu,
        isPlaying,
        playerWon,
        playerLose
    }
    [SerializeField] GameState gameState;

    [SerializeField] bool gameCompleted = false;
    bool playerIsPlaying = false;

    [Header("Objects to disable")]
    [SerializeField] GameObject recket;
    [SerializeField] GameObject playerBall;
    [SerializeField] string ball_Tag;

    private void Awake()
    {
        Debug.Log($"On menu state");
        gameState = GameState.onMenu;
    }

    private void OnEnable()
    {
        Main_Menu_UI_Manager.Reset += OnReset;
        Score_Manager.PlayerWon += IfPlayerWon;
        Main_Menu_UI_Manager.PlayerLose += IfPlayerLose;
        Main_Menu_UI_Manager.LevelSelected += OnLevelLoad;
    }

    private void OnDisable()
    {
        Main_Menu_UI_Manager.Reset -= OnReset;
        Score_Manager.PlayerWon -= IfPlayerWon;
        Main_Menu_UI_Manager.PlayerLose -= IfPlayerLose;
        Main_Menu_UI_Manager.LevelSelected -= OnLevelLoad;
    }

    // on loading level game's state should change to "siPlaying"
    void OnLevelLoad (int dummy)
    {
        gameState = GameState.isPlaying;
        playerIsPlaying = true;
        Debug.Log($"Start checking game start");
    }

    // on payer win game's state should change to player won
    void IfPlayerWon()
    {
        Debug.Log($"Player won");
        gameCompleted = true;
        gameState = GameState.playerWon;
    }

    // if player lose all the hearts game's state should turn to player lose
    void IfPlayerLose()
    {
        Debug.Log($"Player lose");
        gameCompleted = true;
        gameState = GameState.playerLose;
    }

    private void Update()
    {
        // keep checking if the player lost the game or won the game so can take further actions
        if (gameCompleted)
        {
            recket.SetActive(false);
            if (playerBall == null)
                playerBall = GameObject.FindGameObjectWithTag(ball_Tag);
            Destroy(playerBall);
        }
    }

    // when player resets by either entring the main menu or restarting the level
    void OnReset()
    {
        gameCompleted = false;
        gameState = GameState.onMenu;
    }
}
