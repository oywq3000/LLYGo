using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using LitJson;
using Script;
using UnityEngine;

public class JsonTest : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        // var hokages = JsonMapper
        //     .ToObject<Hokag[]>(File.ReadAllText(Application.dataPath + "/JsonText/Hokag.txt"));
        // foreach (var VARIABLE in hokages)
        // {
        //    Debug.Log(VARIABLE);
        // }
        
        
        //convert object to json

        List<Hokag> hokags = new List<Hokag>()
        {
            new Hokag()
            {
                Name = "鸣人",
                Age = 20,
                Skill = "嘴遁"
            }
            ,
            new Hokag()
            {
                Name = "木叶丸",
                Age = 15,
                Skill = "色诱术"
            }
            , new Hokag()
            {
                Name = "凯",
                Age = 30,
                Skill = "八门遁甲"
            }
            
        };
        string json = JsonMapper.ToJson(hokags);
       json = Regex.Unescape(json);//convert Unicode to normal
       
       //encrypt json data
       json = DataSecurity.EnCrypt(json);


       Debug.Log(json);
       File.WriteAllText(Application.dataPath + "/JsonText/HokagInfo.txt",json,Encoding.UTF8);


       json = DataSecurity.DeCrypt(json);
       Debug.Log(json);

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
