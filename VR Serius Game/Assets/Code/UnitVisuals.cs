using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitVisuals : MonoBehaviour
{
    [Header("Heads")]
    [SerializeField] private List<GameObject> hairStyles = new List<GameObject>();

    [Header("Bodys")]
    [SerializeField] private List<GameObject> bodyStyles = new List<GameObject>();

    public Material red;
    public Material green;


    public void RandomizeLook()
    {
        int h = Random.Range(0, hairStyles.Count - 1);
        int b = Random.Range(0, hairStyles.Count - 1);

        for (int i = 0; i < hairStyles.Count; i++)
        {
            if(i != h)
                hairStyles[i].SetActive(false);
        }
        for (int i = 0; i < bodyStyles.Count; i++)
        {
            if(i != b)
                bodyStyles[i].SetActive(false);
        }
    }

    public void switchTag(bool tagged)
    {
        //bodyRenderer.material = tagged ? red : green;
        //hairRenderer.material = tagged ? red : green;
        //legsRenderer.material = tagged ? red : green;
        GetComponent<MeshRenderer>().material = tagged ? red : green;
    }
}

