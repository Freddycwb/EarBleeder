using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScoreActivator : MonoBehaviour
{
    [SerializeField] private GameObject[] playerScoreBars;
    [SerializeField] private IntListVariable playerScores;

    public void SetPlayerScore()
    {
        for (int i = 0; i < playerScores.Value.Count; i++)
        {
            playerScoreBars[i].SetActive(true);
        }
    }
}
