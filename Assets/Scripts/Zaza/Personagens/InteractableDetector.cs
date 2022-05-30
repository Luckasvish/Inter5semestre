using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableDetector : MonoBehaviour
{

    ////////////////////////////////////////////////////////////////////////////////////////
    /// Interactible Detector /////////////////////////
    ///////////////////////////////////////////////////////////////////////////////////////
    private Detection_Manager manager;  ///Manager de detecção;
    public LayerMask InteractableLayer;  ///Camada de Interactable
    Ray detection;  /// Ray castado para detecção

    ///////////////////////////////////////////////////////////////////////////////////////
    ///////////////////////////////////////////////////////////////////////////////////////
    
    void Awake(){   manager = GetComponent<Detection_Manager>();} /////Em Awake(), ele acha o manager;
    
    void FixedUpdate()
    { 
        InteractionDetection(); //////// Detecta as interações em FixedUpdate();
    }

     void InteractionDetection()
    {
        detection = new Ray(manager.detectorPosition.position, transform.forward); /// Cria o raio;
       

        RaycastHit interactable;    /// criando um retorno;
        
        if(Physics.Raycast(detection, out interactable, manager.detectionDistance ,InteractableLayer)) /// (Se tiver uma detecção)
        {
//             Debug.Log("Ta rolando detecção!"); 

            if(interactable.transform.GetComponent<_InteractionOBJ>() != null)                /// (Se não for nula a interação)
            {
                manager.SetDetection(interactable.transform.GetComponent<_InteractionOBJ>());  /// retorna a nova interação ao manager;
    //            Debug.Log("Ta tendo interação : " + interactable.transform.GetComponent<_InteractionOBJ>() ); ///////////
                Gizmos.color = Color.green;
            
            }

            else /// (Se a detecção for nula)
            {
  //              Debug.Log("Detecção de Interação Nula !!!");//////////////////////////////////////////////////////// Debuga ****** AQUI PODE VIR UM FEEDBACK DE FALA 
               
                Gizmos.color = Color.red;
            }
        }
        else /// (Se não houver detecção)
        {
//            Debug.Log("Não ta rolando detecção!"); 
            if(manager.interactionOBJ != null)    /// (Se houver um interagível em manager)
            {
                manager.ClearDetection();   /// limpa a detecção;
            }
            
        }

    }  

    void OnDrawGizmos()
    {
        Gizmos.DrawRay(detection);
    }
    
}
