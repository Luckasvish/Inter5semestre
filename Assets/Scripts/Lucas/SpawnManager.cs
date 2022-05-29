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

    [SerializeField]
    int maxClientsPerLevel, clientsInScene;

    [SerializeField]
    int atualWave;
    int waveSpawner;

    public int timeIndex;

    public float timeBetweenSpawn;
    [SerializeField]
    private float counterSpawn;

    public float timeBetweenWave;
    private float counterWave;

    private bool endWave = false;
    public static bool endNivel = false;
    public Nivel nivel;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        timeBetweenSpawn = nivel.countdownBetweenWaves[timeIndex];
        atualWave = 0;
        waveSpawner = 0;
        counterSpawn = 0;
        counterWave = 0;
    }

    private void Update()
    {

                    //Debug.Log("counterSpawn = " + counterSpawn);
                    //Debug.Log("atualWave = " + atualWave);
                    //Debug.Log(" nivel.waves.Length = " + nivel.waves.Length);

 
        if (endWave)
        {
            if (counterWave >= timeBetweenWave)
            {
                counterWave = 0;
                endWave = false;
                counterSpawn = 0;
            }
            else
            {
                counterWave += Time.deltaTime;
            }
        }
        else
        {
            counterSpawn += Time.deltaTime;
        }

        if (counterSpawn >= timeBetweenSpawn && !endWave)
        {
            //Debug.Log("--------------1--------------");
            if (atualWave < nivel.waves.Length)
            {
                //Debug.Log("--------------2--------------");
                if (waveSpawner < nivel.waves[atualWave].enemyToSpawn.Length)
                {
                    //Debug.Log("--------------3--------------");
                    switch (nivel.waves[atualWave].enemyToSpawn[waveSpawner])
                    {
                        case 0:
                            SpawnEnemyPath(IBehaviour.BehaviourType.Calm);
                            break;
                        case 1:
                            SpawnEnemyPath(IBehaviour.BehaviourType.Impatient);
                            break;
                        case 2:
                            SpawnEnemyPath(IBehaviour.BehaviourType.Angry);
                            break;

                    }
                    if(waveSpawner < nivel.waves[atualWave].enemyToSpawn.Length)
                        waveSpawner += 1;

                    timeBetweenSpawn = nivel.waves[atualWave].countdownBetweenSpawn[waveSpawner];
                }
                else
                {
                    //Debug.Log("--------------4--------------");
                    waveSpawner = 0;
                    atualWave += 1;
                    timeIndex += 1;
                    endWave = true;
                    timeBetweenWave = nivel.countdownBetweenWaves[timeIndex];
                }
            }

            counterSpawn = 0;
        }

    }

    public void SpawnEnemyPath(IBehaviour.BehaviourType _type)
    {
        //(EnemyMovement.Way)Random.Range(0, System.Enum.GetValues(typeof(EnemyMovement.Way)).Length);  
        GameObject _client = Instantiate(Client, Spawn.transform.position, Spawn.transform.rotation);
        ChangeClientsNumber(+1);
        _client.GetComponent<Client>().type = _type;
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
