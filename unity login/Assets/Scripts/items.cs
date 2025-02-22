using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using SimpleJSON;
public class items : MonoBehaviour
{
    Action<string> createItemsCallback;
    // Start is called before the first frame update
    void Start()
    {
        createItemsCallback =(jsonArray) => 
        {
            StartCoroutine(CreateItemsRountine(jsonArray));
        };
        createItems();
    }
    public void createItems()
    {
        Main.instance.web.showUserItems(createItemsCallback);
    }
    IEnumerator CreateItemsRountine(string jsonString)
    {
        JSONArray jsonArray = JSON.Parse(jsonString) as JSONArray;
        for (int i = 0; i < jsonArray.Count; i++)
        {
            bool isdone = false;
            string itemId = jsonArray[i].AsObject["itemID"];
            JSONObject itemInfoJson = new JSONObject();

            Action<string> getItemInfoCallback = (itemInfo) => 
            { 
                isdone = true;
                //Debug.Log(itemInfo);
                JSONArray tempArray = JSON.Parse(itemInfo) as JSONArray;
                itemInfoJson = tempArray[0].AsObject;
                //Debug.Log(itemInfoJson);
            };
            Main.instance.web.getitem(getItemInfoCallback, itemId);

            //wait for callback
            yield return new WaitUntil(() => isdone == true);
            //Debug.Log("got here");
            GameObject item = Instantiate(Resources.Load("Prefabs/item") as GameObject);
            item.transform.SetParent(transform);
            item.transform.localScale = Vector3.one;
            item.transform.localPosition = Vector3.zero;

            item.transform.Find("Name").GetComponent<Text>().text = itemInfoJson["name"];
            item.transform.Find("Price").GetComponent<Text>().text = itemInfoJson["price"];
            item.transform.Find("Description").GetComponent<Text>().text = itemInfoJson["description"];



        }
    }
    // Update is called once per frame
    void Update()
    {
       
    }
}
