using System;
using System.Collections;
using System.Collections.Generic;
using Script.Facade;
using UnityEngine;

public class GameFacade : MonoBehaviour
{
    public static GameFacade Instance;

    private IContainer _container;
     // Start is called before the first frame update
     private void Awake()
     {
         if (GameObject.Find("GameFacade")!=this.gameObject)
         {
             Destroy(this.gameObject);
         }
         else
         {
             Instance = this;
             DontDestroyOnLoad(gameObject);
         }
     }

     private void Start()
     {
         _container = new Container();
         
         
         
         
         
         
     }

     private void RegisterList()
     {
         
     }
     
     
     public void Register<T>(T obj, Mode mode = Mode.Singleton) where T : class, new()
     {
         _container.Register<T>(obj,mode);
     }

     public T GetInstance<T>() where T : class, new()
     {
        return _container.GetInstance<T>();
     }
}
