using System;
using _core.Script.Live.Player.Character;
using _core.Script.Music;
using Cysharp.Threading.Tasks;
using DialogueQuests;
using MysqlUtility;
using Script.Abstract;
using Script.Event;
using Script.Event.Camera;
using Script.Event.CharacterMove;
using Script.Facade;
using Script.UI;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace SceneStateRegion
{
    public class MainState : AbstractState
    {
        private IUIkit _uIkit;

        private bool _isBagOpen = false;
        private bool _isSettingPanelOpen = false;

        private IUnRegisterList _unRegisterList = new UnRegister();

        public MainState(SceneStateController stateController) : base("Main", stateController)
        {
        }

        public override  void StateStart()
        {
            base.StateStart();
            //load weapon bar
            _uIkit = GameFacade.Instance.GetInstance<IUIkit>();
            //register event
            RegisterList();

            //create character

            //todo improve this after 
            var selectedCharacterInfo = GameFacade.Instance.GetSelectedCharacterInfo();
            var playerTransform = GameObject.Find("PlayerPosition").transform;
            var actorData = Addressables.LoadAssetAsync<ActorData>("PlayerGirl").WaitForCompletion();

            if (selectedCharacterInfo.Name=="莉莉娅")
            {
                actorData.portrait = Addressables.LoadAssetAsync<Sprite>("llyHeadIcon").WaitForCompletion();
                var gameObject = GameFacade.Instance.GetInstance<IGameObjectPool>().Dequeue("lly",playerTransform);
                gameObject.GetComponent<ICharacterStatus>().InitCharacterStatus(Int32.Parse(selectedCharacterInfo.Exp));
            }
            else
            {
                actorData.portrait = Addressables.LoadAssetAsync<Sprite>("LSLYHeadIcon").WaitForCompletion();
                var gameObject = GameFacade.Instance.GetInstance<IGameObjectPool>().Dequeue("lsly",playerTransform);
                gameObject.GetComponent<ICharacterStatus>().InitCharacterStatus(Int32.Parse(selectedCharacterInfo.Exp));
            }
            
            //pre load
            // GameFacade.Instance.GetInstance<IGameObjectPool>().ProLoad("Explode10",5);
           
           //operation 
           //hide the mouse 
           Cursor.visible = false;
           
           //player bgm
           AudioManager.Instance.PlayBGM("MainBgm");
        }

        // ReSharper disable Unity.PerformanceAnalysis
        public override void StateUpdate()
        {
            //listening the keyboard event in main scene

            if (Input.GetKeyDown(KeyCode.B))
            {
              
                //open bag
                if (_isBagOpen)
                {
                    _uIkit.ClosePanel("BagPanel");
                   
                }
                else
                {
                    _uIkit.OpenPanel("BagPanel");
                }

                _isBagOpen = !_isBagOpen;
            }

            if (Input.GetKeyDown(KeyCode.T))
            {
                //open bag
                if (_isSettingPanelOpen)
                {
                    _uIkit.ClosePanel("SettingPanel");
                   
                }
                else
                {
                    _uIkit.OpenPanel("SettingPanel");
                }
                _isSettingPanelOpen = !_isSettingPanelOpen;
            }

            
        }


        private void RegisterList()
        {
           
            GameFacade.Instance.RegisterEvent<OnPlayerDead>(e =>
            {
             
                GameFacade.Instance.SendEvent(new FreezingCharacter()
                {
                    IsFreezing = true
                });
                
                //open the dead panel
                _uIkit.OpenPanel("DeadPanel2Main");
            }).AddToUnRegisterList(_unRegisterList);
            

            //register the pause of character movement 
            
            EventTransfer();

            GameFacade.Instance.RegisterEvent<OnPlayerDead>(e =>
            {
                GameFacade.Instance.SendEvent(new FreezingCharacter()
                {
                    IsFreezing = true
                });
                
                //open dead panel
                
            });
        }


        //used to transfer event making higher layer event to specific event of operation
        private async void EventTransfer()  
        {
            //Register Freezing Character Event by the way of transfer
            GameFacade.Instance.RegisterEvent<FreezingCharacter>(e =>
            {
                if (e.IsFreezing)
                {
                    GameFacade.Instance.SendEvent(new ChangeMoveState()
                    {
                        IsCanMove = false
                    });
                
                    GameFacade.Instance.SendEvent(new ChangeWeaponState()
                    {
                        IsCanAttack = false
                    });
                
                    GameFacade.Instance.SendEvent(new ChangeWheelState()
                    {
                        IsEnable = false
                    });
                
                    GameFacade.Instance.SendEvent(new ChangeCameraState()
                    {
                        IsPause = true
                    });

                }
                else
                {
                    GameFacade.Instance.SendEvent(new ChangeMoveState()
                    {
                        IsCanMove = true
                    });
                    GameFacade.Instance.SendEvent(new ChangeWeaponState()
                    {
                        IsCanAttack = true
                    });
                    GameFacade.Instance.SendEvent(new ChangeWheelState()
                    {
                        IsEnable = true
                    });
                    GameFacade.Instance.SendEvent(new ChangeCameraState()
                    {
                        IsPause = false
                    });
                }
            });
            
            //register OnMasterDead for increasing the EXP of character
            GameFacade.Instance.RegisterEvent<OnMasterDead>(e =>
            {
                Debug.Log("Call on Master Dead");
                
                //to catch the message of the master dying
                GameFacade.Instance.SendEvent(new OnExpIncreased()
                {
                    //add  exp for character
                    increment = e.Exp
                });
            }).AddToUnRegisterList(_unRegisterList);
            
            
            
            GameFacade.Instance.RegisterEvent<OnMouseEntryGUI>(e =>
            {
                GameFacade.Instance.SendEvent(new ChangeWeaponState()
                {
                    IsCanAttack = false
                });
            }).AddToUnRegisterList(_unRegisterList);
            
            GameFacade.Instance.RegisterEvent<OnMouseExitGUI>(e =>
            {
                GameFacade.Instance.SendEvent(new ChangeWeaponState()
                {
                    IsCanAttack = true
                });
            }).AddToUnRegisterList(_unRegisterList);

            GameFacade.Instance.RegisterEvent<OnStartAttack>(e =>
            {
                GameFacade.Instance.SendEvent(new ChangeMoveState()
                {
                    IsCanMove = false
                });
            }).AddToUnRegisterList(_unRegisterList);;
            GameFacade.Instance.RegisterEvent<OnEndAttack>(e =>
            {
                GameFacade.Instance.SendEvent(new ChangeMoveState()
                {
                    IsCanMove = true
                });
            }).AddToUnRegisterList(_unRegisterList);;

            GameFacade.Instance.RegisterEvent<OnMouseEntryGUI>(e =>
            {
                GameFacade.Instance.SendEvent(new ChangeCameraState()
                {
                    IsPause = true
                });
            }).AddToUnRegisterList(_unRegisterList);;
            
            GameFacade.Instance.RegisterEvent<OnMouseExitGUI>(e =>
            {
                GameFacade.Instance.SendEvent(new ChangeCameraState()
                {
                    IsPause = false
                });
            }).AddToUnRegisterList(_unRegisterList);;
            
            

            //wait this instance instantiating 
            await UniTask.WaitUntil(() => NarrativeManager.Get()!=null);
            
            Debug.Log("Register NarrativeMangerEvent");
            NarrativeManager.Get().onPauseGameplay += () =>
            {
                GameFacade.Instance.SendEvent(new ChangeMoveState()
                {
                    IsCanMove = false
                });
                
                GameFacade.Instance.SendEvent(new ChangeWeaponState()
                {
                    IsCanAttack = false
                });
                
                GameFacade.Instance.SendEvent(new ChangeWheelState()
                {
                    IsEnable = false
                });
                
                GameFacade.Instance.SendEvent(new ChangeCameraState()
                {
                    IsPause = true
                });
                
                //show mouse 
                Cursor.visible = true;
            };
            
            NarrativeManager.Get().onUnpauseGameplay += () =>
            {
                
                GameFacade.Instance.SendEvent(new ChangeMoveState()
                {
                    IsCanMove = true
                });
                GameFacade.Instance.SendEvent(new ChangeWeaponState()
                {
                    IsCanAttack = true
                });
                GameFacade.Instance.SendEvent(new ChangeWheelState()
                {
                    IsEnable = true
                });
                
                GameFacade.Instance.SendEvent(new ChangeCameraState()
                {
                    IsPause = false
                });
                
                //hide mouse 
                Cursor.visible = false;
            };



            GameFacade.Instance.RegisterEvent<OnCharacterStatusUpdate>(e =>
            {
                //store the current exp for current character
                var selectedCharacterInfo = GameFacade.Instance.GetSelectedCharacterInfo();
                
                //update database record 
                MysqlTool.UpdateCharacterExp(selectedCharacterInfo.Account, selectedCharacterInfo.Id, e.Exp);
            }).AddToUnRegisterList(_unRegisterList);;

        }

        public override void StateEnd()
        {
            //unRegister all event registered in this main state
            _unRegisterList.UnRegisterAll();
            AudioManager.Instance.StopBGM();
            base.StateEnd();
        }
    }
}