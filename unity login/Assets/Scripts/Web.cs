using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEngine;
using UnityEngine.Networking;

public class Web : MonoBehaviour
{
    void Start()
    {
        
    }
    public void getUsers()
    {
        StartCoroutine(GetRequest("http://localhost/UnityBackendTutorial/getUsers.php"));
    }
    public void getDate()
    {
        StartCoroutine(GetRequest("http://localhost/UnityBackendTutorial/getDate.php"));
    }
    public void registerUser(string username, string password, string confirmPassword)
    {
        if (password == confirmPassword)
        {
            StartCoroutine(login(username, password, "http://localhost/UnityBackendTutorial/RegisterUser.php"));
        }
        else
        {
            Debug.Log("Passwords must match");
        }
    }
    public void loginCall(string username, string password)
    {
        StartCoroutine(login(username, password, "http://localhost/UnityBackendTutorial/login.php"));
    }
    IEnumerator GetRequest(string uri)
    {
        using (UnityWebRequest webRequest = UnityWebRequest.Get(uri))
        {
            // Request and wait for the desired page.
            yield return webRequest.SendWebRequest();

            string[] pages = uri.Split('/');
            int page = pages.Length - 1;

            switch (webRequest.result)
            {
                case UnityWebRequest.Result.ConnectionError:
                case UnityWebRequest.Result.DataProcessingError:
                    Debug.LogError(pages[page] + ": Error: " + webRequest.error);
                    break;
                case UnityWebRequest.Result.ProtocolError:
                    Debug.LogError(pages[page] + ": HTTP Error: " + webRequest.error);
                    break;
                case UnityWebRequest.Result.Success:
                    //Debug.Log(pages[page] + ":\nReceived: " + webRequest.downloadHandler.text);
                    Debug.Log(webRequest.downloadHandler.text);
                    break;
            }
        }
    }
    IEnumerator login(string username, string password, string url)
    {
        WWWForm form = new WWWForm();
        form.AddField("loginUser", username);
        form.AddField("loginPass", password);
        using (UnityWebRequest www = UnityWebRequest.Post(url, form))
        {
            yield return www.SendWebRequest();

            if (www.result != UnityWebRequest.Result.Success)
            {
                Debug.LogError(www.error);
            }
            else
            {
                Debug.Log(www.downloadHandler.text);
            }
        }
    }
}
