using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IAssetFactory
{
   //load common resource
   T LoadAsset<T>(string key) where T : class, new();
   
   //load and Instantiate GameObject
   GameObject InstantiateGameObject(string key);

   //release the cached 
   void Release();
}
