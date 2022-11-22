using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        PlayerPrefs.SetString("Name","欧阳");
        
        PlayerPrefs.Save();
        Debug.Log(PlayerPrefs.GetString("Name"));
       
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
