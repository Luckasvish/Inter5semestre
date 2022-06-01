using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableDetector : MonoBehaviour
{
    /// Interactible Detector /////////////
    private Detection_Manager manager;  ///Manager de detecção;
    public LayerMask InteractableLayer;  ///Camada de Interactable
    Ray detection;  /// Ray castado para detecção

    ///////////////////////////////////////////
    
    void Awake(){  manager = GetComponent<Detection_Manager>();} /////Em Awake(), ele acha o manager;
    
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

            if(interactable.transform.GetComponent<_InteractionOBJ>() != null)                /// (Se não for nula a interação)
            {
                _InteractionOBJ interaction = interactable.transform.GetComponent<_InteractionOBJ>();
                
                if(manager.interactionOBJ != interaction.gameObject)
                {
                    manager.ClearDetection();
                    manager.SetDetection(interaction);
                }
            }
        }
        
        else /// (Se não houver detecção)
        {
            if(manager.interactionOBJ != null) manager.ClearDetection();   /// limpa a detecção;
        }

    }  
    
}
