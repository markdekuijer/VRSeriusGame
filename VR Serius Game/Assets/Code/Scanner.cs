using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scanner : MonoBehaviour
{
    private void Update()
    {
        RaycastHit[] hits = Physics.BoxCastAll(transform.position + new Vector3(0, .02f, 0), new Vector3(0.15f,0.01f,0.15f), transform.forward);
        for (int i = 0; i < hits.Length; i++)
        {
            if (hits[i].transform.gameObject.CompareTag("BarCode"))
            {
                print("barcode in box");

                RaycastHit hit;
                if (Physics.Raycast(transform.position, hits[i].transform.position - transform.position, out hit, Mathf.Infinity))
                {
                    print("raycast hit");

                    if (hit.transform.gameObject.CompareTag("BarCode"))
                    {
                        print("direct hit");

                        if (Vector3.Distance(transform.position,hit.transform.position) < 0.1f)
                        {
                            print("succeeded");
                            hits[i].transform.root.tag = "ClothScanned";
                        }
                    }
                }
            }
            //print(hits[i].transform.gameObject.name);
        }
        //print("___________________");
    }
}
