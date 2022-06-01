using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;
using FMOD.Studio;
public class MacroSistema : MonoBehaviour   // GERENTE PRINCIPAL DO ZAZA.
{
    public static MacroSistema sistema; //  Instância estática para referência do gerente.
    public IngredientInstance[] IngredientsInstance = new IngredientInstance[5];    //  Instâncias de ingredientes. 
    public static string[] staticRecipes;   //  Nomes estáticos das receitas.

    int[] counter = new int[5];  // Contador pra gerenciar o spawn dos ingredientes.  

    [SerializeField] internal Input_Manager input_Manager; // Referência pro gerente principal de Inputs.
  
     EventInstance soundTrack;   // Instância da trilha sonora. 
     EventInstance soundScape;  //  Intância da ambiência sonora.

    void Awake()
    {
        if (sistema == null)
        {
            sistema = this;
        }

        staticRecipes = new string[3];

        staticRecipes[0] = "Feijoada";
         staticRecipes[1] = "PratoFeito";
          staticRecipes[2] = "Buchada";


        foreach(_Item i in IngredientsInstance)
        {
            i.gameObject.SetActive(false);
        }

        


    }

    void Start()
    {
    
        for (int i = 0; i < IngredientsInstance.Length; i++)
        { 
            counter[i]= 0;
        }
        
        soundTrack = RuntimeManager.CreateInstance("event:/MUSIC/music_gameplay");
        soundScape = RuntimeManager.CreateInstance("event:/SOUNDSCAPE/soudscape_restaurante");
        soundScape.start();
       // soundTrack.start();

    }
    
    public string GetPossibleRecipes(int recipeIndex) // Método pra pegar uma receita das possíveis (Chamado pelo cliente).  
    {
        return staticRecipes[recipeIndex];
    }
    

    public _Item SpawnIngredient()  // Método que spawna os ingredientes.
    {
       bool spawned = false;
        int index = 0;
       
        for( int i = 0; i < counter.Length; i++)
        {
            if(counter[i] == 0 && spawned == false)
            {
                counter[i] = 1;
                spawned = true;
                index = i;
            }
        }

        if(index >= 5)
        {
            Debug.Log("Já atingiu o limite de itens!!!");
            return null;
        }
        else 
        {
            IngredientsInstance[index].gameObject.SetActive(true);
            return IngredientsInstance[index];
        }
    }

}
