using System;
using System.Collections;
using System.Collections.Generic;
using Script.Abstract;
using UnityEngine;

public class PreProcessing : MonoBehaviour
{

   private IGameObjectPool _gameObjectPool;
   private void Start()
   {
      _gameObjectPool =  GameFacade.Instance.GetInstance<IGameObjectPool>();

     
   }

   void PreLoad(string key, int count)
   {
      for (int i = 0; i < count; i++)
      {
         var dequeue = _gameObjectPool.Dequeue(key);
        
      }
   }
   
}
