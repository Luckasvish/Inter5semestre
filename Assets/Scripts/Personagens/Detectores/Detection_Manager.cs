using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Detection_Manager : MonoBehaviour
{
    public float detectorRadius;

    ///---PREPARER---
    internal Preparer closestPreparer;
    internal bool preparerOnRange;
    ///---STORER---
    internal Storers closestStorer;
    internal bool storerOnRange;
    ///---BALCON---
    internal Balcon closestBalcon;
    internal bool balconOnRange;
    ///---OVEN---
    internal Oven closestOven;
    internal bool ovenOnRange;


    void OnDrawGizmos()
    {

        if (storerOnRange){ Gizmos.color = Color.yellow;}
        
        else if (ovenOnRange){Gizmos.color = Color.red;;}
        
        else if (preparerOnRange){Gizmos.color = Color.green;}
        
        else if (balconOnRange){Gizmos.color = Color.blue;}

        else Gizmos.color = Color.white;

        Gizmos.DrawWireSphere(transform.position,detectorRadius);
    }
}
