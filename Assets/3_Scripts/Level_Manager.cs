using UnityEngine;

public class Level_Manager : MonoBehaviour
{
    [SerializeField] GameObject[] levels;
    [SerializeField] GameObject activeLevel;
    [SerializeField] Transform levelSpawnPlace;

    private void OnEnable()
    {
        Main_Menu_UI_Manager.LevelSelected += SpawnLevel;
        Main_Menu_UI_Manager.Reset += ResetLevel;
    }

    private void OnDisable()
    {
        Main_Menu_UI_Manager.LevelSelected -= SpawnLevel;
        Main_Menu_UI_Manager.Reset -= ResetLevel;
    }

    public void SpawnLevel(int levelCount)
    {
        activeLevel = Instantiate(levels[levelCount], levelSpawnPlace.position, Quaternion.identity);
    }

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
