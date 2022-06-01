using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IngredientInstance : _Item
{
    public  override ItemType type { get; set; }
    public  override string itemName { get; set; }
    // internal Ingredient ingredient;
    public GameObject[] ingredients_Mesh;
    public void SetMesh(string  ingreName)
    {
        
        switch(ingreName)
            {

                case "Feijao":
                     itemName = "Feijao";
                     type = ItemType._PreparedIngredient;
                     ingredients_Mesh[0].SetActive(true);
                                                     break;
                
                case "Arroz":
                     itemName = "Arroz";
                     type = ItemType._PreparedIngredient;
                    ingredients_Mesh[1].SetActive(true);
                                                     break;
                                                     
                case "Carne":
                    itemName = "Carne";
                    type = ItemType._UnpreparedIngredient;
                  ingredients_Mesh[2].SetActive(true);
                                                     break;

                case "Farofa":
                    itemName = "Farofa";
                    type = ItemType._PreparedIngredient;
                    ingredients_Mesh[3].SetActive(true);
                                                     break;

                                                      
                                                      
                default: itemName = "";  break;
            
            }
   

    }

    public void CutMeat()
    {
        ingredients_Mesh[2].SetActive(false);
        ingredients_Mesh[4].SetActive(true);
        Debug.Log("BifeName: " + itemName);

    }


}
