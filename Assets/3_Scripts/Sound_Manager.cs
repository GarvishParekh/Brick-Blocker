using UnityEngine;

public class Sound_Manager : MonoBehaviour
{

    [SerializeField] AudioSource buttonCLickSource;
    [SerializeField] AudioClip audiocbuttonCLickClip;

    private void OnEnable()
    {
        Main_Menu_UI_Manager.ButtonCLick += ButtonClickSound;
    }

    private void OnDisable()
    {
        Main_Menu_UI_Manager.ButtonCLick -= ButtonClickSound;
    }

    void ButtonClickSound ()
    {
        //buttonCLickSource.PlayOneShot(audiocbuttonCLickClip);
    }
}
