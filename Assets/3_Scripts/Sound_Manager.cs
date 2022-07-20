using UnityEngine;

public class Sound_Manager : MonoBehaviour
{

    [SerializeField] AudioSource backgroundMusicSource;
    [SerializeField] AudioClip backgroundMusicClip;

    private void OnEnable()
    {
        Main_Menu_UI_Manager.MusicOn += MusicOn;
        Main_Menu_UI_Manager.MusicOff += MusicOff;
    }

    private void OnDisable()
    {
        Main_Menu_UI_Manager.MusicOn -= MusicOn;
        Main_Menu_UI_Manager.MusicOff -= MusicOff;
    }

    void MusicOn ()
    {
        backgroundMusicSource.enabled = true;
    }

    void MusicOff ()
    {
        backgroundMusicSource.enabled = false;
    }
}
