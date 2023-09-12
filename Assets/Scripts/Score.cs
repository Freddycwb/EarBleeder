using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Score : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI[] scores;

    [SerializeField] private IntVariable playersNumber;
    [SerializeField] private IntListVariable playerScores;

    public void ShowScore()
    {
        for (int i = 0; i < playersNumber.Value; i++)
        {
            scores[i].gameObject.SetActive(true);
            scores[i].text = playerScores.Value[i].ToString();
        }
    }

    public void HideScore()
    {
        for (int i = 0; i < playersNumber.Value; i++)
        {
            scores[i].gameObject.SetActive(false);
        }
    }
}
