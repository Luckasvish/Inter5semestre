using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Oven : MonoBehaviour
{
    internal int cooking_Ingredients;
    string recipe;
    float cooking_Time;
    float cooking_Timer = 0;

    public bool canRetrieveRecipe;
    bool cooking;

    void Update()
    {
        if(cooking)
        {
            Cooking();
        }
    }



    public void ReceiveIngredients(Ingredientes ingre)
    {

        if( ingre.prepared == true)
        {
            cooking_Ingredients ++;
            recipe += ingre.code;
            cooking_Timer+= ingre.cookingTime;
            cooking = true;

        }
    }




    void Cooking()
    {    
        cooking_Time += Time.deltaTime;
        float burn_Timer = 1/3 * cooking_Timer;

        if(cooking_Time >= burn_Timer + cooking_Timer)
        {
            Debug.Log("Comida queimoou !!!!");///
            
        }
        
        else if(cooking_Time >= cooking_Timer)
        {
            canRetrieveRecipe = true;
            Debug.Log("Comida pronta"); ///
            Debug.Log("Pra queimar :" + (cooking_Timer + burn_Timer) +" / " + cooking_Time);
        }
        else
        {
            Debug.Log("Cozinhando :" + cooking_Timer + " / " + cooking_Time);
        }



    }

    public Itens GiveRecipe()
    {
        int counter = 0;
        Itens[] recipe_List;

        switch(recipe)
        {
            case "FFC" :
            case "FCF" :
            case "CFF" :

                recipe_List = MacroSistema.sistema.feijoada;
                foreach(Itens feijoca in recipe_List)
                {

                    if(feijoca.gameObject.activeInHierarchy)
                    {
                        counter ++;
                    }
                }
                Itens feijoada = recipe_List[counter];
                feijoada.gameObject.SetActive(true);
                canRetrieveRecipe = false;
                cooking = false;
                return feijoada;

            case "AFC" :
            case "ACF" :
            case "CFA" :
            case "CAF" :
            case "FAC" :
            case "FCA" :

                recipe_List = MacroSistema.sistema.PFs;
                foreach(Itens pratoF in recipe_List)
                {
                    if(pratoF.gameObject.activeInHierarchy)
                    {
                        counter ++;
                    }
                }
                Itens PF = recipe_List[counter];
                PF.gameObject.SetActive(true);
                canRetrieveRecipe = false;
                cooking = false;
                return PF;

            

            case "FaCC" :
            case "CFaC" :
            case "CCFa" :

                recipe_List = MacroSistema.sistema.Farofa;
                foreach(Itens farofada in recipe_List)
                {
                    if(farofada.gameObject.activeInHierarchy)
                    {
                        counter ++;
                    }
                }
                Itens Farofa = recipe_List[counter];
                Farofa.gameObject.SetActive(true);
                canRetrieveRecipe = false;
                cooking = false;
                return Farofa;

                
            default : Debug.Log("Deu ruim");
            return null;
        }



    }

}
