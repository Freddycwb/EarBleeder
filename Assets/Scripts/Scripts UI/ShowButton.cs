using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowButton : MonoBehaviour
{
    public float delay;
    public Animator button;

    void Start()
    {
        //button = GameObject.GetComponent<Animator>();
        StartCoroutine(ShowAfter());

    }
    IEnumerator ShowAfter()
    {
        delay = delay;
            yield return new WaitForSeconds(delay);
            button.SetTrigger("Show");
    }
}
