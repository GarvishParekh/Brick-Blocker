using TMPro;
using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Main_Menu_UI_Manager : MonoBehaviour
{
    public static Action ButtonCLick;
    public static Action Reset;
    public static Action<int> LevelSelected;

    public static Action MusicOff;
    public static Action MusicOn;

    [SerializeField] Animator main_Menu_Animation;
    [SerializeField] GameObject start_Button;
    [SerializeField] GameObject animation_Trigger;
    [SerializeField] GameObject player_racket;
    [SerializeField] GameObject player_Ball;
    [SerializeField] Vector3 ballSpawnPosition = new Vector3(0, 0, 6);

    [Header ("Main menu UI elements")]
    [SerializeField] GameObject setting_Pannel;
    [SerializeField] GameObject level_Panel;
    [SerializeField] GameObject leaderboard_Panel;

    [SerializeField] GameObject close_Button;

    [Header ("In-Game UI elements")]
    [SerializeField] string continueString;
    [SerializeField] Transform playerHearts;

    [Header ("Toggle button text")]
    [SerializeField] Color toggle_On_Color;
    [SerializeField] Color toggle_Off_Color;
    
    [Space]
    [SerializeField] TMP_Text sfx_Text;
    [SerializeField] bool sfx_On = false;

    [Space]
    [SerializeField] TMP_Text music_Text;
    [SerializeField] bool music_On = false;

    [Header("Level selection")]
    [SerializeField] TMP_Text level_Display_Text;
    [SerializeField] GameObject main_Canvas;
    [SerializeField] GameObject level_Intro_Canvas;
    [SerializeField] GameObject level_Holder;
    [SerializeField] int levelCount = 0;
    
    private void Start()
    {
        Toggle_OnStart();
    }

    private void OnEnable()
    {
        Ball_Function.BallMissed += BallMissedFunction;
    }

    private void OnDisable()
    {
        Ball_Function.BallMissed -= BallMissedFunction;
    }

    void BallMissedFunction()
    {
        if (playerHearts.GetChild(0).gameObject == null)
            return;

        GameObject firstHeart = playerHearts.GetChild(0).gameObject;
        Destroy(firstHeart);
    }

    // change panels on main menu only
    public void _ChangePanel (GameObject active_Panel)
    {
        if (active_Panel.activeInHierarchy)
            return;
        _CloseAllPanel();

        active_Panel.SetActive(true);
        close_Button.SetActive(false);
        close_Button.SetActive(true);
    }

    // close all UI panels and elements
    public void _CloseAllPanel ()
    {
        // panels
        setting_Pannel.SetActive(false);
        level_Panel.SetActive(false);
        leaderboard_Panel.SetActive(false);

        // buttons
        close_Button.SetActive(false);
    }

    public void _ButtonClick()
        => ButtonCLick?.Invoke();


    public void _StartButton ()
    {
        main_Menu_Animation.enabled = true;
        Destroy(start_Button);
    }

    public void _Exit_Button ()
    {
        Application.Quit();
    }

    // toggle setting on start
    void Toggle_OnStart()
    {
        sfx_On = true;
        sfx_Text.text = "On";
        sfx_Text.color = toggle_On_Color;

        music_On = true;
        music_Text.text = "On";
        music_Text.color = toggle_On_Color;
    }

    // sfx toggle button
    public void _Sfx_Toggle()
    {
        if (sfx_On)
        {
            sfx_On = false;
            sfx_Text.text = "Off";
            sfx_Text.color = toggle_Off_Color;
        }
        else if (!sfx_On)
        {
            sfx_On = true;
            sfx_Text.text = "On";
            sfx_Text.color = toggle_On_Color;
        }
    }

    // music toggle button
    public void _Music_Toggle()
    {
        if (music_On)
        {
            MusicOff?.Invoke();
            music_On = false;
            music_Text.text = "Off";
            music_Text.color = toggle_Off_Color;
        }
        else if (!music_On)
        {
            MusicOn?.Invoke();
            music_On = true;
            music_Text.text = "On";
            music_Text.color = toggle_On_Color;
        }
    }

    public void _Continue_Button ()
    {
        _CloseAllPanel();
        main_Canvas.SetActive(false);
        level_Intro_Canvas.SetActive(true);

        levelCount = PlayerPrefs.GetInt(continueString, 1);

        int level_Number = levelCount + 1;

        if (level_Number < 10)
            level_Display_Text.text = $"LEVEL 0{level_Number}";
        else if (level_Number >= 10)
            level_Display_Text.text = $"LEVEL {level_Number}";
    }

    // selcting levels from level panel
    public void _Level_Button (int level_Number)
    {
        _CloseAllPanel();
        main_Canvas.SetActive(false);
        level_Intro_Canvas.SetActive(true);

        levelCount = level_Number - 1;
        PlayerPrefs.SetInt(continueString, levelCount);

        if (level_Number < 10)
            level_Display_Text.text = $"LEVEL 0{level_Number}";
        else if (level_Number >= 10)
            level_Display_Text.text = $"LEVEL {level_Number}";
    }

    // back button from level to main menu 
    public void _Back_Button()
    {
        main_Canvas.SetActive(true);
        level_Intro_Canvas.SetActive(false);

        Invoke(nameof(ActiveLevelPanel), 0.7f);
    } void ActiveLevelPanel() => _ChangePanel(level_Panel);

    // for player to start the game after UI
    public void _Start_Game_Button ()
    {
        // close all the panels
        main_Canvas.SetActive(false);
        level_Intro_Canvas.SetActive(false);

        // enable all player to see the level
        level_Holder.SetActive(true);

        animation_Trigger.SetActive(false);
        animation_Trigger.SetActive(true);
        Invoke(nameof(SpawnRacket), 2);
        Invoke(nameof(SpawnBall), 2.5f);

        LevelSelected?.Invoke(levelCount);
    } void SpawnRacket() => player_racket.SetActive(true);
    void SpawnBall() => Instantiate(player_Ball, ballSpawnPosition, Quaternion.identity);


    public void _InGame_Main_Menu_Button ()
    {
        Reset?.Invoke();
        main_Canvas.SetActive(true);
        level_Intro_Canvas.SetActive(false);

        level_Holder.SetActive(false);
        player_racket.SetActive(false);
        Invoke(nameof(ActiveLevelPanel), 0.7f);
    }
}
