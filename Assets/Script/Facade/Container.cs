using System;
using System.Collections.Generic;
using UnityEditorInternal;
using UnityEngine;

namespace Script.Facade
{


    public enum Mode
    {
        Singleton,
        Transient
    }

    interface IContainer
    {
        void Register<T>(T obj, Mode mode = Mode.Singleton) where T : class, new();
        T GetInstance<T>() where T : class, new();
    }
    
    public class Container:IContainer
    {
        //store bean 
        private Dictionary<Type, object> _objects;


        public void Register<T>(T obj,Mode mode = Mode.Singleton) where T:class,new()
        {
            switch (mode)
            {
                case Mode.Singleton:
                    if (!_objects.ContainsKey(typeof(T)))
                    {
                        //if it is not duplicate contain it!
                        _objects.Add(typeof(T),obj);
                    }
                    break;
                case Mode.Transient:
                    break;
               
            }
        }

        public T GetInstance<T>()where T:class,new()
        {
            if (_objects.TryGetValue(typeof(T),out object obj))
            {
                return obj as T;
            }
            else
            {
                Debug.LogError($"{typeof(T)} is not registered before");
                return null;
            }
        }
    }
}