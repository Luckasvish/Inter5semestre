using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Detection_Manager : MonoBehaviour
{
    //  COMPONENTE DO GERENTE DE DETECÇÃO
    internal _InteractionOBJ interactionOBJ {get; set;} //  Objeto interagível.
    ////////////////////

    // PROPRIEDADES DA DETECÇÃO
    internal bool canInteract {get; set;}   //  booleana para determinar a possibilidade de interação.
    public Transform detectorPosition;  // Posição.y do detector.
    [SerializeField] internal float detectionDistance;  // Distancia de detecção.
    internal bool preparerInteract; //  booleana para interação com o preparador.

    bool highlighted;
    ////////////////////////////

    //  PROPRIEDADES DO HIGHLIGHT
    float glowTimer = 1;    //  Temporizador
    public float glowSpeed;  //   Tempo
    bool glowing = false;   //  booleana para dizer se está ligado ou não o Highlight.
    ////////////////////////////

    // HUD DE PLATES
  
    ///////////////////////////

    void Update()
    {
        if(canInteract) //  (se puder interagir)
        {
            CheckBalconForPlate(interactionOBJ,true);

            if(highlighted == false)
            {  
                BlinkDetection(interactionOBJ);
            }
            preparerInteract = CheckPreparer(interactionOBJ);
            
            Glow(); // Highlight()


        }
    }


    void BlinkDetection(_InteractionOBJ interaction)
    {
        interaction.blinking = true;
        interaction.blinkTimer = 0;
        highlighted = true;
    }


    // Função para atribuir uma detecção >>> chamada em InteractableDetector
    public void SetDetection(_InteractionOBJ interaction)
    {
            interactionOBJ = interaction; // recebe a interação do detector e aloca aqui no manager;
            canInteract = true;     // permite interagir;
    }

    // Método para iluminar o objeto para HighLight
    void Glow()
    {
        if(glowing == false) // (se não estiver brilhando)
        {
            glowTimer += Time.deltaTime * glowSpeed; // O brilho vai deminuindo -- Quanto maior o valor do parâmentro menor é o brilho.

            if(glowTimer >= 4) glowing = true; //  (e o brilho chegar a 4) {ele volta a brilhar}.

        }
        else    //  (se estiver brilhando)
        {
            glowTimer -= Time.deltaTime * glowSpeed; //  O brilho vai aumentando  -- Quanto menor o valor do parâmentro maior é o brilho.
            if(glowTimer <= 1) glowing = false; //  (se po brilho chegar a 1) {ele volta a deixar de brilhar}.
        }

//        interactionOBJ.material.SetFloat("_emission", glowTimer);   //  Seta o brilho no Shader.
    }

    bool CheckPreparer(_InteractionOBJ interaction) {return (interaction.GetComponent<Preparer>() != null)? true: false;}

    public Preparer GetPreparer( ){return interactionOBJ.GetComponent<Preparer>();}

    void CheckBalconForPlate(_InteractionOBJ interaction , bool setActive)
    {
        Balcon b = interaction.GetComponent<Balcon>();

        if(b != null) if(b.gotPlate) b.plateOnThis.hud.SetActive(setActive);
            
        else return;

    }


  

    // Método para anular a detecção. >>> chamada em InteractableDetector
    public void ClearDetection()
    {
        if(interactionOBJ != null)
        {
            CheckBalconForPlate(interactionOBJ , false);
            if(highlighted == true) highlighted = false;
            
            interactionOBJ = null;    /// Anula a referência de interaagível;
        }

            canInteract = false;    // Tira a permição para interagir.

    }

}

