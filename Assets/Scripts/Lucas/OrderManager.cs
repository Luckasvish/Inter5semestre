using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OrderManager : MonoBehaviour
{
    public static OrderManager instance;
    public RectTransform hudOrderGrid;
    public GameObject orderTextImage;
    public GameObject TabImage;
    public Transform AnimStart;
    public Transform AnimEnd;

    public List<Food> recipesToProduce = new List<Food>();

    [SerializeField]
    Image[] OrderImages = new Image[3];
    bool active;


    private void Awake()
    {
        instance = this;
        StartCoroutine(HudMove());
        //hudOrderGrid.gameObject.SetActive(active);
        //orderTextImage.gameObject.SetActive(active);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
            HideHud();
    }
    void HideHud()
    {
        active = !active;
        StopCoroutine(HudMove());
        StartCoroutine(HudMove());
        //hudOrderGrid.gameObject.SetActive(active);
        //orderTextImage.gameObject.SetActive(active);
        TabImage.SetActive(!active);
    }

    IEnumerator HudMove()
    {
        if(active)
        {
            if(hudOrderGrid.transform.position.x < AnimStart.transform.position.x)
            {
                hudOrderGrid.transform.Translate(new Vector3(AnimStart.transform.position.x, 0) * Time.deltaTime * 100f, Space.World);
                orderTextImage.transform.Translate(new Vector3(AnimStart.transform.position.x, 0) * Time.deltaTime * 100f, Space.World);
                yield return new WaitForSeconds(0.03f);
                StartCoroutine(HudMove());
            }
            else 
                yield break;
        }
        else
        {
            if (hudOrderGrid.transform.position.x > AnimEnd.transform.position.x)
            {
                hudOrderGrid.transform.Translate(new Vector3(AnimEnd.transform.position.x, 0) * Time.deltaTime * 100f, Space.World);
                orderTextImage.transform.Translate(new Vector3(AnimEnd.transform.position.x, 0) * Time.deltaTime * 100f, Space.World);
                yield return new WaitForSeconds(0.03f);
                StartCoroutine(HudMove());
            }
            else 
                yield break;
        }
    }

    public Food AddRecipeToList(Food recipeToAdd,Client client)
    {
        Food clientFood = Instantiate(recipeToAdd, hudOrderGrid);
        recipeToAdd.client = client;
        recipesToProduce.Add(recipeToAdd);
        return clientFood;
    }    
    
    public Sprite GetOrderImage(string _clientOrder)
    {
        int _recipeindex = 0;
        switch (_clientOrder)
        {
            case "Feijoada":
                _recipeindex = 0;
                break;
            case "PratoFeito":
                _recipeindex = 1;
                break;
            case "Buchada":
                _recipeindex = 2;
                break;
        }
        return OrderImages[_recipeindex].sprite;
    }

    public void RemoveRecipeInList(Food recipeToRemove)
    {
        recipesToProduce.Remove(recipeToRemove);
        Destroy(recipeToRemove.gameObject);
    }

}