using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeltChanger : MonoBehaviour
{
    public float changingDelay = 1f;
    public Material[] materials;
    GameObject conveyourBelt;

    void ChangeMaterial()
    {
        int index = Random.Range(0, materials.Length);
        Vector2 offset = new Vector2(Random.value, Random.value);
        materials[index].SetTextureOffset("_MainTex", offset);
        conveyourBelt.GetComponent<MeshRenderer>().material = materials[index];
    }

    IEnumerator ChangeRoutine()
    {
        ChangeMaterial();
        yield return new WaitForSeconds(changingDelay);
        StartCoroutine(ChangeRoutine());
    }

    // Start is called before the first frame update
    void Start()
    {
        conveyourBelt = FindObjectOfType<ConveyourBelt>().gameObject;
        StartCoroutine(ChangeRoutine());
    }

}
