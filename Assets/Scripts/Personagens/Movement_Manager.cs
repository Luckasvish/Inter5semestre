using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement_Manager : MonoBehaviour
{
    internal CharacterController controller;
    public float rotationSpeed;
    
    public float speed;
    void Start()
    {
        controller = GetComponent<CharacterController>();
    }



    public void Move(Vector3 moveInput)
    {
        moveInput = new Vector3 ( moveInput.x * Time.deltaTime * speed, moveInput.y , moveInput.z * Time.deltaTime * speed);
    
        //Quaternion direction = Quaternion.LookRotation(moveInput, Vector3.up);

        if(moveInput.x != 0 || moveInput.z != 0)
        {
        
            transform.forward = Vector3.Lerp(transform.forward, moveInput,0.9f);
        
        
        }

        controller.Move(moveInput);
    }


}
