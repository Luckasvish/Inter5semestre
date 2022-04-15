using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ingredients : Item
{
    public override string itemName { get; set;}
    public override ItemType type { get; set;}
    public GameObject[] ingredients_Mesh;

    public void SpawnBeans()
    {

               ingredients_Mesh[0].SetActive(true);
               ingredients_Mesh[1].SetActive(false);
               ingredients_Mesh[2].SetActive(false);
               type = ItemType._PreparedIngredient;
               itemName = "Beans";
    }


                
    public void SpawnRice(){

                ingredients_Mesh[0].SetActive(false);
                ingredients_Mesh[1].SetActive(true);
                ingredients_Mesh[2].SetActive(false); 
                type = ItemType._PreparedIngredient;
                itemName = "Rice";
    }
    

    public void SpawnMeat(){

                ingredients_Mesh[0].SetActive(false);
                ingredients_Mesh[1].SetActive(false);
                ingredients_Mesh[2].SetActive(true); 
                type = ItemType._UnpreparedIngredient;
                itemName = "Meat";
    }
    
}
