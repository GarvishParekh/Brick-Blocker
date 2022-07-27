using TMPro;
using System;
using UnityEngine;

public class Main_Menu_UI_Manager : MonoBehaviour
{
    public static Action Reset;
    public static Action PlayerLose;
    public static Action ButtonCLick;
    public static Action<int> LevelSelected;

    public static Action MusicOn;
    public static Action MusicOff;
    [Header ("Plaer pref")]
    [SerializeField] string MusicPlayerPref;

    [Header ("Components")]
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
    [SerializeField] int heartsCount;
    [SerializeField] GameObject hearts;
    [SerializeField] GameObject gameOverPanel;
    [SerializeField] GameObject levelCompletePanel;
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
        // if hearts all are used
        if (playerHearts.childCount == 0)
        {
            Debug.Log($"Player lose");
            gameOverPanel.SetActive(true);     // enable the in game ui after losing all hearts
            PlayerLose?.Invoke();

            // reset the level holder 
            level_Holder.SetActive(false);
            level_Holder.SetActive(true);
            return;
        }

        // if hearts are left 
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

    #region Toggle Settings 
    // toggle setting on start
    void Toggle_OnStart()
    {
        sfx_On = true;
        sfx_Text.text = "On";
        sfx_Text.color = toggle_On_Color;

        // getting the last setting for music 
        int music_toggle = PlayerPrefs.GetInt(MusicPlayerPref, 1);
        if (music_toggle == 0)
        {
            MusicOff?.Invoke();
            music_On = false;
            music_Text.text = "Off";
            music_Text.color = toggle_Off_Color;
        }
        else if (music_toggle == 1)
        {
            MusicOn?.Invoke();
            music_On = true;
            music_Text.text = "On";
            music_Text.color = toggle_On_Color;
        }
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
            PlayerPrefs.SetInt(MusicPlayerPref, 0);
            MusicOff?.Invoke();
            music_On = false;
            music_Text.text = "Off";
            music_Text.color = toggle_Off_Color;
        }
        else if (!music_On)
        {
            PlayerPrefs.SetInt(MusicPlayerPref, 1);
            MusicOn?.Invoke();
            music_On = true;
            music_Text.text = "On";
            music_Text.color = toggle_On_Color;
        }
    }
    #endregion

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

    public void _NextLevel_Button()
    {
        _CloseAllPanel();
        ResetEvents();

        main_Canvas.SetActive(false);
        level_Intro_Canvas.SetActive(true);

        int CurrentLevel = levelCount;
        levelCount = CurrentLevel + 1;
        PlayerPrefs.SetInt(continueString, levelCount);
        int level_Number = levelCount + 1;          // Display number for level

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
        //spawn player life 
        for (int i = 0; i < heartsCount; i++)
        {
            Instantiate(hearts, playerHearts.position, Quaternion.identity, playerHearts);
        }
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

    #region Reset Events
    void ResetEvents ()
    {
        gameOverPanel.SetActive(false);        // close the in-game UI
        // close all the hearts 
        for (int i = 0; i < playerHearts.childCount; i++)
        {
            GameObject child = playerHearts.GetChild(i).gameObject;
            Destroy(child);
        }
        Reset?.Invoke();
        Invoke(nameof(ActiveLevelPanel), 0.7f);

        // level amd player racket
        level_Holder.SetActive(false);
        player_racket.SetActive(false);
    }

    public void _InGame_Main_Menu_Button ()
    {
        ResetEvents();

        // manage canvas
        main_Canvas.SetActive(true);
        level_Intro_Canvas.SetActive(false);
    }


    public void _InGame_Restart_Button()
    {
        ResetEvents();

        // manage canvas
        main_Canvas.SetActive(false);
        level_Intro_Canvas.SetActive(true);
    }
    #endregion
}
