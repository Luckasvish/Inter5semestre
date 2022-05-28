using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public abstract class _Item : MonoBehaviour // CLASSE MÃE DE ITENS.
    {
    public  abstract ItemType type { get; set; }    // Tipo de item.
    public  abstract string itemName { get; set; }  //  Nome do item.

}



