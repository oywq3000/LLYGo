using System;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using MysqlUtility;
using UnityEngine;
using UnityEngine.UI;

public class EnrollController : MonoBehaviour
{
    private InputField _nameInput;
    private InputField _passwordInput;
    private Dropdown _sexDropdown;
    private Dropdown _bornYearDropdown;

    private bool _nameCf = false;
    private bool _passwordCf = false;
    private bool _ageCf = false;

    void Start()
    {
        //get input
        _nameInput = transform.Find("InputName").GetComponent<InputField>();
        _passwordInput = transform.Find("InputPassword").GetComponent<InputField>();
        _sexDropdown = transform.Find("SexDropdown").GetComponent<Dropdown>();
        _bornYearDropdown = transform.Find("BornYearDropdown").GetComponent<Dropdown>();

        //register Input check event
        RegisterInputCheckEvent();
        //register button event
        
        transform.Find("EnrollBtn").GetComponent<Button>().onClick.AddListener(Enroll);
        transform.Find("ReturnBtn").GetComponent<Button>().onClick.AddListener(Return);
    }

    void Enroll()
    {
        //get input 
        var myAccount = _nameInput.text;
        var myPassword = _passwordInput.text;
        var mySex = _sexDropdown.captionText.text;
        var myAge = _sexDropdown.captionText.text;

        //format check for input stage
        if (!FormatCheck()) return;

        var password = MysqlTool.GetPassword(myAccount);
        if (password == "")
        {
            //insert the player into database
            MysqlTool.AddPlayer(myAccount, myPassword, mySex, myAge);

            Debug.Log("enroll successfully");
        }
        else
        {
            //todo this name is already enrolled
            Debug.Log("this account is already enrolled");
        }
    }


    void RegisterInputCheckEvent()
    {
        _nameInput.onValueChanged.AddListener(value =>
        {
            var tip = transform.Find("InputName/tip").gameObject;
            //dynamically match this 
            if (Regex.IsMatch(value, "^[-_a-zA-Z0-9]{4,16}$"))
            {
                _nameCf = true;
                tip.SetActive(false);
            }
            else
            {
                _nameCf = false;

                tip.GetComponent<Text>().text = "账号（4到16位，字母数字下划线，减号）";
                tip.SetActive(true);
                
            }
        });
        _passwordInput.onValueChanged.AddListener(value =>
        {
            //dynamically match this 
            var tip = transform.Find("InputPassword/tip").gameObject;
            
            if (Regex.IsMatch(value, "(?=.*[A-Z])(?=.*[a-z])(?=.*\\d)(?=.*[$@!%*#?&])[A-Za-z\\d$@!%*#?&]{6,}$"))
            {
                _passwordCf = true;

              
                tip.SetActive(false);
            }
            else
            {
                _passwordCf = false;
                
                tip.GetComponent<Text>().text = "密码由至少由一个大写、小写和一个特色字符与数字组成";
                tip.SetActive(true);
            }
        });
    }

    bool FormatCheck()
    {
        if (_ageCf&&_passwordCf)
        {
            return true;
        }
        return false;
    }

    void Return()
    {
        gameObject.SetActive(false);
        transform.parent.Find("Login").gameObject.SetActive(true);
    }
}