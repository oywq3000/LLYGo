using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.AddressableAssets;
using Object = UnityEngine.Object;

public class ResourceFactory
{

    private readonly Dictionary<string,  Object> _assetCache = new Dictionary<string, Object>();
    
    //commonly loading
    public T LoadAsset<T>(string key) where T : class, new()
    {
        if (_assetCache.TryGetValue(key, out Object obj))
        {
            return obj as T;
        }
        else
        {
            var waitForCompletion = Addressables.LoadAssetAsync<T>(key).WaitForCompletion();
            //add to cache
            _assetCache.Add(key, waitForCompletion as Object);

            return waitForCompletion;
        }

    }
    
    //Load and instantiate GameObject
    public GameObject InstantiateGameObject(string key)
    {
        if (_assetCache.TryGetValue(key,out Object obj))
        {
            return GameObject.Instantiate(obj as GameObject);
        }
        else
        {
            var waitForCompletion = Addressables.LoadAssetAsync<GameObject>(key).WaitForCompletion();
            _assetCache.Add(key,waitForCompletion);
            return GameObject.Instantiate(waitForCompletion);
        }
    }
    
    //clean all cache
    public void Release()
    {
        //release by addressables
        foreach (var VARIABLE in _assetCache)
        {
            Addressables.Release(VARIABLE.Value);
        }
        
        //clear cache
        _assetCache.Clear();
    }


}