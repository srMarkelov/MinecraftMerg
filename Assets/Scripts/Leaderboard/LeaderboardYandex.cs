using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using YG.Example;

public class LeaderboardYandex : ILeaderboard
{
    private LeaderboardScriptableObject _leaderboardScriptableObject;
    private FinishLevelController _finishLevelController;

    public void SetNewResultLeaderboard(LeaderboardScriptableObject leaderboardScriptableObject)
    {
        _leaderboardScriptableObject = leaderboardScriptableObject;
    }

    public void SetFinishLevelController(FinishLevelController finishLevelController)
    {
        _finishLevelController = finishLevelController;
    }

    public void Inst()
    {
        var instantiateGameObject = GameObject.Instantiate(_leaderboardScriptableObject.LeaderboardGameObjects[0].LeaderboardGO);
        _finishLevelController.SetNewResultLeaderboard(instantiateGameObject.GetComponent<NewResultLeaderboard>());
    }
}
