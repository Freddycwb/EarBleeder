using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinnerActor : MonoBehaviour
{
    [SerializeField] private GameObjectListVariable players;
    [SerializeField] private IntListVariable scores;

    [SerializeField] private GameObject body;
    [SerializeField] private GameObject[] arms;
    [SerializeField] private GameObject[] legs;
    [SerializeField] private MeshRenderer eyes;

    private bool _haveSkin;

    public void SetSkin()
    {
        int biggestScore = 0;
        int winnerIndex = 0;
        int i = 0;
        foreach (var score in scores.Value)
        {
            if (score > biggestScore)
            {
                biggestScore = score;
                winnerIndex = i;
            }
            i++;
        }
        GameObject skin = players.Value[winnerIndex].GetComponentInChildren<PlayerAnimator>().GetBody();
        if (_haveSkin)
        {
            Destroy(body.transform.GetChild(0).gameObject);
        }
        SkinObjects newBody = Instantiate(skin, body.transform.position, body.transform.rotation).GetComponent<SkinObjects>();
        Destroy(newBody.transform.GetChild(0).gameObject);
        newBody.transform.SetParent(body.transform);
        i = 0;
        foreach (GameObject newArms in arms)
        {
            if (newBody.arms.Length > 0)
            {
                arms[i].GetComponent<MeshRenderer>().enabled = false;
                GameObject newArm = Instantiate(newBody.arms[i], arms[i].transform.position, arms[i].transform.rotation);
                newArm.transform.SetParent(arms[i].transform);
            }
            else
            {
                arms[i].GetComponent<MeshRenderer>().enabled = true;
                if (arms[i].transform.childCount > 0)
                {
                    Destroy(arms[i].transform.GetChild(0).gameObject);
                }
            }
            newArms.GetComponent<MeshRenderer>().material = newBody.limbsMaterial;
            i++;
        }

        foreach (GameObject legs in legs)
        {
            legs.GetComponent<MeshRenderer>().material = newBody.limbsMaterial;
        }

        eyes.material = newBody.victoryEyes;
        _haveSkin = true;
    }
}
