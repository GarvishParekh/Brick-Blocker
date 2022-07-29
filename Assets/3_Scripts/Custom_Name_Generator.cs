using TMPro;
using UnityEngine;
using UnityEngine.UI;
using LootLocker.Requests;

public class Custom_Name_Generator : MonoBehaviour
{
    [SerializeField] TMP_Text player_Name_Text;
    [SerializeField] Button change_Player_name;
    [Space]
    [SerializeField] string PlayerName;
    [SerializeField] string Default_Name;

    [Space]
    public string[] First_Name = new string[]
    {
        "adorable", "brainy", "clever", "eager", "excited", "funny", "aback", "abaft", "abandoned", "abashed", "aberrant", "abhorrent",
        "abiding", "abject", "ablaze", "able", "abnormal", "aboard", "aboriginal", "abortive", "abounding", "abrasive", "abrupt", "absent", 
        "absorbed", "absorbing", "cold", "absurd", "abundant", "abusive", "acceptable", "accessible", "accidental", "accurate", "acid", 
        "acidic", "acoustic", "acrid", "actually", "adamant", "adorable", "adventurous", "Mr"
    };


    public string[] Last_Name = new string[]
    {
        "history", "map", "computer", "food", "data", "library", "idea", "area", "story", "video", "people", "history", "way", "art", "world",
        "information", "map", "Taco", "family", "Husky", "health",  "system", "computer", "meat", "year", "thanks", "music", "person",
        "reading", "method", "data", "food", "Lama", "theory", "law", "bird", "literature", "problem", "software", "control",
        "knowledge", "power", "ability", "economics", "King", "Senpai", "Bean", "Burger", "Pizza"
    }; 

    public void _Set_Player_Name()
    {
        string getFirstName = First_Name[Random.Range(0, First_Name.Length)];
        string getLastName = Last_Name[Random.Range(0, Last_Name.Length)];

        string playerName = $"{getFirstName}_{getLastName} {System.DateTime.Now.Millisecond}";
        player_Name_Text.text = playerName;
        PlayerPrefs.SetString(PlayerName, playerName);

        LootLockerSDKManager.SetPlayerName(playerName, (response) =>
        {
            if (response.success)
                Debug.Log("player Name Set");
            else
                Debug.Log("Failed setting player name");
        });
    }

    private void Start()
    {
        string checkName = PlayerPrefs.GetString(PlayerName, Default_Name);
        player_Name_Text.text = checkName;
    }

    private void Update()
    {
        if (Input.GetKeyDown (KeyCode.Space))
        {
            PlayerPrefs.DeleteKey(PlayerName);
        }
    }
}
