using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Detection_Manager : MonoBehaviour
{
    public float detectorRadius;
    internal Interactable interactable;
    internal bool canInteract;  
    internal InteractableType type;

    void OnDrawGizmos()
    {
        
        if (canInteract)
        {
            Gizmos.color = Color.blue;
            interactable.feedback.ToogleHighLight();
        }
        else Gizmos.color = Color.white;
        Gizmos.DrawWireSphere(transform.position,detectorRadius);
    }
}

