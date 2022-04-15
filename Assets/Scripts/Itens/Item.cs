using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public abstract class Item : MonoBehaviour
    {
    public  abstract ItemType type { get; set; }
    public  abstract string itemName { get; set; }

}
public enum ItemType 
{
    _UnpreparedIngredient,
    _PreparedIngredient,
    _Recipe,
    _Plate,
    _Glass,
    _EmptyGlass,
    _Pan
}


