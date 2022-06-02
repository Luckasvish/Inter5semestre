using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClientRandomizer : MonoBehaviour
{
    public Material[] colors;
    public GameObject[] hairs;

    public void HairRandomizer()
    {
        int i = Random.Range(0, hairs.Length);
        int h = Random.Range(0,colors.Length);
        hairs[i].SetActive(true);
        hairs[i].GetComponent<Renderer>().sharedMaterial = colors[h];
    }

}
