using TMPro;
using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Main_Menu_UI_Manager : MonoBehaviour
{
    public static Action ButtonCLick;

    [SerializeField] Animator main_Menu_Animation;
    [SerializeField] GameObject start_Button;

    [Header ("Main menu panels")]
    [SerializeField] GameObject setting_Pannel;
    [SerializeField] GameObject level_Panel;
    [SerializeField] GameObject leaderboard_Panel;

    [SerializeField] GameObject close_Button;

    [Header ("Toggle button text")]
    [SerializeField] Color toggle_On_Color;
    [SerializeField] Color toggle_Off_Color;
    
    [Space]
    [SerializeField] TMP_Text sfx_Text;
    [SerializeField] bool sfx_On = false;

    [Space]
    [SerializeField] TMP_Text music_Text;
    [SerializeField] bool music_On = false;

    private void Start()
    {
        Toggle_OnStart();
    }

    public void _ChangePanel (GameObject active_Panel)
    {
        if (active_Panel.activeInHierarchy)
            return;
        _CloseAllPanel();

        active_Panel.SetActive(true);
        close_Button.SetActive(false);
        close_Button.SetActive(true);
    }

    public void _CloseAllPanel ()
    {
        setting_Pannel.SetActive(false);
        level_Panel.SetActive(false);
        leaderboard_Panel.SetActive(false);

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
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    void Toggle_OnStart()
    {
        sfx_On = true;
        sfx_Text.text = "On";
        sfx_Text.color = toggle_On_Color;

        music_On = true;
        music_Text.text = "On";
        music_Text.color = toggle_On_Color;
    }

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

    public void _Music_Toggle()
    {
        if (music_On)
        {
            music_On = false;
            music_Text.text = "Off";
            music_Text.color = toggle_Off_Color;
        }
        else if (!music_On)
        {
            music_On = true;
            music_Text.text = "On";
            music_Text.color = toggle_On_Color;
        }
    }
}
