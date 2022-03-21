using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Storers : MonoBehaviour
{
    public char code;
   
    
    public Itens GiveIngredient(char code)
    {
        int counter = 0;

        switch(code)
        {   
            case 'A':   
                Debug.Log("Chegooou!!A");///
                foreach( Itens rice in MacroSistema.sistema.rice)
                {
                 
                    if (rice.gameObject.activeInHierarchy)
                    { 
                        counter ++;
                    }

                }
             
                Itens Rice =  MacroSistema.sistema.rice[counter];
                Debug.Log(Rice);///
                Rice.gameObject.SetActive(true);
                return Rice;    
            


            case 'F':   
                Debug.Log("Chegooou!!F");///
                
                foreach( Itens beans in MacroSistema.sistema.beans)
                {
                 
                    if (beans.gameObject.activeInHierarchy)
                    { 
                        counter ++;
                    }

                }
                
                Itens Beans =  MacroSistema.sistema.beans[counter];
                Debug.Log(Beans);///
                
                Beans.gameObject.SetActive(true);
                
                return Beans;    
            
            case 'C':    
                Debug.Log("Chegooou!!C");///
                
                foreach( Itens meat in MacroSistema.sistema.meat)
                {
                 
                    if (meat.gameObject.activeInHierarchy)
                    { 
                        counter ++;
                    }

                }
                
                Itens Meat =  MacroSistema.sistema.meat[counter];
                Debug.Log(Meat);///
                
                Meat.gameObject.SetActive(true);
                
                return Meat;   
        
            
            default : 
                Debug.Log("Chegooou!!!NoDefault");///
                
                return null;
        
        }

        


    }
}
