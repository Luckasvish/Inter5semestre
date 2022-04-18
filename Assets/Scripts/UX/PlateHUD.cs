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

    internal MeshRenderer mesh;
    public Material[] material; 


    


    void Awake()
    {
        thisPlate = GetComponentInParent<Plates>();
        mesh = thisPlate.GetComponent<MeshRenderer>();
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
        
            if(MacroSistema.sistema.input_Manager.pressed01)
            {
                selector.transform.position = options[0].transform.position;
                thisPlate.recipe = new Recipes("Feijoada");
                mesh.material = material[0]; 
                
                
            }

            else if(MacroSistema.sistema.input_Manager.pressed02)
            {
                selector.transform.position = options[1].transform.position;
                thisPlate.recipe = new Recipes("PratoFeito");
                mesh.material = material[1]; 
            }

            else if(MacroSistema.sistema.input_Manager.pressed03)
            {
                selector.transform.position = options[2].transform.position;
                thisPlate.recipe = new Recipes("Buchada");
                mesh.material = material[2]; 
            }
           
        
    }
    void SetRecipeOnPlateHUD()
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
            }

    }

}
