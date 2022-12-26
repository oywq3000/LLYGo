using System;
using System.Collections.Generic;
using DialogueQuests;
using UnityEngine;

namespace _core.Script.Enemy
{
    public class MonsterGroup : MonoBehaviour
    {
        //mange the children monster

        public QuestData questData;
        
        private List<IOnDead> deadRecalls = new List<IOnDead>();

        [SerializeField] private int threasholdCount;

        private int deadCount = 0;

        private void Start()
        {
            var componentsInChildren = transform.GetComponentsInChildren<IOnDead>();

            threasholdCount = componentsInChildren.Length;

            //make the unified recall method
            foreach (var componentsInChild in componentsInChildren)
            {
                componentsInChild.OnDead += OnOneMonsterDead;
            }
        }

        public void MonsterAllDead()
        {
            //Complete quest
            NarrativeManager.Get().CompleteQuest(questData);
        }
        
        
        private void OnOneMonsterDead()
        {
            deadCount++;
            CheckCount();
        }


        private void CheckCount()
        {
            //if the dead Count is greater the threshold
            if (deadCount >= threasholdCount)
            {
                MonsterAllDead();
            }
        }

      
    }
}