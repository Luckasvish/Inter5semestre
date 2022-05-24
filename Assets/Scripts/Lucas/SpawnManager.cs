using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public static SpawnManager instance;
    [SerializeField]
    internal GameObject Spawn;
    [SerializeField]
    GameObject Client;

    int instanceTimer;

    [SerializeField]
    int maxClientsPerLevel;
    [SerializeField]
    int clientsInScene;

    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Spawner());
    }

    // Update is called once per frame

    IEnumerator Spawner()
    {
        if(clientsInScene >= maxClientsPerLevel)
        {
            yield return new WaitForSeconds(3);
            StartCoroutine(Spawner());
            yield break;
        }

        yield return new WaitForSeconds(3);
        Instantiate(Client, Spawn.transform.position, Quaternion.identity);
        ChangeClientsNumber(+1);
        StartCoroutine(Spawner());
    }

    public int GetMaxClientPerLevel()
    {
        return maxClientsPerLevel;
    }
    public int GetClientsNumber()
    {
        return maxClientsPerLevel;
    }

    public void ChangeClientsNumber(int increaseClientNumber)
    {
        clientsInScene += increaseClientNumber;
    }
}
