using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public abstract class _Item : MonoBehaviour // CLASSE MÃE DE ITENS.
    {
    public  abstract ItemType type { get; set; }    // Tipo de item.
    public  abstract string itemName { get; set; }  //  Nome do item.

    public static ItemType CheckItemType(_Item item){ return item.type;}    //  Método estático pra checar o tipo de um item. 
    

}



