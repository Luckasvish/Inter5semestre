using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Detection_Manager : MonoBehaviour
{
    public float detectorRadius;

    ///---BALCON---
    internal Interagibles interagible;
   
    internal bool canInteract;
  

    void OnDrawGizmos()
    {
        
        if (canInteract){Gizmos.color = Color.blue;}

        else Gizmos.color = Color.white;

        Gizmos.DrawWireSphere(transform.position,detectorRadius);
    }
}
