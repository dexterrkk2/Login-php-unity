using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using SimpleJSON;
public class ImageManager : MonoBehaviour
{
    public static ImageManager Instance;
    JSONObject versionJson;
    string versionPath;
    string basePath;
    // Start is called before the first frame update
    void Start()
    {
        if(Instance != null)
        {
            Destroy(Instance);
            return;
        }
        Instance = this;
        versionJson = new JSONObject();
      
        basePath = Application.persistentDataPath + "/Images/";
        if (!Directory.Exists(basePath))
        {
            Directory.CreateDirectory(basePath);
        }
        versionPath = basePath + "VersionJson";
        if (File.Exists(versionPath))
        {
            string jsonText = File.ReadAllText(versionPath);
            versionJson = JSON.Parse(jsonText) as JSONObject;
        }
    }
    bool imageExists(string name)
    {
        return File.Exists(basePath + name);
    }
    public void imageSave(string name, byte[] bytes, int imgVer)
    {
        File.WriteAllBytes(basePath + name, bytes);
        updateVersionJson(name, imgVer);
    }
    public byte[] imageLoad(string name, int imgVer)
    {
        byte[] bytes = new byte[0];
        if (imageExists(name)&& imageUpToDate(name,imgVer))
        {
            bytes =File.ReadAllBytes(basePath + name);
        }
        return bytes;
    }
    public Sprite convertImage(byte[] bytes)
    {
        Texture2D texture = new Texture2D(2, 2);
        texture.LoadImage(bytes);

        Sprite sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(.5f, .5f));
        return sprite;
    }

    void updateVersionJson(string name, int imgVer)
    {
        versionJson[name] = imgVer;
    }
    bool imageUpToDate(string name, int ver)
    {
        if (versionJson[name] != null)
        {
            return versionJson[name].AsInt == ver;
        }
        return false;
    }
    public void saveVersionJson()
    {
        File.WriteAllText(versionPath, versionJson.ToString());
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
