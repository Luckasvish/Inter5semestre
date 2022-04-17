using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IngredientInstance : Item
{
    public  override ItemType type { get; set; }
    public  override string itemName { get; set; }
    internal Ingredients ingredient;
    public GameObject[] ingredients_Mesh;
    public void SetMesh(int i)
    {
        

        switch(i)
            {

                case 0:
                     itemName = "Beans";
                     type = ItemType._PreparedIngredient;
                     ingredient = new Ingredients(IngreType._Beans);
                     ingredient.ingreType = IngreType._Beans;
                                                     break;
                case 1:
                     itemName = "Rice";
                     type = ItemType._PreparedIngredient;
                     ingredient = new Ingredients(IngreType._Rice);
                     ingredient.ingreType = IngreType._Rice;
                                                     break;
                                                     
                case 2:
                    itemName = "Meat";
                    type = ItemType._UnpreparedIngredient;
                    ingredient = new Ingredients(IngreType._Meat);
                    ingredient.ingreType = IngreType._Meat;
                                                     break;
                default: itemName = "";  break;
            
            }

            ingredients_Mesh[i].SetActive(true);


    }


}
