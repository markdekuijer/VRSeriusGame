using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kassaaaah : MonoBehaviour
{
    public bool Open;
    public Animator kassa;
    AudioSource a;

    private void Start()
    {
        a = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.transform.root.gameObject.name == "Player")
        {
            if (Open)
            {
                kassa.SetTrigger("open");
                if(!a.isPlaying)
                    a.Play();
            }
            else
            {
                kassa.SetTrigger("close");
                if (!a.isPlaying)
                    a.Play();
            }
        }
    }
}
