using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Input_Manager : MonoBehaviour
{
    internal bool pressedE => Input.GetKeyDown(KeyCode.E);
    internal bool pressedQ => Input.GetKeyDown(KeyCode.Q);
    internal bool holdE => Input.GetKey(KeyCode.E);
    internal Vector3 moveInput => MovementInput();
    internal bool pressed01 => Input.GetKeyDown(KeyCode.Alpha1);
    internal bool pressed02 => Input.GetKeyDown(KeyCode.Alpha2);
    internal bool pressed03 => Input.GetKeyDown(KeyCode.Alpha3);
    internal bool pressedSpace => Input.GetKeyDown(KeyCode.Space);
  
    //Método que retorna o Vetor inputado.
    Vector3 MovementInput()
    {
        float hInput = Input.GetAxisRaw("Horizontal");          float vInput = Input.GetAxisRaw("Vertical");
       
        Vector3 input = new Vector3 (hInput * Camera.main.transform.forward.z,0,vInput * Camera.main.transform.forward.z).normalized;
       
        return input;
    }



}
