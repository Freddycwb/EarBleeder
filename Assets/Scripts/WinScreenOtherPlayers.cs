using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class WinScreenOtherPlayers : MonoBehaviour
{
    [SerializeField] private GameObjectListVariable players;
    [SerializeField] private IntListVariable scores;

    [SerializeField] private Image[] othersDisplays;

    public void SetOtherPlayers()
    {
        foreach (var display in othersDisplays)
        {
            display.transform.parent.gameObject.SetActive(false);
        }

        for (int i = 0; i < scores.Value.Count - 1; i++)
        {
            othersDisplays[i].transform.parent.gameObject.SetActive(true);
        }

        Vector2[] playerAndScore = new Vector2[scores.Value.Count];

        for (int i = 0; i < scores.Value.Count; i++)
        {
            playerAndScore[i] = new Vector2(i, scores.Value[i]);
        }

        playerAndScore = playerAndScore.OrderByDescending(x => x.y).ToArray();

        for (int i = 1; i < scores.Value.Count; i++)
        {
            othersDisplays[i - 1].sprite = players.Value[Mathf.FloorToInt(playerAndScore[i].x)].GetComponentInChildren<PlayerAnimator>().GetBody().GetComponent<SkinObjects>().loseSprite;
        }
    }
}
