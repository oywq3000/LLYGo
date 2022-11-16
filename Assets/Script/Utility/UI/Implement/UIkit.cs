using System;
using System.Collections.Generic;
using Script.Abstract;
using UnityEngine;

namespace Script.UI
{
    public class UIkit:IUIkit
    {
        private IGameObjectPool _gameObjectPool;
        
        private Dictionary<string, GameObject> _openedUiPanel = new Dictionary<string, GameObject>();


        //configure resource loading method via this constructor method
        public UIkit(IGameObjectPool gameObjectPool)
        {
            _gameObjectPool = gameObjectPool;
        }

        public void OpenPanel(string key)
        {
            var gameObject = _gameObjectPool.Dequeue(key);

            try
            {
                //store opened panel 
                _openedUiPanel.Add(key, gameObject);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
           
        }

        public void ClosePanel(GameObject obj)
        {
            try
            {
                _gameObjectPool.Enqueue(obj);

                //remove from opened panel list of panel
                _openedUiPanel.Remove(obj.name.TrimEnd("(Clone)".ToCharArray()));
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
           
        }

        public void ClosePanel(string key)
        {
            try
            {
                _gameObjectPool.Enqueue(_openedUiPanel[key]);
            
                //remove from opened panel list of panel
                _openedUiPanel.Remove(key);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
            
        }


        //Release 
        public void Release()
        {
            _openedUiPanel.Clear();
            
            _gameObjectPool.Release();
        }
    }
}