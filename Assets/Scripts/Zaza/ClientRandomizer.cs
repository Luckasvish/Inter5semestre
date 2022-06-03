using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClientRandomizer : MonoBehaviour
{
    public Material[] colors;
    public Material[] clothes;
    public GameObject[] hairs;

    public void HairRandomizer()
    {
        int i = Random.Range(0, hairs.Length);
        int h = Random.Range(0,colors.Length);
        int c = Random.Range(0,clothes.Length);
        
        hairs[i].SetActive(true);
        hairs[i].GetComponent<Renderer>().sharedMaterial = colors[h];
        this.GetComponentInChildren<Renderer>().sharedMaterial = clothes[h];
    }

}
