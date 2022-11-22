using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Xml;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class XMLController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        // Debug.Log("sss");
        // XmlDocument xml = new XmlDocument();
        //
        // //read xml file
        // xml.Load(Application.dataPath + "/Xml/Hokags.xml");
        //
        // //get the root node of xml file
        // XmlNode root = xml.LastChild;
        //
        //
        // //get primary node of root
        // var rootChildNodes = root.ChildNodes;
        //
        //
        // List<XmlElement> xmlElements = new List<XmlElement>();
        //
        // foreach (var rootChildNode in rootChildNodes)
        // {
        //     xmlElements.Add(rootChildNode as XmlElement);
        // }
        //
        // foreach (var VARIABLE in rootChildNodes)
        // {
        //     Debug.Log(VARIABLE);
        // }
        //
        // foreach (var element in xmlElements)
        // {
        //
        //     Hokag hokag = new Hokag();
        //     hokag.ID = element.GetAttribute("id");
        //     hokag.Name = element.ChildNodes[0].InnerText;
        //     hokag.Age = Int32.Parse(element.ChildNodes[1].InnerText);
        //     hokag.Skill = element.ChildNodes[2].InnerText;
        //     
        //     Debug.Log(hokag);
        // }

        XmlDocument xml = new XmlDocument();
        
        //create declaration
        xml.AppendChild(xml.CreateXmlDeclaration("1.0", "UTF-8", null));
        
        //create root element
        xml.AppendChild(xml.CreateElement("Hokags"));
        
        //create root node
        XmlNode root = xml.SelectSingleNode("Hokags");
        //add element
        var hokagInfo = xml.CreateElement("HokagInfo");

        //set attribute
        hokagInfo.SetAttribute("id", "1");

        var nameElement = xml.CreateElement("Name");
        var ageelement = xml.CreateElement("Age");
        var skillElement1 = xml.CreateElement("Skill");

        nameElement.InnerText = "鸣人";
        ageelement.InnerText = "21";
        skillElement1.InnerText = "嘴遁";

        root.AppendChild(hokagInfo);
        hokagInfo.AppendChild(nameElement);
        hokagInfo.AppendChild(ageelement);
        hokagInfo.AppendChild(skillElement1);
        
        xml.Save(Application.dataPath+"/Xml/newHokags.xml");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
