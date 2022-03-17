using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PJ_Controller : MonoBehaviour
{

    //COMPONENTES
    Rigidbody rgbody;

    //Movimentação
    public bool canMove;
    public float moveSpeed;
    float xInput;
    float zInput;

    //TROCA DE PERSONAGEM
    public string personName;
    bool isActive;

    void Start()
    {
        rgbody = GetComponent<Rigidbody>();
    }

    
    void Update()
    {   
        
        xInput = Input.GetAxis("Horizontal")* moveSpeed;
        zInput = Input.GetAxis("Vertical")* moveSpeed;


        if(this.personName == Controlador.activePerson)
        {
            this.isActive = true;
        }
        else this.isActive = false;
        
    }



    void FixedUpdate()
    {
        if(canMove && isActive)
        {
            rgbody.velocity = new Vector3( xInput , 0 ,zInput);
        }
        else rgbody.velocity = new Vector3(0,0,0);  
    
    }

}
