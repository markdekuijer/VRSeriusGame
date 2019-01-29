using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kassaaaah : MonoBehaviour
{
    public bool Open;
    public Animator kassa;

    private void OnTriggerEnter(Collider other)
    {
        if(other.transform.root.gameObject.name == "Player")
        {
            if (Open)
                kassa.SetTrigger("open");
            else
                kassa.SetTrigger("close");
        }
    }
}
