using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Oven : MonoBehaviour
{
    public Pan Pan;
    public bool hasPan = true;

    public Transform PanPosition;

    void Awake()
    {
        Pan.transform.position = PanPosition.position;
        hasPan = true;
    }

    public Itens GivePan(Itens _Pan)
    {
        if(hasPan)
        {
            _Pan = Pan;
            Pan = null;
            hasPan = false;
            return _Pan;
        }
        else 
        {
            Debug.Log("Ta sem a panela!");///
            return null;
        }

    }
    public void ReceivePan(Itens _Pan)
    {
        Pan = _Pan.GetComponent<Pan>();

        if(Pan != null)
        {
            Pan.transform.position = PanPosition.position;
            hasPan = true;
        }
        else 
        {
            Debug.Log("Isso não é uma panela : " + _Pan);///
        }

    }
}
