using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Cysharp.Threading.Tasks;
using Script.JsonData;
using UnityEngine;
using UnityEngine.UI;

public class LoginController : MonoBehaviour
{
    private InputField mAccountField;
    private InputField mPasswordField;
    private Text mTips;

    private void Awake()
    {
        mAccountField = transform.Find("AccountInput").GetComponent<InputField>();
        mPasswordField = transform.Find("PasswordInput").GetComponent<InputField>();
        mTips = transform.Find("Tips").GetComponent<Text>();
    }
    
    private void OnEnable()
    {
        //register btn
        var buttons = GetComponentsInChildren<Button>();
        //the first is for login
        buttons[0].onClick.AddListener(() =>
        {
            Login();
        });
        buttons[1].onClick.AddListener(() =>
        {
            Register();
        });
        
    }

    void Register()
    {
      
        if ( mAccountField.text == ""||mPasswordField.text =="")
        {
            Tip("账号不能为空");
        }
        else
        {
            User user = new User();
            user.account = mAccountField.text;
            user.password = mPasswordField.text;
            SaveUser(user);
        }
     
    }
    
    void Login()
    {
      
        if ( mAccountField.text == ""||mPasswordField.text =="")
        {
            Tip("账号不能为空");
        }
        else
        {
            var readPassword = ReadPassword(mAccountField.text);
            if (readPassword== "")
            {
                Tip("用户名未注册");
            }else if (readPassword == mPasswordField.text)
            {
                Tip("登入成功！");
            }
            else
            {
                Tip("密码错误！");
            }
        }
     
    }

 

    void SaveUser(User user)
    {
        if (ReadPassword(user.account) != "")
        {
            Tip("该用户名已被注册,请重输");
            return;
        };
        
        string js = JsonUtility.ToJson(user);
        string fileUrl = Application.streamingAssetsPath + "\\jsonInfo.txt";
        using (StreamWriter sw =new StreamWriter(File.Open(fileUrl,FileMode.Append)))
        {
            sw.WriteLine(js);
            sw.Close();
            sw.Dispose();
        }

        Tip("注册成功！请点击登入");

    }

    string ReadPassword(string account)
    {
        string fileUrl = Application.streamingAssetsPath + "\\jsonInfo.txt";
        string password = "";
        
        using (StreamReader sr =new StreamReader(fileUrl))
        {
            while (!sr.EndOfStream)
            {
                var user = JsonUtility.FromJson<User>(sr.ReadLine());
                if (user.account == account)
                {
                    password = user.password;
                }
            }
            sr.Close();
            sr.Dispose();
        }
        return password;
    }
    
    
    async void  Tip(string message)
    {
        //login
        mTips.text = message;
        mTips.color = Color.red;
        await UniTask.Delay(TimeSpan.FromSeconds(1));
        mTips.text = "";
    }
}
