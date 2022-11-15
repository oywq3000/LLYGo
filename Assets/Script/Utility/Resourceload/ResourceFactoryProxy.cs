using System;
using System.Collections.Generic;
using Script.Abstract;
using UnityEngine;
using UnityEngine.XR.WSA.Input;
using Object = UnityEngine.Object;


namespace Script.AssetFactory
{
    public class ResourceFactoryProxy//:IAssetFactory
    {
        private ResourceFactory _resourceFactory = new ResourceFactory();
        
        private Dictionary<string, Queue<GameObject>> mObjectMetrix = new Dictionary<string, Queue<GameObject>>();

        public void Enqueue(IGameObjectPool obj)
        {
            //Disable this object and get the reference of GameObject
            var gameObject = obj.Disable();

            //find the corresponding key in this dictionary for queue
            if (mObjectMetrix.TryGetValue(gameObject.name.TrimEnd("(Clone)".ToCharArray()),
                out Queue<GameObject> queue))
            {
                queue.Enqueue(gameObject);
            }
            else
            {
                Queue<GameObject> newQueue = new Queue<GameObject>();
                newQueue.Enqueue(gameObject);
                //trim the redundant "(Clone)" for GameObject
                mObjectMetrix.Add(gameObject.name.TrimEnd("(Clone)".ToCharArray()), newQueue);
            }
        }
        
        
        public GameObject DequeueGameObject(string name)
        {
            GameObject gameObject;
            if (mObjectMetrix.TryGetValue(name, out Queue<GameObject> queue))
            {
                if (queue.Count > 0)
                {
                    //dequeue from GameObject queue
                    gameObject = queue.Dequeue();

                    //initiate this gameObject 
                    gameObject.GetComponent<IGameObjectPool>().Init();
                }
                else
                {
                    //if the gameobjects in GameObject Pool are exhausted, load it
                    //note: you do not need put it into pool cause the pool is for disabled GameObject
                    gameObject = _resourceFactory.InstantiateGameObject(name);
                }
            }
            else
            {
                //if not found, load it
                //note: you do not need put it into pool cause the pool is for disabled GameObject
                 gameObject = _resourceFactory.InstantiateGameObject(name);

                //then create a key-value pair for this GameObject Name
                Queue<GameObject> newQueue = new Queue<GameObject>();
             
            }

            return gameObject;
        }
        
        //clean all of GameObject Pool and cache of reference
        public void Release()
        {
            foreach (var keyValuePair in mObjectMetrix)
            {
                while (keyValuePair.Value.Count!=0)
                {
                 GameObject.Destroy( keyValuePair.Value.Dequeue());  
                }
            }
            _resourceFactory.Release();
        }
    }
}