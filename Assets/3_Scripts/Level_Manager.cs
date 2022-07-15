using UnityEngine;
using UnityEngine.SceneManagement;

public class Level_Manager : MonoBehaviour
{
    // example for comments

    public static Level_Manager levelInstane;
    public static int levelCount = 0;

    private void Awake()
    {
        if (levelInstane == null)
            levelInstane = this;
        else
        {
            Destroy(gameObject);
            return;
        }
        
        DontDestroyOnLoad(gameObject);
    }

    public void ChangeLevel()
    {
        SceneManager.LoadScene(levelCount);
    }

}
