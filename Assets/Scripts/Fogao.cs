using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fogao : MonoBehaviour
{
    public string Receita;
    public GameObject highlight;
    Cozinha cozinheiro;
    Ingrediente novo_Ingred;
    bool cozinhando;
    bool cozinhou;
    bool queimou;

    public float tempo_Cozimento;
    public float tempo_Cozinhando;
    public float tempo_Queimar;
    float Queimar_Timer;
        void Update()
    {
        if(cozinhando == true){Cozinhar();}

        print (cozinhando);
       
    }

    void Cozinhar()
    {

        tempo_Cozinhando += Time.deltaTime;
        Queimar_Timer = tempo_Cozimento + tempo_Queimar;

        if(tempo_Cozinhando >= tempo_Cozimento)
        {
            print(Receita);

            cozinhou = true;
        }

        if(tempo_Cozinhando >= Queimar_Timer)
        {
            queimou = true;
        }


    }

    void PegarReceita()
    {

        if(!queimou)
        {
                

                    if(Receita == "FFC" || Receita == "FCF" || Receita == "CFF")
                    {

                        print ("Feijoada");
                    } 
                    
                    else if (Receita == "AFC" || Receita == "FCA" || Receita == "ACF" || Receita == "FAC" || Receita == "CAF" || Receita == "CFA")
                    {

                             print ("PF");
                    }

                    else
                    {
                        print ("Deu Ruim!!");
                    }

        }

        else
        {
            print ("Prato veio queimado, com gosto de merda!");

        }

        cozinhando = false;
        Receita = "";
    }


    void OnTriggerEnter(Collider coli)
    {
        cozinheiro = coli.gameObject.GetComponent<Cozinha>();
    }

    void OnTriggerStay(Collider coli)
    {

        if(coli.CompareTag("Cook"))
        {
            
            highlight.SetActive(true);

            if(cozinheiro.tem_Ingrediente && Input.GetKey(KeyCode.E) && Receita.Length < 3)
            {
                novo_Ingred = cozinheiro.ingrediente;
                
                if(novo_Ingred.etapas == 0)
                {
                
                    cozinheiro.ingrediente.gameObject.SetActive(false);
                    cozinheiro.tem_Ingrediente = false;
                    
                    if(Receita == "")
                    {
                        tempo_Cozinhando = 0;
                        tempo_Cozimento = 0;
                    }
        
                    Receita += novo_Ingred.nome;
                    tempo_Cozimento += novo_Ingred.tempoDeCozimento;
                    cozinhando = true;
                    
                }

                else
                {
                    print("Esse ingrediente precisa ser preparado!");
                    novo_Ingred = null;

                }
            }

            if(cozinheiro.tem_Ingrediente == false && Input.GetKey(KeyCode.E) && cozinhou && cozinhando == true)
            {
                PegarReceita();
            }


        }
    }

    void OnTriggerExit(Collider coli)
    {

        if(coli.CompareTag("Cook"))
        { 
            highlight.SetActive(false);
            
            cozinheiro = null;
        }
    }
}
