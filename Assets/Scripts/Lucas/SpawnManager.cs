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
    [SerializeField]
    int clientNumberInWave;

    public float timeBetweenSpawn;
    [SerializeField]
    private float counterSpawn;

    public float timeToWaitBetweenWaves;
    private float counterBetweenWave;

    private bool endWave = false;
    public static bool endNivel = false;
    public Nivel nivel;

    public bool _TutorialEnded = true;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        atualWave = 0;
        clientNumberInWave = 0;
        counterSpawn = 0;
        counterBetweenWave = 0;
        timeBetweenSpawn = nivel.countdownBetweenWaves[atualWave];
    }

    private void Update()
    {

                    //Debug.Log("counterSpawn = " + counterSpawn);
                    //Debug.Log("atualWave = " + atualWave);
                    //Debug.Log(" nivel.waves.Length = " + nivel.waves.Length);

        if(_TutorialEnded)
        {
            if (endWave)
            {
                if (counterBetweenWave >= timeToWaitBetweenWaves)
                {
                    counterBetweenWave = 0;
                    endWave = false;
                    counterSpawn = 0;
                }
                else
                {
                    counterBetweenWave += Time.deltaTime;
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
                    if (clientNumberInWave < nivel.waves[atualWave].enemyToSpawn.Length)
                    {
                        //Debug.Log("--------------3--------------");
                        switch (nivel.waves[atualWave].enemyToSpawn[clientNumberInWave])
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
                        clientNumberInWave += 1;
                        if(clientNumberInWave < nivel.waves[atualWave].countdownBetweenSpawn.Length)
                            timeBetweenSpawn = nivel.waves[atualWave].countdownBetweenSpawn[clientNumberInWave];
                    }
                    else
                    {
                        //Debug.Log("--------------4--------------");
                        clientNumberInWave = 0;
                        atualWave += 1;
                        endWave = true;
                        timeBetweenSpawn = nivel.waves[atualWave].countdownBetweenSpawn[clientNumberInWave];
                        timeToWaitBetweenWaves = nivel.countdownBetweenWaves[atualWave];
                    }
                }

                counterSpawn = 0;
            }
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
