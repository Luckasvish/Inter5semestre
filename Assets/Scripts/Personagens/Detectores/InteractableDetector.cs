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
    void FixedUpdate(){ InteractionDetection();}///////// Detecta as interações em FixedUpdate();

     void InteractionDetection()
    {
        detection = new Ray( new Vector3 (transform.position.x, manager.detectorPosition.position.y,transform.position.z) , transform.forward); /// Cria o raio;
        RaycastHit interactable;    /// criando um retorno;
        
        if(Physics.Raycast(detection, out interactable, manager.detectionDistance ,InteractableLayer)) /// (Se tiver uma detecção)
        {
            if(interactable.transform.GetComponent<Interactable>() != null)                /// (Se não for nula a interação)
            {
                if(manager.interactable == null)    /// (Se ainda não houver um interagível no manager)
                {
                    manager.SetDetection(interactable.transform.GetComponent<Interactable>());  /// retorna a interação ao manager;
                }
                else    /// (Se já houver um interagível em manager)
                {
                    manager.SetDetection(interactable.transform.GetComponent<Interactable>());  /// retorna a nova interação ao manager;
                }

            }
            else /// (Se a detecção for nula)
            {
                Debug.Log("Detecção de Interação Nula !!!");//////////////////////////////////////////////////////// Debuga ****** AQUI PODE VIR UM FEEDBACK DE FALA 
            }
        }
        else /// (Se não houver detecção)
        {
            if(manager.interactable != null)    /// (Se houver um interagível em manager)
            {
                manager.ClearDetection();   /// limpa a detecção;
            }
            
        }
    }  
    
}
