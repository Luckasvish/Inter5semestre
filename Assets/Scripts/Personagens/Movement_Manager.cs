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
    

        Quaternion direction = Quaternion.LookRotation(moveInput, Vector3.up);

        transform.rotation = direction;

        controller.Move(moveInput);
    }


}
