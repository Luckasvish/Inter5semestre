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
    ////////////////////////////

    //  PROPRIEDADES DO HIGHLIGHT
    float glowTimer = 1;    //  Temporizador 
    public float glowSpeed;  //   Tempo
    bool glowing = false;   //  booleana para dizer se está ligado ou não o Highlight.
    ////////////////////////////

    // HUD DE PLATES
    bool hudOn;  // booleana para ajudar a lidar com a HUD do prato
    ///////////////////////////

    void Update()
    {
        if(canInteract) //  (se puder interagir)
        { 
            if(hudOn == false) 
            { 
                ToogleePlateHUD(interactionOBJ); // (se a HUD do PRATO estiver desligada) { Lida com a HUD()} 
            }
            else if (hudOn == true)
            {


            }


            CheckPreparer(interactionOBJ);
            Glow(); // Highlight()
        }
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

        interactionOBJ.material.SetFloat("_emission", glowTimer);   //  Seta o brilho no Shader.
    }

    void CheckPreparer(_InteractionOBJ interaction)
    {
        Preparer p = interaction.GetComponent<Preparer>();
        
        if(p != null) preparerInteract = true;
        
        else preparerInteract = false;
    }

    public Preparer GetPreparer( ){return interactionOBJ.GetComponent<Preparer>();}


    // Método para lidar com a HUD de pratos. 
    void ToogleePlateHUD(_InteractionOBJ interaction)    
    {
        if (interaction.GetComponent<Balcon>() != null) // (se tiver um balcao )
        {

            if(interaction.hasItemOnIt == true) //  (se o balcão tiver item)
            {
                Plates p = interaction.itenOnThis.GetComponent<Plates>();    //  Tenta achar um prato.
                
                if(p != null)   //  (se achar um prato)
                {
                    if( p.recipe == null || p.recipe.ingreNeeded.Count > 0)
                    {

                        if(hudOn == true)   // (se a hud estiver ativa)
                        {
                            p.hud.SetActive(false); //  Desativa a HUD()
                            hudOn = false;
                        }

                        else // (se a huda estiver desativada)
                        {
                            p.hud.SetActive(true);  //  Ativa a HUD()
                            hudOn = true;
                        }
                    }
                 
                }
            }
        }
    }


    // Método para anular a detecção. >>> chamada em InteractableDetector
    public void ClearDetection()  
    {
        if(hudOn == true)   ToogleePlateHUD(interactionOBJ); // (se a HUD do prato estiver ligada) {Lida com a HUD()}

        canInteract = false;    // Tira a permição para interagir.
        interactionOBJ.material.SetFloat("_emission", 4);   // Diminui o brilho do HighLight. 
        interactionOBJ = null;    /// Anula a referência de interaagível;
    }

}

