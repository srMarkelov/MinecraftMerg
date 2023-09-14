using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "LeaderboardSO",menuName = "Scriptables/LeaderboardSO")]
public class LeaderboardScriptableObject : ScriptableObject
{
    [SerializeField] private List<LeaderboardGameObject> leaderboardGameObjects;
    public List<LeaderboardGameObject> LeaderboardGameObjects => leaderboardGameObjects;
}

[Serializable]
public class LeaderboardGameObject
{
    [SerializeField] private LeaderboardType nameLeaderboard;
    [SerializeField] private GameObject leaderboardGO;
    
    public LeaderboardType NameLeaderboard => nameLeaderboard;
    public GameObject LeaderboardGO => leaderboardGO;
}
