using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    [SerializeField] private GameObject body;

    public void SetBody(GameObject skin)
    {
        if (body.transform.childCount > 0)
        {
            Destroy(body.transform.GetChild(0).gameObject);
        }
        GameObject bodyNewChild = Instantiate(skin, body.transform.position, body.transform.rotation);
        bodyNewChild.transform.SetParent(body.transform);
    }
}
