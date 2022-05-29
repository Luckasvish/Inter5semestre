using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement_Manager : MonoBehaviour
{
    // COMPONENTES DO GERENTE DE MOVIMENTO 
    internal CharacterController controller;
    /////////////////////////

    // PROPRIEDADES
    float rotationSpeed = 0.01f; //  Velocidade de rotação. *** Padrão : 0.05f (28/05/22)
    float smoothVelocity;   //  Velocidade de suavização da rotação.
    public float speed; //  Velocidade de movimento. *** Padrão : 20 (28/05/22)
    ////////////////////////
    void Start(){ controller = GetComponentInChildren<CharacterController>();}

    public void Move(Vector3 moveInput)
    {
        if(this.controller.isGrounded == false)  //  Se não estiver no chão 
        {
            Vector3 gravity = new Vector3(0, -10, 0); // Vetor pra gravidade.
            this.controller.Move(gravity);   //  Aplicação da gravidade.
        }
     
        else if (this.controller.isGrounded == true)
        {
            float targetAngle = Mathf.Atan2(moveInput.x, moveInput.z) * Mathf.Rad2Deg;  //  Cálculo do ângulo alvo (dado através do input).
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref smoothVelocity, rotationSpeed);   // Cálculo para ser aplicado à rotação.
        
            if(moveInput.magnitude > 0.1f)  // Se houver input
            {
                transform.rotation = Quaternion.Euler(0f,angle,0f); //  Rotaciona
                this.controller.Move(moveInput * speed * Time.deltaTime);    //  Movimenta
            }

        }
        else Debug.Log("Gravidade tá maluka!!!");///////////////
    }

    

}
