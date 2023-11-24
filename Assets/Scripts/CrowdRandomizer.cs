using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrowdRandomizer : MonoBehaviour
{
    [SerializeField] private GameObject crowdPrefab;
    [SerializeField] private Vector2 gridSize;
    [SerializeField] private Vector2 min, areaSize;
    [SerializeField] private Transform parent;

    private List<GameObject> currentCrowd = new List<GameObject>();


    void Start()
    {
        GenerateCrowd();
    }

    public void GenerateCrowd()
    {
        for (int i = currentCrowd.Count - 1; i >= 0; i--)
        {
            Destroy(currentCrowd[i]);
            currentCrowd.RemoveAt(i);
        }

        for (int i = 0; i < gridSize.x; i++)
        {
            for (int j = 0; j < gridSize.y; j++)
            {
                Vector2 worldPos = areaSize * new Vector2(i,j) / gridSize + min;
                var a = Instantiate(crowdPrefab, new Vector3(worldPos.x, transform.position.y, worldPos.y), Quaternion.identity);
                currentCrowd.Add(a);
                a.transform.SetParent(parent);
            }
        }
    }
}
