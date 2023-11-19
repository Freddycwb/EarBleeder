using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static UnityEditor.Experimental.GraphView.GraphView;

public class PlayerScore : MonoBehaviour
{
    [SerializeField] private int playerID;
    [SerializeField] private Image portrait;
    [SerializeField] private Animator[] stars;
    [SerializeField] private IntListVariable playerScores;
    [SerializeField] private GameObjectListVariable players;

    public void UpdateScore()
    {
        if (!gameObject.activeSelf)
        {
            return;
        }
        StartCoroutine("ScoreSequence");
    }

    private IEnumerator ScoreSequence()
    {
        portrait.sprite = players.Value[playerID].GetComponentInChildren<PlayerAnimator>().GetBody().GetComponent<SkinObjects>().scoreSprite;
        int currentScore = playerScores.Value[playerID];
        yield return new WaitForSeconds(1.5f);
        yield return new WaitForSeconds(playerID * 0.2f);
        for (int i = 0; i < currentScore; i++)
        {
            if (!stars[i].gameObject.activeSelf)
            {
                stars[i].gameObject.SetActive(true);
                stars[i].Play("Show");
                yield return new WaitForSeconds(0.1f);
            }
        }
    }

    public void SetPlayerScore()
    {
        foreach (var star in stars)
        {
            star.gameObject.SetActive(false);
        }
        if (playerScores.Value.Count < playerID + 1)
        {
            gameObject.SetActive(false);
        }
    }
}
