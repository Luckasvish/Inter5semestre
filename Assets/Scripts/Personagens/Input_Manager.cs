using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Input_Manager : MonoBehaviour
{
    internal bool pressedE;
    internal Vector3 moveInput;
    internal bool pressed01;
    internal bool pressed02;
    internal bool pressed03;
    void Update()
    {
       pressedE =  Input.GetKeyDown(KeyCode.E);
       pressed01 = Input.GetKeyDown(KeyCode.Alpha1);
       pressed02 = Input.GetKeyDown(KeyCode.Alpha2);
       pressed03 = Input.GetKeyDown(KeyCode.Alpha3);
       moveInput = MovementInput();
    }


    Vector3 MovementInput()
    {
        Vector3 input = new Vector3 (Input.GetAxis("Horizontal"),0,Input.GetAxis("Vertical"));
        return input;
    }



}
