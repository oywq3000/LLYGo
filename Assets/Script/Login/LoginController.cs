using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoginController : MonoBehaviour
{
    private InputField _nameInput;
    private InputField _passwordInput;
   
    void Start()
    {
        //get input
        _nameInput = transform.Find("InputName").GetComponent<InputField>();
        _passwordInput = transform.Find("InputPassword").GetComponent<InputField>();
        
        //register button event
        transform.Find("LoginBtn").GetComponent<Button>().onClick.AddListener(Login);
        transform.Find("EnrollBtn").GetComponent<Button>().onClick.AddListener(Enroll);
    }

    void Login()
    {
        
    }

    void Enroll()
    {
        
    }
}
