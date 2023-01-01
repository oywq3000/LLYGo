using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace PlayerRegion
{
    //abstract from Player
    //non-agent in .net framework
    public class CurrentPlayer
    {
        private InventoryScrObj _bag;

        private string _account;

        private int _characterIndex;
        
        private List<_core.AcountInfo.CharacterInfo> _characterInfos;

        //judge a wonderful 

      //todo for final complete 
        public CurrentPlayer(string account)
        {
            //get the current acount
            _account = account;
            //load the bag belonged the account
            _bag =  Resources.Load<InventoryScrObj>("Bag/Inventory/Bag0");

            //to hold the current character index
            _characterIndex = -1;
        }


        
        public void UpdateAccount(string account)
        {
            _account = account;
        }
        public string GetAccount()
        {
            return _account;
        }

        public void UpdateCharacterIndex(int index)
        {
            _characterIndex = index;
        }

        public int GetCharacterIndex()
        {
            return _characterIndex;
        }

        public void SetCharacterList(List<_core.AcountInfo.CharacterInfo> list)
        {
            _characterInfos = list;
        }

        public List<_core.AcountInfo.CharacterInfo> GetCharacterInfos()
        {
            return _characterInfos;
        }
        
        public _core.AcountInfo.CharacterInfo GetSelectedCharacterInfo()
        {
            if (_characterInfos!=null&&_characterIndex!=-1)
            {
                return _characterInfos[_characterIndex];
            }

            return default;
        }
        

        public InventoryScrObj GetBag()
        {
            return _bag;
        }

        public void Dispose()
        {
            Debug.Log("PlayerDisposable");
        }

        ~CurrentPlayer()
        {
            Debug.Log("PlayerDestroy");
        }
    }
}