using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class LeaderboardController : MonoBehaviour
{
    [SerializeField] private LeaderboardScriptableObject _leaderboardScriptableObject;
    [SerializeField] private FinishLevelController _finishLevelController;

    public ILeaderboard ILeaderboard;

    private void Awake()
    {
#if UNITY_WEBGL
        ILeaderboard = new LeaderboardYandex();
#endif
        if (ILeaderboard != null)
        {
            ILeaderboard.SetNewResultLeaderboard(_leaderboardScriptableObject);
            ILeaderboard.SetFinishLevelController(_finishLevelController);
        }
    }

    private void Start()
    {
        if (ILeaderboard != null)
        {
            ILeaderboard.Inst();
        }
    }
}
