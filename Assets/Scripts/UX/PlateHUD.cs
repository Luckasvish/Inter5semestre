using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlateHUD : MonoBehaviour
{
    internal Plates thisPlate;
    public Image[] options;
    public Image selector;

    internal MeshRenderer mesh;
    public Material[] material; 


    bool settle;


    void Awake()
    {
        thisPlate = GetComponentInParent<Plates>();
        mesh = thisPlate.GetComponent<MeshRenderer>();
    }




    void Update()
    {
        if(this.gameObject.activeInHierarchy)
        {
            Run(settle);
        }
    }
  

    void Run(bool recipeSettled)
    {
        if(recipeSettled == false)
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

    }

}
