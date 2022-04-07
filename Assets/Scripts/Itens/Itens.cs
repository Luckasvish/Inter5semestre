using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Itens : MonoBehaviour
    {
    public  abstract ItenType type { get; set; }
    public  abstract string itemName { get; set; }

}
public enum ItenType 
{
    _UnpreparedIngredient,
    _PreparedIngredient,
    _Recipe,
    _Plate,
    _Glass,
    _Glass00,
    _Pan
}


