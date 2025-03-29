using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Windows;

public class ImageManager : MonoBehaviour
{
    public static ImageManager Instance;

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

        basePath = Application.persistentDataPath + "/Images/";
        if (!Directory.Exists(basePath))
        {
            Directory.CreateDirectory(basePath);
        }
    }
    bool imageExists(string name)
    {
        return File.Exists(basePath + name);
    }
    public void imageSave(string name, byte[] bytes)
    {
        File.WriteAllBytes(basePath + name, bytes);
    }
    public byte[] imageLoad(string name)
    {
        byte[] bytes = new byte[0];
        if (imageExists(name))
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
    // Update is called once per frame
    void Update()
    {
        
    }
}
