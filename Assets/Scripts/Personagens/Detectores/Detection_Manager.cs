using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Detection_Manager : MonoBehaviour
{
    ////////////////////////////////////////////////////////////////////////////
    /// Detection_Manager /////////////////////////
    ////////////////////////////////////////////////////////////////////////////
    /// Interação
    internal Interactable interactable {get; set;}


    ////////////////////////////////////////////////////////////////////////////
    /// Permição para interagir
    internal bool canInteract {get; set;}
    
    
    ////////////////////////////////////////////////////////////////////////////
    /// Detector
    public Transform detectorPosition;  /// Posição.y do detector
    [SerializeField] internal float detectionDistance;  /// Distancia de detecção
   

    ////////////////////////////////////////////////////////////////////////////
    ////////////////////////////////////////////////////////////////////////////
    ////////////////////////////////////////////////////////////////////////////




    public void SetDetection(Interactable interaction)  /// Atribuir detecção  >>> chamada em InteractableDetector
    {
            interactable = interaction;     /// recebe a interação do detector e aloca aqui no manager;
            interactable.ToogleHighLight(true);
            canInteract = true;     /// permite interagir;
    }

    public void ClearDetection()    /// Limpar detecção  >>> chamada em InteractableDetector
    {
        canInteract = false;    /// tira a permição para interagir
        interactable.ToogleHighLight(false);
        interactable = null;    /// anula a referência de interaagível;
    }

}

