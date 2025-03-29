using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using SimpleJSON;
using UnityEngine.XR;
public class ItemManager : MonoBehaviour
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
            string inventoryID = jsonArray[i].AsObject["ID"];
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
            GameObject itemObject = Instantiate(Resources.Load("Prefabs/item") as GameObject);
            Item item = itemObject.AddComponent<Item>();

            item.ItemID = itemId;
            item.ID = inventoryID;
            
            itemObject.transform.SetParent(transform);
            itemObject.transform.localScale = Vector3.one;
            itemObject.transform.localPosition = Vector3.zero;

            itemObject.transform.Find("Name").GetComponent<Text>().text = itemInfoJson["name"];
            itemObject.transform.Find("Price").GetComponent<Text>().text = itemInfoJson["price"];
            itemObject.transform.Find("Description").GetComponent<Text>().text = itemInfoJson["description"];

            byte[] bytes =ImageManager.Instance.imageLoad(itemId);
            if(bytes.Length == 0)
            {
                Action<byte[]> getItemIconCallback = (downloadedBytes) =>
                {
                    //Debug.Log(webRequest.downloadHandler.data);

                    //Debug.Log(itemInfo);
                    itemObject.transform.Find("Image").GetComponent<Image>().sprite = ImageManager.Instance.convertImage(downloadedBytes);
                    ImageManager.Instance.imageSave(itemId, downloadedBytes);
                    //itemInfoJson = tempArray[0].AsObject;
                    //Debug.Log(itemInfoJson);
                };
                Main.instance.web.getItemImage(itemId, getItemIconCallback);
            }
            //load from device
            else
            {
                itemObject.transform.Find("Image").GetComponent<Image>().sprite = ImageManager.Instance.convertImage(bytes);
            }
            //set sell button
            itemObject.transform.Find("SellButton").GetComponent<Button>().onClick.AddListener(() =>
            {
                string userId = Main.instance.userInfo.getUserID();
                string itemID = itemId;
                string id = inventoryID;
                GameObject self = itemObject;
                //Debug.Log(id);
                Main.instance.web.sellItem(userId, itemID, id);
                Destroy(self);
            });
        }
    }
    // Update is called once per frame
    void Update()
    {
       
    }
}
