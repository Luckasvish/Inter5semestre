using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MacroSistema : MonoBehaviour
{
   public static MacroSistema sistema;
   public Ingredientes[] Ingredientes = new Ingredientes[5];
   public Recipes[] Recipes = new Recipes[5];

   [SerializeField] internal Input_Manager input_Manager; ///Sistema de Inputs.
  



    void Awake()
    {
        if (sistema == null)
        {
            sistema = this;
        }

        foreach(Itens i in Ingredientes)
        {
            i.gameObject.SetActive(false);
        }
      
         foreach(Itens i in Recipes)
        {
            i.gameObject.SetActive(false);
        }
      
    }
}
