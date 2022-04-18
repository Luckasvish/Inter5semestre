using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Oven : Interactable
{
    public override InteractableType type { get; set;}
    FeedBackManager feedback {get;set;}
    
    public override Item itenItHas { get; set; }
    public override bool hasItemOnIt {get; set;}
    public  GameObject highLight;
    public override bool highLightOn {get; set;}

    public Transform PanPosition;

    public Pan _Pan;

    void Awake()
    {
        type = InteractableType._Oven;
        highLight.SetActive(false);
        
    }

    void Update()
    {
        if(highLightOn)
      {
        highLight.SetActive(true);
      }
      else 
      {
        highLight.SetActive(false);
      }
    }
    
    void Start()
    {
        itenItHas = _Pan;
        itenItHas.transform.position = PanPosition.position;
        hasItemOnIt = true;
    }


    public override Item GiveItens(Item Buffer)
    {
       
            Buffer = itenItHas;
            itenItHas = null;
            hasItemOnIt = false;
            _Pan.onOven = false;
            _Pan = null;
            return Buffer;
       
    }

    public override void ReceiveItens(Item Pan)
    {
                _Pan = Pan.GetComponent<Pan>();
                itenItHas = _Pan;
                _Pan.onOven = true;
                itenItHas.transform.position = PanPosition.position;
                hasItemOnIt = true;
       
    }
    
}
