using System;
using System.Collections.Generic;
using UnityEngine;

namespace _core.Script.Enemy
{
    public class MonsterGroup : MonoBehaviour
    {
        //mange the children monster

        private List<IOnDead> deadRecalls = new List<IOnDead>();

        private int deadCount = 0;
        
        private void Start()
        {
            var componentsInChildren = transform.GetComponentsInChildren<IOnDead>();

            foreach (var componentsInChild in componentsInChildren)
            {
                componentsInChild.OnDead = OnOneMonsterDead;
            }
        }

        private void OnOneMonsterDead()
        {
            deadCount++;
        }
    }
}