using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlateHUD : MonoBehaviour
{
    internal Plates thisPlate;
    public Image[] options;
    public GameObject[] recipes;
    public Image selector;

   

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
                RunRecipeSelection();
            }
            else 
            {
                SetRecipeOnPlateHUD();
            }
        }

        
        
    }
  

    void RunRecipeSelection()
    {
        string recipeName;
            
            if(MacroSistema.sistema.input_Manager.pressed01)
            {
                selector.transform.position = options[0].transform.position;
                recipeName = "Feijoada";    
            }

            else if(MacroSistema.sistema.input_Manager.pressed02)
            {
                selector.transform.position = options[1].transform.position;
                recipeName = "PratoFeito";
                
            }

            else if(MacroSistema.sistema.input_Manager.pressed03)
            {
                selector.transform.position = options[2].transform.position;
                recipeName = "Buchada";
            }
           
            else return;

            thisPlate.ReceiveNewRecipe(recipeName);

    }
    public void SetRecipeOnPlateHUD()
    {
         for (int i = 0; i < options.Length ; i++)
            {
                options[i].gameObject.SetActive(false);
            }
            selector.gameObject.SetActive(false);

            switch(thisPlate.recipe.itemName)
            {
                case "Feijoada": recipes[0].SetActive(true); break;
            
                case "PratoFeito": recipes[1].SetActive(true); break;
            
                case "Buchada": recipes[2].SetActive(true); break;
            
                default : break;
            
            }

    }

}
