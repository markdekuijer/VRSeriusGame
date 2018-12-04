using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scanner : MonoBehaviour
{
    private void Update()
    {
        Physics.BoxCastAll(transform.position + new Vector3(0, .02f, 0), new Vector3(0.3f,0.05f,0.3f), Vector3.up);
    }
    private void OnDrawGizmos()
    {
        Gizmos.DrawCube(transform.position + new Vector3(0, .02f, 0), new Vector3(0.3f, 0.05f, 0.3f));
    }
}
