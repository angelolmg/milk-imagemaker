using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class destroyitem : MonoBehaviour
{
    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject != null && col.gameObject.tag == "Product")
            Destroy(col.gameObject);
    }
}
