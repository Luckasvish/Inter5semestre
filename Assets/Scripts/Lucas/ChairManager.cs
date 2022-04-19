using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChairManager : MonoBehaviour
{
    public static ChairManager instance;
    List<GameObject> avaiableChair;

    public GameObject allChair;
    
    GameObject myChair;
    private void Awake()
    {
        instance = this;
        avaiableChair = new List<GameObject>();
        foreach (Chair chair in allChair.GetComponentsInChildren<Chair>())
        {
            AddChair(chair.gameObject);
        }
    }


    public void AddChair(GameObject chair)
    {
        avaiableChair.Add(chair);
        Debug.Log(avaiableChair.Count);
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
        chairIndex = Random.RandomRange(0, avaiableChair.Count);
        Debug.Log(chairIndex);
        myChair = avaiableChair[chairIndex];
        //StartCoroutine(RemovingChair());
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
