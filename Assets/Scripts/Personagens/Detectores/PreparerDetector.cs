using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PreparerDetector : MonoBehaviour
{
    private Detection_Manager manager;
    public LayerMask PreparerLayer;
    
    void Awake()
    {
        //PreparerLayer = LayerMask.GetMask("Preparer");
        manager = GetComponent<Detection_Manager>();
    }
    void FixedUpdate()
    {
        PreparerDetection();
    }

    void PreparerDetection()
    {
        Collider[] detection = Physics.OverlapSphere(transform.position,manager.detectorRadius,PreparerLayer); //Juntando as detecções em um Collider[]
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
                    
                    manager.preparerOnRange = true;
                    manager.closestPreparer = objDetected[closestDetectionIndex].GetComponent<Preparer>();
                }

                else 
                {   
                    manager.preparerOnRange = false;
                     manager.closestPreparer = null;
                }   
    }
}