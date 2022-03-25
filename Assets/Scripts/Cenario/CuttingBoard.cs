using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CuttingBoard : MonoBehaviour
{
    public Transform Board;
    public float timeToPrepare;
    float timer;
    public Itens _ingre;
    public bool canGetPreparedMeat;

    public bool hasMeat;
    
    
    
    public void Update()
    {

        if(hasMeat == true && canGetPreparedMeat == false)
        {
            Cut();
        }

    }
    
    
    public void Cut()
    {

        Chef.ItenInHand = null;
        Ingredientes meat = _ingre.GetComponent<Ingredientes>();
        
        if(meat.code == "C")
        {
            _ingre.SetPosition(Board.position);
            timer += Time.deltaTime;
            Debug.Log("Cortando");
            if(timer >= timeToPrepare)
            {
                meat.prepared = true;
                Debug.Log("Cortou");
                canGetPreparedMeat = true;
            }
        }

        else
        {
            Debug.Log("Esse ingrediente n rola aqui!!!");///

        }

        
    }


    public Itens GetPreparedMeat()
    {
        if(canGetPreparedMeat == true)
        {
            canGetPreparedMeat = false;
            hasMeat = false;
            return _ingre;
        }

        else
        { 
            Debug.Log("Ainda n pode pegar o Ingrediente");///
            return null;
        }
    
    }

}
