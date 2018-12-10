using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopExit : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Unit"))
        {
            other.gameObject.GetComponent<Unit>().CheckStolenItems();
        }
    }
}
