using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class collistionDebugger : MonoBehaviour
{
    private CapsuleCollider col;

    private void Start()
    {
        col = GetComponent<CapsuleCollider>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log(collision.gameObject.name);
    }
}
