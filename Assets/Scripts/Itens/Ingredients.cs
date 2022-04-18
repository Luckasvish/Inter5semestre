using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ingredients
{
    internal IngreType ingreType;


    public Ingredients(IngreType type)
    {
        ingreType = type;
    }




}

public enum IngreType 
{
    _Beans,
    _Meat,
    _Rice,
    _Farofa

}
