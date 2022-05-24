using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement_Manager : MonoBehaviour
{
    internal CharacterController controller;
    public float rotationSpeed;
    float smoothVelocity;
    
    public float speed;
    void Start()
    {
        controller = GetComponent<CharacterController>();
    }



    public void Move(Vector3 moveInput)
    {
        float targetAngle = Mathf.Atan2(moveInput.x, moveInput.z) * Mathf.Rad2Deg;
        float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref smoothVelocity, rotationSpeed);
       
        if(moveInput.magnitude > 0.1f)
        {
            transform.rotation = Quaternion.Euler(0f,angle,0f);
            controller.Move(moveInput * speed * Time.deltaTime);
        }
    }


}
