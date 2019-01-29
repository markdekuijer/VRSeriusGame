using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoneyDeleter : MonoBehaviour
{
    AudioSource a;

    private void Start()
    {
        a = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Cash"))
        {
            other.gameObject.SetActive(false);
            a.Play();
        }
    }
}
