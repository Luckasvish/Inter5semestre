using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Armazenagem : MonoBehaviour
{
    public Ingrediente ingrediente;
    public GameObject highlight;

    void OnTriggerStay(Collider coli)
    {

        if(coli.CompareTag("Cook"))
        {
            Cozinha cozinheiro = coli.gameObject.GetComponent<Cozinha>();
            
            highlight.SetActive(true);
            if(cozinheiro.tem_Ingrediente == false && Input.GetKey(KeyCode.E))
            {
                ingrediente.gameObject.SetActive(true);
                cozinheiro.ingrediente = ingrediente;
                cozinheiro.tem_Ingrediente = true;
            }
        }
    }

    void OnTriggerExit(Collider coli)
    {

        if(coli.CompareTag("Cook"))
        { 
            highlight.SetActive(false);
        }
    }
}
