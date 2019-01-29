using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scanner : MonoBehaviour
{
    public LayerMask mask;
    public AudioSource a;

    private void Update()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit, Mathf.Infinity, mask))
        {

            if (hit.transform.gameObject.CompareTag("BarCode"))
            {
                print("direct hit");

                if (Vector3.Distance(transform.position, hit.transform.position) < 0.25f)
                {
                    print("succeeded");
                    hit.transform.parent.tag = "ClothScanned";
                    a.Play();
                    for (int i = 0; i < hit.transform.parent.childCount; i++)
                    {
                        if (hit.transform.root.GetChild(i).gameObject.tag != "BarCode")
                            hit.transform.root.GetChild(i).gameObject.tag = "ClothScanned";
                        else
                            hit.transform.root.GetChild(i).gameObject.SetActive(false);
                    }
                }
            }
        }
    }
}
