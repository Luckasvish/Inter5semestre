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
        else if (clientsInScene > (maxClientsPerLevel * (3 / 4)) + (maxClientsPerLevel * 1/ 8))
            yield return new WaitForSeconds(Random.RandomRange(25, 30));
        else if(clientsInScene > maxClientsPerLevel*(3/4))
            yield return new WaitForSeconds(Random.RandomRange(10, 25));
        else if(clientsInScene > maxClientsPerLevel * (1 / 4))
            yield return new WaitForSeconds(Random.RandomRange(6, 16));
        else
            yield return new WaitForSeconds(Random.RandomRange(4, 10));
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
