using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ILeaderboard
{
    public void SetNewResultLeaderboard(LeaderboardScriptableObject leaderboardScriptableObject);
    public void SetFinishLevelController(FinishLevelController finishLevelController);
    public void Inst();
}
