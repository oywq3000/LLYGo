using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IAssetFactory
{
   //load and Instantiate Animal 
   T LoadAsset<T>(string key) where T : class, new();
   GameObject LoadInsAsset(string key);
}
