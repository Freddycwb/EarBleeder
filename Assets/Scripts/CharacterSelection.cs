using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSelection : MonoBehaviour
{
    [SerializeField] private GameObjectListVariable skins;
    [SerializeField] private GameObjectListVariable players;
    [SerializeField] private Animator[] skinsDisplays;
    [SerializeField] private GameObject[] selectors;
    private int[] selectorsOverlapping = new int[6];
    private List<int> skinsTaken = new List<int>(new int[] {-2, -2, -2, -2, -2, -2});

    public Action skinHasBeenSelected;

    private void OnEnable()
    {
        for (int i = 0; i < selectorsOverlapping.Length; i++)
        {
            selectorsOverlapping[i] = i;
        }
    }

    public void SetSelectorsOverlapping(int[] i)
    {
        selectorsOverlapping = i;
    }

    public int GetSelectorOverlapping(int i)
    {
        return selectorsOverlapping[i];
    }

    public int[] GetSelectorsOverlapping()
    {
        return selectorsOverlapping;
    }

    public void ResetSkinsTaken()
    {
        skinsTaken = new List<int>(new int[] { -2, -2, -2, -2, -2, -2 });
    }

    public GameObject EnableSelector(int defaultID)
    {
        GameObject skin = skins.Value[0];
        if (selectors[players.Value.Count].activeSelf)
        {
            skin = skins.Value[selectorsOverlapping[defaultID - 1]];
            return skin;
        }
        selectors[players.Value.Count].SetActive(true);
        if (selectorsOverlapping[defaultID-1] != defaultID-1)
        {
            skin = skins.Value[selectorsOverlapping[defaultID - 1]];
            return skin;
        }
        if (skinsTaken[players.Value.Count] != -2)
        {
            int id = players.Value.Count - 1;
            for (int i = 1; i < skins.Value.Count; i++)
            {
                int s = id + i > skins.Value.Count ? id + i - skins.Value.Count : id + i;
                if (skinsTaken[s] == -2)
                {
                    selectorsOverlapping[players.Value.Count] = s;
                    skinsDisplays[s].Play("SkinDisplayOverlapped");
                    skin = skins.Value[s];
                    break;
                }
            }
        }
        else
        {
            skinsDisplays[players.Value.Count].Play("SkinDisplayOverlapped");
            skin = skins.Value[players.Value.Count];
        }
        OrderSelectors();
        return skin;
    }

    public void DisableSelector(int defaultID)
    {
        for (int i = 0; i < selectors.Length; ++i)
        {
            if (i >= players.Value.Count)
            {
                if(i - 1 >= 0 && i - 1 < selectors.Length) selectors[i - 1].SetActive(false);
                break;
            }
        }
        skinsDisplays[selectorsOverlapping[defaultID - 1]].Play("SkinDisplayLeaved");
        OrderSelectors();
    }

    public GameObject MoveSelector(int defaultID, int dir)
    {
        GameObject skin = skins.Value[0];
        int escape = 0;
        int id = selectorsOverlapping[defaultID - 1];
        int oldId = id;
        bool foundSkin = false;

        while (!foundSkin)
        {
            if (dir > 0)
            {
                id -= 1;
                if (id < 0) id = selectorsOverlapping.Length - 1;
            }
            else
            {
                id += 1;
                if (id > selectorsOverlapping.Length - 1) id = 0;
            }
            if (skinsTaken[id] == -2)
            {
                skin = skins.Value[id];
                foundSkin = true;
            }
            escape++;
            if (escape >= 100)
            {
                break;
            }
        }
        selectorsOverlapping[defaultID - 1] = id;
        selectors[defaultID - 1].transform.position = skinsDisplays[id].transform.position - (Vector3.forward * 0.01f);
        skinsDisplays[id].Play("SkinDisplayOverlapped");

        // Checando se tinha alguem nesse id, se nao tocar animacao de leave na skin
        bool foundId = false;
        for (int i = 0; i < players.Value.Count && !(foundId = selectorsOverlapping[i] == oldId); i++) ;
        if (!foundId) skinsDisplays[oldId].Play("SkinDisplayLeaved");
        // --- //
    
        return skin;
    }

    public GameObject GetSkin(int defaultID)
    {
        int id = defaultID - 1;
        GameObject skin = skins.Value[selectorsOverlapping[id]];
        if (skinsTaken[selectorsOverlapping[id]] == -2)
        {
            goto stop;
        }
        if (skinsTaken[selectorsOverlapping[id]] == defaultID)
        {
            goto stop;
        }
        for (int i = 1; i < skins.Value.Count; i++)
        {
            int s = (selectorsOverlapping[id] + i) % skins.Value.Count;
            Debug.Log(s + " " + skinsTaken[s]);
            if (skinsTaken[s] == -2)
            {
                selectorsOverlapping[id] = s;
                skin = skins.Value[s];
                break;
            }
        }

        stop:
        OrderSelectors();
        return skin;
    }

    public void SelectSkin(int defaultID)
    {
        skinsDisplays[selectorsOverlapping[defaultID - 1]].Play("SkinDisplaySelected");
        skinsTaken[selectorsOverlapping[defaultID - 1]] = defaultID;
        if (skinHasBeenSelected != null)
        {
            skinHasBeenSelected.Invoke();
        }
    }

    public void DeselectSkin(int defaultID)
    {
        skinsTaken[selectorsOverlapping[defaultID - 1]] = -2;
    }

    private void OrderSelectors()
    {
        int i = 0;
        foreach (var selector in selectors)
        {
            selector.transform.position = skinsDisplays[selectorsOverlapping[i]].transform.position - (Vector3.forward * 0.01f);
            i++;
        }
    }
}
