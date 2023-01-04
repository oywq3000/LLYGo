using System;
using Script.Event;
using Script.Facade;
using Script.UI;
using UnityEditor;
using UnityEngine.UI;
using UnityEngine;
using System.Collections;

namespace _core.Script.UI.Panel
{
    public class StartPanel:AbstractUIPanel
    {
        public Button startButton;
        public Button exitButton;
       

        private void Start()
        {
            //disable it at first
            startButton.gameObject.SetActive(false);
            
            startButton.onClick.AddListener(() =>
            {
                GameFacade.Instance.GetInstance<IUIkit>().OpenPanel("Login");
                
                //hide this button
                startButton.gameObject.SetActive(false);
            });

            //show or hide start button
            GameFacade.Instance.RegisterEvent<OnStartButtonSwitch>(e =>
            {
                if (e.IsShow)
                {
                    startButton.gameObject.SetActive(true);
                }
                else
                {
                    startButton.gameObject.SetActive(false);
                }
            
            }).UnRegisterOnDestroy(gameObject);
            
            
            exitButton.onClick.AddListener(() =>
            {
#if UNITY_EDITOR
                EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif
            });
            
            //open login panel
            GameFacade.Instance.GetInstance<IUIkit>().OpenPanel("Login");
        }

        public override void OnOpen()
        {
           
        }

        
        
        protected override void Onclose()
        {
         
        }
    }
}