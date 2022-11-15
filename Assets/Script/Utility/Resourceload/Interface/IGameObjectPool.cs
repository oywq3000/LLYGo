using UnityEngine;

namespace Script.Abstract
{
    //implemented by those that need governed by Object Pool
    public interface IGameObjectPool
    {
        GameObject Disable();
        void Init();

        
    }
}