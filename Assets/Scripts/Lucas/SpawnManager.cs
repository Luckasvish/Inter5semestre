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
        counterSpawn += Time.deltaTime;
        if (endWave)
        {
            if (counterWave >= timeBetweenWave)
            {
                counterWave = 0;
                endWave = false;
            }
            else
            {
                counterWave += Time.deltaTime;
            }
        }

        if (counterSpawn >= timeBetweenSpawn && !endWave)
        {
            if (atualWave < nivel.waves.Length)
            {
                if (waveSpawner < nivel.waves[atualWave].enemyToSpawn.Length)
                {
                    switch (nivel.waves[atualWave].enemyToSpawn[atualWave])
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
                    waveSpawner += 1;
                }
                else
                {
                    waveSpawner = 0;
                    atualWave += 1;
                    timeIndex += 1;
                    endWave = true;
                    timeBetweenWave = nivel.countdownBetweenWaves[timeIndex];
                }
            }
            else
            {
                endNivel = true;
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
