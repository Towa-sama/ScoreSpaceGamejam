using System.Collections;
using System.Collections.Generic;
using LootLocker.Requests;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class Leaderboard : MonoBehaviour
{
    private int LeaderboardID = 9834;

    [SerializeField] private TextMeshProUGUI playerNames;
    [SerializeField] private TextMeshProUGUI playerScores;
    // Start is called before the first frame update
    void Start()
    {
    }

    public IEnumerator SubmitScoreRoutine(int scoreToUpload)
    {
        bool done = false;
        string name = PlayerPrefs.GetString("Name");
        LootLockerSDKManager.SubmitScore(name, scoreToUpload, LeaderboardID, (response) =>
        {
            if (response.success)
            {
                Debug.Log("Success");
                done = true;
            }
            else
            {
                Debug.Log("Fail " + response.Error);
                done = true;
            }
            
        });
        yield return new WaitWhile(() => done == false);
    }

    public IEnumerator FetchTopHighScoresRoutine()
    {
        bool done = false;
        LootLockerSDKManager.GetScoreListMain(LeaderboardID, 25, 0, (response) =>
        {
            if (response.success)
            {
                string tempPlayerName = "Names\n";
                string tempPlayerScore = "Scores\n";

                LootLockerLeaderboardMember[] members = response.items;

                for (int i = 0; i < members.Length; i++)
                {
                    tempPlayerName += members[i].rank + ". ";
                    if (members[i].player.name != "")
                    {
                        tempPlayerName += members[i].player.name;
                    }
                    else
                    {
                        tempPlayerName += members[i].player.id;
                    }

                    tempPlayerScore += members[i].score + "\n";
                    tempPlayerName += "\n";
                }

                done = true;
                playerNames.text = tempPlayerName;
                playerScores.text = tempPlayerScore;
            }
        });
        yield return new WaitWhile(() => done == false);
    }
}
