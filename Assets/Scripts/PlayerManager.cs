using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LootLocker.Requests;

public class PlayerManager : MonoBehaviour
{
    private Leaderboard _leaderboard;
    // Start is called before the first frame update
    void Start()
    {
        _leaderboard = GameObject.FindWithTag("MainCamera").GetComponent<Leaderboard>();
        StartCoroutine(SetupRoutine());
    }

    IEnumerator SetupRoutine()
    {
        yield return StartCoroutine(LoginRoutine());
        yield return StartCoroutine(_leaderboard.FetchTopHighScoresRoutine());
    }

    IEnumerator LoginRoutine()
    {
        bool done = false;
        LootLockerSDKManager.StartGuestSession((response) =>
        {
            if (response.success)
            {
                Debug.Log("Player was logged in");

                done = true;
            }
            else
            {
                Debug.Log("Couldnt login");
                done = true;
            }
        });
        yield return new WaitWhile(() => done == false);
    }
}
