using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChairManager : MonoBehaviour
{
    public static ChairManager instance;
    List<GameObject> avaiableChair = new List<GameObject>();

    GameObject allChair;
    

    private void Awake()
    {
        instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        foreach (GameObject chair in allChair.GetComponentsInChildren<GameObject>())
        {
            AddChair(chair);
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddChair(GameObject chair)
    {
        avaiableChair.Add(chair);
    }

    public void RemoveChair(GameObject chair)
    {
        avaiableChair.Remove(chair);
    }

    public void ClearChair()
    {
        avaiableChair.Clear();
    }

    public GameObject GetChair()
    {
        int chairIndex = 0;
        chairIndex = Random.RandomRange(1, avaiableChair.Count);
        GameObject myGameObject = avaiableChair[chairIndex];
        RemoveChair(myGameObject);
        return myGameObject;
    }

    public bool CheckIfHasAvaiableChair()
    {
        if (avaiableChair.Count > 0)
        {
            return true;
        }
        else return false;
    }
}
