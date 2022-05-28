using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu ( menuName = "Obj_Spawner" , fileName =  "New_Obj_Spawner")]
public class _OBJSpawner : ScriptableObject //  SPAWNER DE OBJETOS. 
{
    public GameObject[] objects;    //  Objetos para serem spawnados ( meshs a serem escolhidas ).


    public void SpawnObj(string _objName)
    {

        foreach(GameObject obj in objects)
        {
            if(obj.name == _objName)
            {
                obj.GetComponentInParent<Transform>().gameObject.SetActive(true);
                obj.SetActive(true);
            }

        }

    }



}
