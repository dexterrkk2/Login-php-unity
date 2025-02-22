using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserInfo : MonoBehaviour
{
    string userID;
    string userName;
    string password;
    string userLevel;
    string coins;
    public void setInfo(string userId, string username, string userpassword)
    {
        this.userID = userId;
        this.userName = username;
        this.password = userpassword;
    }
    public string getUserID()
    {
        return userID;
    }
}
