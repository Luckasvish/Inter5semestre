using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BalconDetector : MonoBehaviour
{
    private Detection_Manager manager;
    public LayerMask BalconLayer;
    

    void Awake()
    {
        //BalconLayer = LayerMask.GetMask("Balcon");
        manager = GetComponent<Detection_Manager>();
    }
    void FixedUpdate()
    {
        BalconDetection();
    }

     void BalconDetection()
    {
        Collider[] detection = Physics.OverlapSphere(transform.position,manager.detectorRadius,BalconLayer); //Juntando as detecções em um Collider[]
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
                    
                    manager.balconOnRange = true;
                    manager.closestBalcon = objDetected[closestDetectionIndex].GetComponent<Balcon>();
                }

                else 
                {   
                    manager.balconOnRange = false;
                    manager.closestBalcon = null;
                } 
    }  
}
