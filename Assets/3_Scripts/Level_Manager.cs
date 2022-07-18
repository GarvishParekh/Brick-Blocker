using UnityEngine;
using UnityEngine.SceneManagement;

public class Level_Manager : MonoBehaviour
{

    [SerializeField] GameObject[] levels;
    [SerializeField] Transform levelSpawnPlace;

    public static int levelCount = 0;

    public void SpawnLevel()
    {
        Instantiate(levels[levelCount], levelSpawnPlace.position, Quaternion.identity);
    }
}
