using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StorersDetector : MonoBehaviour
{
    private Detection_Manager manager;
    public LayerMask StorerLayer;

    void Awake()
    {
       // StorerLayer = LayerMask.GetMask("Storer");
        manager = GetComponent<Detection_Manager>();
    }
    void FixedUpdate()
    {
        StorerDetection();
    }

    void StorerDetection()
    {
        Collider[] detection = Physics.OverlapSphere(transform.position,manager.detectorRadius,StorerLayer); //Juntando as detecções em um Collider[]
        List<GameObject> objDetected = new List<GameObject>();  //Criando uma lista de GameObjects
        int closestDetectionIndex = 0; //Detector por index

                if(detection.Length > 0) //SE as detecções forem maior que 0 
                {
                    foreach(Collider c in detection) //PRACADA collider em  nas colisões 
                    {
                        objDetected.Add(c.gameObject); //Add o collider.gameObject pra lista
                    }
                
                    float closeItemDistance = Vector3.Distance(transform.position,objDetected[0].transform.position); //Criando uma distancia até o item mais próximo.

                    for(int i = 0; i < objDetected.Count ; i ++)// Pra toda a contagem de Colliders na lista
                    {
                        if(Vector3.Distance(transform.position, objDetected[0].transform.position) < closeItemDistance)//SE a distancia for menor que a distância do item mais próximo
                        {
                            closestDetectionIndex = i; // O index do item mais próximo se torna i
                        }
                    }
                    
                    manager.storerOnRange = true;
                    manager.closestStorer = objDetected[closestDetectionIndex].GetComponent<Storers>();
                }

                else 
                {   
                    manager.storerOnRange = false;
                    manager.closestStorer  = null;
                }   
    }
}
