﻿using System;
using _core.Script.Live.Player.Character;
using _core.Script.Utility.Extension;
using Cysharp.Threading.Tasks;
using Script.Event;
using UnityEngine;

namespace _core.Script.Live
{
    public class CharacterLive : LiveEntity,ICharacterStatus
    {
        //the _exp of this character
        [SerializeField] private int exp;
        [SerializeField] private int level;

        [SerializeField] private int wholeMp;
        [SerializeField] private int currentMp;

        private Animator _animator;


        protected override async  void Init()
        {
            //await a frame for the scene initiating 
           await UniTask.Yield();
            
            
            _animator = GetComponent<Animator>();


            //register the event
            GameFacade.Instance.RegisterEvent<OnExpIncreased>(OnExpIncrease);
            
            //update the hub of character
            GameFacade.Instance.SendEvent(new OnCharacterStatusUpdate()
            {
                Exp = exp,
                CurrentHp = currentHp,
                CurrentMp = currentMp
            });
            
            //todo this is testing code you need to delete it in finally test
            //set exp manually
            InitCharacterStatus(exp);
        }
        
        protected override void OnDead()
        {
            //play git hit animation
            _animator.SetTrigger("Dead");
            GameFacade.Instance.SendEvent<OnPlayerDead>();
        }

        protected override void OnGetHit(float damage)
        {
            //Update bleed slider via Event
            GameFacade.Instance.SendEvent(new OnCharacterInjured()
            {
                Damage = damage,
                Duration = _animator.runtimeAnimatorController.animationClips.GetClip("GetHit").length
            });

            //play git hit animation
            _animator.SetTrigger("GetHit");
        }
        
        private void OnExpIncrease(OnExpIncreased increment)
        {
            exp += increment.increment;

            //update the level
            var newLevel = exp / 100;

            if (newLevel > level)
            {
                var increaseLevel = newLevel - level;

                //increase blood
                wholeHp += 35 * increaseLevel;
                currentHp += 35 * increaseLevel;

                wholeMp += 16 * increaseLevel;
                currentMp += 16 * increaseLevel;
                
                //increase Level
                level = newLevel;
            }
            
            //update displaying panel by sending event
            GameFacade.Instance.SendEvent(new OnCharacterStatusUpdate()
            {
                Exp = exp,
                CurrentHp = currentHp,
                CurrentMp = currentMp
            });
        }

        /// <summary>
        /// provide to outside for init character status in game
        /// 
        /// </summary>
        public void InitCharacterStatus(int initExp)
        {
            //init the exp
            exp = initExp;
            
            level = exp / 100;
            wholeHp = 550 + level * 35;
            wholeMp = 80 + level * 16;

            currentHp = wholeHp;
            currentMp = wholeMp;
            
       
        }
        
        //specify current Hp and Mp
        public void InitCharacterStatus(int initExp,int hp,int mp)
        {
            level = initExp / 10;
            wholeHp = 550 + level * 35;
            wholeMp = 80 + level * 16;

            currentHp = hp;
            currentMp = mp;
        }
    }
}