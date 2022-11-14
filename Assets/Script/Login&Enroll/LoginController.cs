using System.Collections;
using System.Collections.Generic;
using MysqlUtility;
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
        //get input 
        var myName = _nameInput.text;
        var myPassword = _passwordInput.text;

        //check input
        if (myName != "")
        {
            var password = MysqlTool.GetPassword(myName);
            if (password != "")
            {
                if (password == myPassword)
                {
                    //todo login successfully
                    Debug.Log("login successfully");
                }
                else
                {
                    //todo password error
                    Debug.Log("password error");
                }
            }
            else
            {
                //todo not find this name from database
                Debug.Log("not find this name from database");
            }
        }
        else
        {
            //todo not string is input

            Debug.Log(" not string is input");
        }
    }

    void Enroll()
    {
        gameObject.SetActive(false);
        transform.parent.Find("Enroll").gameObject.SetActive(true);
    }
}