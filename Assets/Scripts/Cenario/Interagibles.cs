using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Interagibles : MonoBehaviour
{
    [SerializeField]
    public  abstract InteragibleType type { get; set; }
    public abstract Itens itenItHas {get;set;}
    public abstract bool hasItemOnIt {get;set;}

    public abstract Itens GiveItens(Itens itenToGive);
    public abstract void ReceiveItens(Itens itenReceived);
    
}

public enum InteragibleType 
{
    _Oven,
    _Storer,
    _Preparer,
    _Balcon,
}
