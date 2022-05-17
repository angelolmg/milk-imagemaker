using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawnitem : MonoBehaviour
{
    public GameObject[] items;
    public float spawnDelay = 2.0f;
    public float spawnRadius = 2.0f;
    public int maxConnectedItems = 4;
    public float spawnMultipleChance = 0.3f;
    public float uprightItemChance = 0.3f;
    public float padding = 0.015f;

    void Start(){
        StartCoroutine(SpawnRoutine());
    }

    public void Spawn()
    {
        // Spawn base object
        int index = Random.Range(0, items.Length);
        Quaternion rotation = Quaternion.Euler(-90, Random.Range (0, 360), Random.Range (0, 360));
        //Quaternion rotation = Quaternion.Euler(-90, 90, 90);
        GameObject item = Instantiate(items[index], 
                                      transform.position + Random.insideUnitSphere * spawnRadius, 
                                      rotation);

        // Create list of spawned objects
        List<GameObject> spawned = new List<GameObject>();
        spawned.Add(item);

        // Figure out how many instances to spawn
        // The chance tp spawn a new item are (1/4^i) proportinal to each additional
        // Meaning new itens will have increasingly less chance to spawn
        int extraInstances = 0;
        for (int i = 0; i < maxConnectedItems - 1; i++){
            if(Random.value <= (spawnMultipleChance/(4^i))){
                extraInstances += 1;
            }
        }

        // Spawn that many instances
        for (int i = 1; i <= extraInstances; i++){
            float d = item.GetComponent<BoxCollider>().size.x * item.transform.localScale.x;
            float distance = (d + padding) * i;
            Vector3 newpos = item.transform.position - item.transform.right * distance;
            GameObject newitem = Instantiate(items[index], newpos, rotation);
            spawned.Add(newitem);
            spawned[i-1].AddComponent<FixedJoint>().connectedBody = spawned[i].GetComponent<Rigidbody>();
        }
        
        // Freeze rotation if itens should be upright
        if(Random.value <= uprightItemChance){
            foreach (GameObject i in spawned){
                i.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotationX |
                                                          RigidbodyConstraints.FreezeRotationY | 
                                                          RigidbodyConstraints.FreezeRotationZ;
            }
        }     
    }

    IEnumerator SpawnRoutine()
    {
        Spawn();
        yield return new WaitForSeconds(spawnDelay);
        StartCoroutine(SpawnRoutine());
    }
}
