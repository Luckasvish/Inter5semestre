using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Recipes 

{
   internal string itemName { get; set; }
   internal Ingredients[] ingreNeeded = new Ingredients[3]; 
   public int price;
   
      public Recipes(string recipeName)
      {
            Ingredients[] recipe = new Ingredients[3];
            switch(recipeName)
            {
                  case "Feijoada" : recipe[0] = new Ingredients(IngreType._Beans); 
                                    recipe[1] = new Ingredients(IngreType._Beans); 
                                    recipe[2] = new Ingredients(IngreType._Meat); 
                                    itemName = "Feijoada";
                                                                              break;
                  
                  case "PratoFeito":recipe[0] = new Ingredients(IngreType._Beans); 
                                    recipe[1] = new Ingredients(IngreType._Rice); 
                                    recipe[2] = new Ingredients(IngreType._Meat); 
                                    itemName = "PratoFeito";
                                                                              break;
                                                                        
                  
                  case "Buchada":   recipe[0] = new Ingredients(IngreType._Meat); 
                                    recipe[1] = new Ingredients(IngreType._Meat); 
                                    recipe[2] = new Ingredients(IngreType._Farofa); 
                                    itemName = "Buchada";

                                                                              break;
                                                                        
                  default:  Debug.Log("Deu ruim !!");
                                                                        break;
            }
            ingreNeeded = recipe;

      }


}

