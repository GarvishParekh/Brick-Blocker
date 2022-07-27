using UnityEngine;

public class Level_Manager : MonoBehaviour
{
    [SerializeField] GameObject[] levels;
    [SerializeField] GameObject activeLevel;
    [SerializeField] Transform levelSpawnPlace;

    private void OnEnable()
    {
        Main_Menu_UI_Manager.Reset += ResetLevel;
        Main_Menu_UI_Manager.LevelSelected += SpawnLevel;
    }

    private void OnDisable()
    {
        Main_Menu_UI_Manager.Reset -= ResetLevel;
        Main_Menu_UI_Manager.LevelSelected -= SpawnLevel;
    }

    public void SpawnLevel(int levelCount)
    {
        // get the level count via button player clicked on and spawn the level
        activeLevel = Instantiate(levels[levelCount], levelSpawnPlace.position, Quaternion.identity);
    }

    // reset the active level for spawning new level
    void ResetLevel ()
    {
        if (activeLevel == null)
        {
            Debug.Log("No level selected");
            return;
        }
        Destroy(activeLevel);
    }
}
