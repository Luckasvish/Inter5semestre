using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RecipeChoise : MonoBehaviour
{
    internal Plates thisPlate;
    public Image[] options;
    
    public GameObject branquin;
    public GameObject[] icones;
    int index = 0;

    void Awake()
    {
        thisPlate = GetComponentInParent<Plates>();
      
    }




    void Update()
    {
        if(this.gameObject.activeInHierarchy)
        {
            if(thisPlate.settle == false)
            {
                if(MacroSistema.sistema.input_Manager.pressedQ)
                {
                    ChangeRecipe();
                }

                RunRecipeSelection();
            }

            else if(thisPlate.recipe.ingreNeeded.Count <=0)
            {
                SetRecipeOnPlateHUD();
            }
        }

        
        
    }
  
    public void ChangeRecipe()
    {
        index ++;
        if(index > 2)
        {
            index = 0;
        }

    }



    void RunRecipeSelection()
    {
        string recipeName;


        switch(index)
        {
            case 0:
            for (int i = 0; i < options.Length ; i++)
            {
                options[i].gameObject.SetActive(false);
            }
             
            options[0].gameObject.SetActive(true);
            
            recipeName = "Feijoada";   
            
            break;
            
            case 1:
            for (int i = 0; i < options.Length ; i++)
            {
                options[i].gameObject.SetActive(false);
            }
            
            options[1].gameObject.SetActive(true);
            
            recipeName = "PratoFeito";   
            
            break;
            
            case 2:
            for (int i = 0; i < options.Length ; i++)
            {
                options[i].gameObject.SetActive(false);
            }
            
            options[2].gameObject.SetActive(true);
            
            recipeName = "Buchada";   
            
            break;

            default: return;
        }
//            Debug.Log("ThisPlateRecipe: " + recipeName);

            thisPlate.ReceiveNewRecipe(recipeName);

    }

    public void CleanPlate()
    {
        branquin.SetActive(false);
    }

    public void SetRecipeOnPlateHUD()
    {
        if(branquin.activeInHierarchy == false)
        {
            branquin.SetActive(true);
            for (int i = 0; i < options.Length ; i++)
            {
                options[i].gameObject.SetActive(false);
            }
        }



            switch(thisPlate.recipe.itemName)
            {
                case "Feijoada":    icones[0].gameObject.SetActive(true);
                
                break;
            
                case "PratoFeito":  icones[1].gameObject.SetActive(true);
                
                break;
            
                case "Buchada": icones[2].gameObject.SetActive(true);
                
                break;
            
                default : break;       
            }

    }

}
