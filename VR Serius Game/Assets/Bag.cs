using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bag : MonoBehaviour
{
    [SerializeField] private Shop shop;
    Transform t;
    Rigidbody rb;

    private void Start()
    {
        t = transform;
        rb = GetComponent<Rigidbody>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Cloth"))
        {
            print("put in back");
            other.gameObject.transform.parent.gameObject.SetActive(false);
        }
        if (other.gameObject.CompareTag("Unit"))
        {
            shop.HandleCounterQueue();
            Reset();
        }
    }

    private void Reset()
    {
        transform.position = t.position;
        transform.rotation = t.rotation;
        rb.useGravity = false;
    }
}
