using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawnitem : MonoBehaviour
{
    public GameObject[] items;
    public float spawnDelay = 2.0f;
    public float spawnRadius = 2.0f;
    int index;

    void Start(){
        StartCoroutine(SpawnRoutine());
    }

    public void Spawn()
    {
        index = Random.Range (0, items.Length);
        Quaternion rotation = Quaternion.Euler(-90, Random.Range (0, 360), Random.Range (0, 360));


        GameObject item = (GameObject)Instantiate(items[index], 
                                                  transform.position + Random.insideUnitSphere * spawnRadius, 
                                                  rotation);
        if(Random.value >= 0.7){
            item.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotationX |
                                                         RigidbodyConstraints.FreezeRotationY | 
                                                         RigidbodyConstraints.FreezeRotationZ;
        }
            
    }

    IEnumerator SpawnRoutine()
    {
        Spawn();
        yield return new WaitForSeconds(spawnDelay);
        StartCoroutine(SpawnRoutine());
    }
}
