using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartEnd : MonoBehaviour
{
    public bool start;
    public Shop s;
    public Animator a1, a2;
    AudioSource a;

    private void Start()
    {
        a = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.root.gameObject.name == "Player")
        {
            a.Play();
            if (start)
            {
                s.Begin();
                a1.SetTrigger("start");
                a2.SetTrigger("start");
            }
            else
                Application.Quit();
        }
    }

}
