using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bag : MonoBehaviour
{
    [SerializeField] private Shop shop;
    public Transform t;

    public AudioSource a, a2;

    public int neededItems;
    bool resettt;
    private void Start()
    {
        Reset();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("ClothScanned"))
        {
            print("put in back");
            other.gameObject.SetActive(false);
            neededItems--;
            a.Play();
        }
        if (other.gameObject.CompareTag("Unit") && neededItems <= 0)
        {
            shop.HandleCounterQueue();
            Reset();
            a2.Play();
        }
    }

    private void LateUpdate()
    {
        if (resettt)
        {
            transform.position = t.position;
            transform.rotation = t.rotation;
            resettt = false;
        }
    }

    private void Reset()
    {
        resettt = true;   

    }
}
