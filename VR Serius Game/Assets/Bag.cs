using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bag : MonoBehaviour
{
    [SerializeField] private Shop shop;
    Vector3 t;
    Quaternion q;
    Rigidbody rb;

    public int neededItems;

    private void Start()
    {
        t = transform.position;
        q = transform.rotation;
        rb = GetComponent<Rigidbody>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("ClothScanned"))
        {
            print("put in back");
            other.gameObject.transform.parent.gameObject.SetActive(false);
            neededItems--;
        }
        if (other.gameObject.CompareTag("Unit") && neededItems <= 0)
        {
            shop.HandleCounterQueue();
            Reset();
        }
    }

    private void Reset()
    {
        transform.position = t;
        transform.rotation = q;
        rb.useGravity = false;
    }
}
