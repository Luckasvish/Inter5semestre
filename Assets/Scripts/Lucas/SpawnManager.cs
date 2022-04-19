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
        yield return new WaitForSeconds(Random.RandomRange(2, 10));
        Instantiate(Client, Spawn.transform.position, Quaternion.identity);
        StartCoroutine(Spawner());
    }
}
