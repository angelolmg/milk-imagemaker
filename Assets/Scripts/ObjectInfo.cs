using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectInfo : MonoBehaviour
{
    public string objectName;
    public float destroyTimer = 10f;

    void Start()
    {
        Destroy(gameObject, destroyTimer);
    }

}
