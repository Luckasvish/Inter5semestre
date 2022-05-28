using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TopDownCamera : MonoBehaviour
{
    

    public Transform target;    
    
    public float cameraSpeed;

    public Vector3 offset;

    
    void Update()
    {
        transform.position = Vector3.Lerp( transform.position , target.position + offset , cameraSpeed * Time.deltaTime );
    }
}
