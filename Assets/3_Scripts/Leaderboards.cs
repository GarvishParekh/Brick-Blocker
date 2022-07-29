using TMPro;
using UnityEngine;
using System.Collections;
using LootLocker.Requests;

public class Leaderboards : MonoBehaviour
{
    private readonly int leaderBoardID = 5002;
    string playerID;

    [SerializeField] TMP_Text[] nameText;
    [SerializeField] TMP_Text[] scoreText;

    private void Start()
    {
        StartCoroutine(LoginRoutine());
    }

    #region Lootlocker Login
    IEnumerator LoginRoutine ()
    {
        bool done = false;
        LootLockerSDKManager.StartGuestSession((response) =>
        {
            if (response.success)
            {
                Debug.Log("Login susscefull");
                PlayerPrefs.SetString(playerID, response.player_id.ToString());
                done = true;
            }
            else
            {
                Debug.Log("Could not login");
                done = true;
            }
        });
        yield return new WaitWhile(() => done == false); 
    }
    #endregion

    private void OnEnable()
    {
        Score_Manager.UpdateLeaderboards += UpdateScore;
    }

    private void OnDisable()
    {
        Score_Manager.UpdateLeaderboards -= UpdateScore;
    }

    IEnumerator SubmitScoreRoutine (int scoreToUpload)
    {
        bool done = false;
        string playerIdentifier = PlayerPrefs.GetString(playerID);
        LootLockerSDKManager.SubmitScore(playerIdentifier, scoreToUpload, leaderBoardID, (response) =>
        {
            if (response.success)
            {
                Debug.Log("Score uploaded");
                done = true;
            }
            else
            {
                Debug.Log($"Failed {response.Error}");
                done = true;
            }
        });
        yield return new WaitWhile(() => done == false);
    }

    void UpdateScore (int ScoreToUpdate)
        =>StartCoroutine(nameof(SubmitScoreRoutine), ScoreToUpdate);

    // get data from the leaderbaords 
    [System.Obsolete]
    IEnumerator FetchToHighScoreRoutine ()
    {
        bool done = false;
        LootLockerSDKManager.GetScoreListMain(leaderBoardID, 10, 0, (response) =>
        {
            if (response.success)
            {
                LootLockerLeaderboardMember[] members = response.items;

                for (int i = 0; i < members.Length; i++)
                {
                    if (members[i].player.name != "")
                        nameText[i].text = members[i].player.name.ToString();
                    else
                    {
                        nameText[i].text = members[i].player.id.ToString();
                    }
                    scoreText[i].text = members[i].score.ToString();
                }
            }
            else
            {
                Debug.Log($"Failed {response.Error}");
                done = true;
            }
        });
        yield return new WaitWhile(() => done == false); 
    }

    public void  _ShowLeaderBaords ()
    {
        StartCoroutine(nameof(FetchToHighScoreRoutine));
    }
}
