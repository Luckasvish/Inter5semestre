using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tabua_Carne : MonoBehaviour
{
   public GameObject highlight;
   bool preparando;
   public float tempo_Preparo;
   float tempo_Timer;

   Cozinha cozinheiro;

    void Update()
    {

        if(preparando){Preparar();}

    }
   
    void Preparar()
    {
        tempo_Timer -= Time.deltaTime;

        if(tempo_Timer <=0)
        {

            cozinheiro.ingrediente.etapas = 0;
            print("Cortou a Carne");
            preparando = false;
        }


    }


   void OnTriggerStay(Collider coli)
    {

        if(coli.CompareTag("Cook"))
        {
            cozinheiro = coli.gameObject.GetComponent<Cozinha>();
            highlight.SetActive(true);

            if(cozinheiro.ingrediente.nome == "C" && Input.GetKey(KeyCode.E))
            {
                preparando = true;   
                tempo_Timer = tempo_Preparo;
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
