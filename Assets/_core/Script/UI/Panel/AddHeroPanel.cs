using System;
using System.Collections.Generic;
using _core.Script.UI.UIVariable;
using Script.UI;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace _core.Script.UI.Panel
{
    
    
    public class AddHeroPanel:AbstractUIPanel,IOperationPanel
    {
        public Action<IVariable> Result { get; set; }

        public TextMeshProUGUI title;
        public Button cFBtn;
        public Button closeBtn;

        public Sprite nonSelected;
        public Sprite selected;

        public Transform selectZone;

        private int _herosIndex;

        private Button[] _optionButtons;
        
        //preset created character name for temp
        private static List<string> _heros = new List<string>()
        {
            "莉莉娅",
            "萝莎莉娅"
        };
        private void Start()
        {
            cFBtn.onClick.AddListener(() =>
            {
                //trigger the recall for this result;
                Result?.Invoke(new SelectHeroName()
                {
                    heroName =   _heros[_herosIndex]
                });
                CloseSelf();
            });
            //register event
            closeBtn.onClick.AddListener(CloseSelf);
            _optionButtons = selectZone.GetComponentsInChildren<Button>();
            RegisterItemEvent();
            //init select 
            InitSelection();
        }


        private void RegisterItemEvent()
        {
            for (int i = 0; i < _optionButtons.Length; i++)
            {
                //record the current variable of i
                int a = i;
                
                _optionButtons[a].onClick.AddListener(() =>
                {
                    //set index 
                    _herosIndex = a;
                    
                    Debug.Log("the current i is "+ a);
                    //change the background of this frame
                    _optionButtons[a].transform.Find("GradeFrame").GetComponent<Image>().sprite = selected;
                    
                    foreach (var componentsInChild in _optionButtons)
                    {
                        if (componentsInChild!= _optionButtons[a])
                        {
                            componentsInChild.transform.Find("GradeFrame").GetComponent<Image>().sprite = nonSelected;
                        }
                    }

                    //assign the name of hero to the title
                    title.text = _heros[_herosIndex];
                });
                
                
                
            }
        }
        
        private void InitSelection()
        {
            _optionButtons[_herosIndex].transform.Find("GradeFrame").GetComponent<Image>().sprite = selected;
                    
            foreach (var componentsInChild in _optionButtons)
            {
                if (componentsInChild!= _optionButtons[_herosIndex])
                {
                    componentsInChild.transform.Find("GradeFrame").GetComponent<Image>().sprite = nonSelected;
                }
            }
            
            //assign the name of hero to the title
            title.text = _heros[_herosIndex];
        }
        
        
        public override void OnOpen()
        {
            
        }

        protected override void Onclose()
        {
            
        }

       
    }
}