using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;

public class Balcon : _InteractionOBJ
{
    public override InteractableType type { get; set;}

    public override _Item itenOnThis { get; set; }
    public override bool hasItemOnIt {get; set;}
    internal override Material material{get ; set;}  

    
    public Transform itemPosition;    
    internal bool havePlate;


    
        void Awake()
    {
        type = InteractableType._Balcon;

        if(GetComponentInChildren<Plates>() == null)
        {
            itenOnThis = null;
        }
        else
        {
          hasItemOnIt=true;
          itenOnThis = GetComponentInChildren<Plates>();
        }
      
    }
    void Start()
    {
        material = GetComponent<MeshRenderer>().material;
        material.SetFloat("_emission", 4);
    }

    void Update()
    {
      

      if(hasItemOnIt)
      {
        if(itenOnThis.type == ItemType._Plate)
        {
          Plates plate = itenOnThis.GetComponent<Plates>();



        }
      }
    }


    public override void ReceiveItens(_Item itenInHand)
    {
        itenOnThis = itenInHand;
        itenOnThis.transform.position = itemPosition.position;
        RuntimeManager.PlayOneShot("event:/SFX GAMEPLAY/sfx_put");
        hasItemOnIt = true;
    }

    public override _Item GiveItens(_Item itenToGive)//Método para dar o item sobre ele ***precisa de um buffer parar tranfosmar itenOnIt em nulo***
    {
        itenToGive = itenOnThis;
        itenOnThis = null;
        hasItemOnIt = false;
        RuntimeManager.PlayOneShot("event:/SFX GAMEPLAY/sfx_pick");
        return itenToGive;
    }

    // Método para tratar a interação
     public override void Interact(_Item iten, PJ_Character chef)
     {
        if(iten == null)            //    CHECA SE O JOGADOR TEM ITEM NA MÃO OU NÃO 
        {                                   
           if(hasItemOnIt) chef.ReceiveItens(this);     //  PATH #1 : Se o jogador não tiver um item na mão > #1.1 : CHECA SE O BALCÃO TEM ITEM OU NÃO
                                                       //  (se o balcão tiver item) { O jogador recebe o item}

            else Debug.Log("Não tem item aqui!");   ////////  
        } 

        else   //  PATH #2 : Se o jogador tiver um item na mão  > #2.1 : CHECA SE O BALCÃO TEM ITEM OU NÃO
        {
          
          if(hasItemOnIt == false ) ReceiveItens(chef.GiveIten(chef.itenInHand));  //  PATH #3 : Se não houver item no balcão >> O BALCÃO RECEBE O ITEM.
          
          
          else //  PATH #4 : Se o balcão tiver item >>>  CHECA QUAL O TIPO DO ITEM.
          {
              switch(_Item.CheckItemType(itenOnThis))
              {

                      //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////                      
                      case ItemType._Plate: //  #4.1 : NO CASO DO TIPO DO ITEM SER PRATO 

                            if(iten.type == ItemType._Pan)  //  (se o item do jogador for uma panela)
                            {
                                  Pan panX = iten.GetComponent<Pan>();
                                  Plates plate = itenOnThis.GetComponent<Plates>();
                                  InteractPanToPlate(panX, plate);  //  Interage a panela com o prato. 
                            }
          
                            else Debug.Log("Ta fazendo o que com isso aqui ??"); ///////////////////////  (se não for uma panela)

                      break;

                      
                      //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
                      case ItemType._PreparedIngredient: //  #4.2 : NO CASO DO TIPO DO ITEM SER UM INGREDIENTE PREPARADO

                            if(iten.type == ItemType._Pan)    //  (se o item do jogador for uma panela)
                            {     
                                  Pan panY = iten.GetComponent<Pan>();
                                  IngredientInstance ingre = itenOnThis.GetComponent<IngredientInstance>();    
                                  InteractPanToIngre(panY, ingre); // Interage a panela com o ingrediente.
                            }
                            
                            else Debug.Log("Ta fazendo o que com isso aqui ??"); ///////////////////////  (se não for uma panela)

                      break;
                      
                     
                      //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////                      
                      case ItemType._Pan: //  #4.3 : NO CASO DO TIPO DO ITEM SER UMA PANELA
                      Pan panZ = itenOnThis.GetComponent<Pan>();

                            if(iten.type == ItemType._Plate)  //  # 4.31 : (se o item do jogador for um prato)
                            {   
                                  Plates plate = itenOnThis.GetComponent<Plates>();
                                  InteractPanToPlate(panZ, plate);  //  Interage a panela com o prato.
                            }
                            
                            else if (iten.type == ItemType._PreparedIngredient) // # 4.32 :  (se o item do jogador for um prato)
                            {
                                IngredientInstance ingre = itenOnThis.GetComponent<IngredientInstance>();    
                                InteractPanToIngre(panZ, ingre); // Interage a panela com o ingrediente.
                            
                            }
                        
                        break;
                      
                      //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
                        default: Debug.Log("Isso aí não vai rolar não!!!!"); ///////////  //  #4.4 : EM QUALQUER OUTRO CASO 
                        break;
                      
                      //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////                        
            }
          }  
       }
     }
  

    //  Método para interagir a panela com o prato 
    void InteractPanToPlate(Pan pan , Plates plate)
    {

        if(plate != null && pan != null)  
        {

          if(pan.ingredient != null && pan.ready.activeInHierarchy == true) //  (se a panela tiver um ingrediente e estiver pronta)
          {
              if(plate.CheckIngredient(pan.ingredient.itemName) == true) 
              plate.ReceiveIngredient(pan.GiveItem(pan.ingredient.itemName)); //  O prato tenta receber o item da panela.
                                        
              else Debug.Log("Esse ingrediente não entra nessa receita!!!!");///////////////////
                                          
          }

          else if (pan.ingredient != null && pan.ready.activeInHierarchy == false) //  (se a panela tiver um ingrediente mas não estiver pronta)
          Debug.Log("Esse ingrediente não esta pronto ainda !!!!");///////////////////
                                        
          else if(pan.ingredient == null) // (se a panela não tiver ingrediente)
          Debug.Log("Não tem ingrediente nessa panela !!!!"); /////////////////////
                                        
          else  // #2.2 DEAD END *** Se o programa chegar aqui, tem algo de errado !
          Debug.Log("Algo ta muito errado !!!!!"); //////////////////////////

        }  
    }

    //Método para interagir a panela com ingrediente
    void InteractPanToIngre(Pan pan , IngredientInstance ingre)
    {
        if(pan != null && pan.ingredient == null) // (se a panela estiver vazia)                          
        pan.ReceiveItens(ingre);  //  A panela recebe o ingrediente.
            
                                  
        else if (pan != null && pan.ingredient != null)  // (se a panela estiver cheia)
        Debug.Log("Essa panela já está cheia!!!!"); ////////////// 

    }

  }








