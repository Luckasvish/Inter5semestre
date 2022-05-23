using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AngerManager : MonoBehaviour
{
    [SerializeField]
    Client myClient;

    [SerializeField]
    int clientAnger;

    int angerToIncrease;

    int starterIrritation;
    int maxIrritation = 30;
    // Start is called before the first frame update
    private void Awake()
    {
        angerToIncrease = myClient.SetStarterIrritation();
    }
    void Start()
    {
        starterIrritation = myClient.RandomizeAngerBar();
        clientAnger = starterIrritation;
    }

    public IEnumerator IncreaseAnger()
    {

        if(clientAnger >= maxIrritation)
        {   
            myClient.ChangeIrritation();
            clientAnger = 0;
        }
        yield return new WaitForSeconds(1);
        clientAnger += angerToIncrease;
        StartCoroutine(IncreaseAnger());
    }
}
