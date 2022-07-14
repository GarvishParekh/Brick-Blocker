using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Main_Menu_UI_Manager : MonoBehaviour
{
    public static Action ButtonCLick;

    [SerializeField] Animator main_Menu_Animation;
    [SerializeField] GameObject start_Button;

    [SerializeField] GameObject setting_Pannel;
    [SerializeField] GameObject level_Panel;
    [SerializeField] GameObject leaderboard_Panel;

    [SerializeField] GameObject close_Button;

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
}
