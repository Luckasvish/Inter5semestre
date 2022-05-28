using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class OrderHUD : MonoBehaviour
{
    public Image[] recipeOrder ;

    MacroSistema macro;

    void Start()
    {
        macro = MacroSistema.sistema;
    }


    internal void SetReciepOrderHUD(string recipeName)
    {

        switch(recipeName)
        {
            case "Feijoada":    recipeOrder[0].gameObject.SetActive(true);
                                recipeOrder[1].gameObject.SetActive(false);
                                recipeOrder[2].gameObject.SetActive(false);


                             break;
            
            case "PratoFeito":   recipeOrder[0].gameObject.SetActive(false);
                                recipeOrder[1].gameObject.SetActive(true);
                                recipeOrder[2].gameObject.SetActive(false);
            
                            break;
            
            case "Buchada":  recipeOrder[0].gameObject.SetActive(false);
                                recipeOrder[1].gameObject.SetActive(false);
                                recipeOrder[2].gameObject.SetActive(true);
                            break;
        
        }




    }

}
