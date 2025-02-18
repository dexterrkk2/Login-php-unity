using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class RegisterAccount : MonoBehaviour
{
    public InputField usernameInput;
    public InputField passwordInput;
    public InputField confirmPassword;
    public Button registerButton;
    // Start is called before the first frame update
    void Start()
    {
        registerButton.onClick.AddListener(() => Main.instance.web.registerUser(usernameInput.text, passwordInput.text, confirmPassword.text));
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
