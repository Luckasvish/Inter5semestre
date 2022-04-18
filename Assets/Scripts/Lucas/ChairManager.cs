using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChairManager : MonoBehaviour
{
    public static ChairManager instance;
    List<GameObject> avaiableChair = new List<GameObject>();

    public GameObject allChair;
    
    GameObject myChair;
    private void Awake()
    {
        instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        foreach (Chair chair in allChair.GetComponentsInChildren<Chair>())
        {
            AddChair(chair.gameObject);
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
        myChair = avaiableChair[chairIndex].gameObject;
        Vector3 pos = myChair.transform.position;
        StartCoroutine(RemovingChair());
        return myChair;
    }
    IEnumerator RemovingChair()
    {   
        yield return new WaitForSeconds(1f);
        RemoveChair(myChair);
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
