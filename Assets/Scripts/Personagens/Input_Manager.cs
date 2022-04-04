using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Input_Manager : MonoBehaviour
{
    internal bool pressedE;
    internal Vector3 moveInput;
    void Update()
    {
       pressedE =  Input.GetKeyDown(KeyCode.E);
       moveInput = MovementInput();
    }


    Vector3 MovementInput()
    {
        Vector3 input = new Vector3 (Input.GetAxis("Horizontal"),0,Input.GetAxis("Vertical"));
        return input;
    }

}
