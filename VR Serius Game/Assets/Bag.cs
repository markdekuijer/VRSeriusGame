using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bag : MonoBehaviour
{
    [SerializeField] private Shop shop;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Cloth"))
        {
            print("put in back");
            other.gameObject.SetActive(false);
        }
        if (other.gameObject.CompareTag("Unit"))
        {
            shop.HandleCounterQueue();
        }
    }
}
