using System;
using System.Collections;
using System.Collections.Generic;
using Script.Abstract;
using Script.AssetFactory;
using Script.Facade;
using Script.UI;
using UnityEngine;

public class GameFacade : MonoBehaviour
{
    public static GameFacade Instance;

    private IContainer _container;
     // Start is called before the first frame update
     private void Awake()
     {
         #region GenerateInstance
         if (GameObject.Find("GameFacade")!=this.gameObject)
         {
             Destroy(this.gameObject);
         }
         else
         {
             Instance = this;
             DontDestroyOnLoad(gameObject);
         }
         #endregion
        
         
         _container = new Container();
         //Instance register list
         RegisterList();
     }
    
     private void RegisterList()
     {
         //there is for registering mono instance

         #region RegisterResourceFactory

         //register common assetFactory
           var assetFactory = Register<IAssetFactory>(new ResourceFactory());
          
           //register GameObject pool by injecting from assetFactory internally 
           var gameObjectPool = Register<IGameObjectPool>(new ResourceFactoryProxy(assetFactory));

           #endregion

         #region UIkit

         //register UIkit by injecting from GameObject pool
         Register<IUIkit>(new UIkit(gameObjectPool));

        
         #endregion
     }
     
     
     public T Register<T>(T obj, Mode mode = Mode.Singleton) where T : class
     {
         _container.Register<T>(obj,mode);
         return obj;
     }

     public T GetInstance<T>() where T : class
     {
        return _container.GetInstance<T>();
     }

     public void Print()
     {
         
     }
}
