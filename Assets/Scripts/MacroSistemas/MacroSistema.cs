using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MacroSistema : MonoBehaviour
{
   public static MacroSistema sistema;
   public Itens[] beans = new Itens[5];
   public Itens[] rice = new Itens[5];
   public Itens[] meat = new Itens[5];

    void Awake()
    {
        if (sistema == null)
        {
            sistema = this;
        }
    }
}
