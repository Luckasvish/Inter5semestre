using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Storers : MonoBehaviour
{
    public char code;
    public Transform chefsHands;
    
    public void GiveIngredient(char code)
    {
        int counter = 0;

        switch(code)
        {   

            case 'A':   foreach( Itens rice in MacroSistema.sistema.rice)
            {
                 
                 if (rice.gameObject.activeInHierarchy)
                 { counter ++;}

            }
                MacroSistema.sistema.rice[counter].gameObject.SetActive(true);

                MacroSistema.sistema.rice[counter].SetPosition(chefsHands);
                
            break;



            case 'F':   foreach( Itens beans in MacroSistema.sistema.beans)
            {
                 
                 if (beans.gameObject.activeInHierarchy)
                 { counter ++;}

            }
                MacroSistema.sistema.beans[counter].gameObject.SetActive(true);

                MacroSistema.sistema.beans[counter].SetPosition(chefsHands);

            break;



            case 'C':   foreach( Itens meat in MacroSistema.sistema.meat)
            {
                 
                 if (meat.gameObject.activeInHierarchy)
                 { counter ++;}

            }
                MacroSistema.sistema.meat[counter].gameObject.SetActive(true);
                MacroSistema.sistema.meat[counter].SetPosition(chefsHands);

            break;





        }
    }
}
