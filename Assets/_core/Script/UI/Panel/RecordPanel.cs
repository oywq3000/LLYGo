using System;
using _core.Script.UI.Panel.Data;
using Script.Event;
using Script.UI;
using TMPro;
using UnityEngine;

namespace _core.Script.UI.Panel
{
    public class RecordPanel : AbstractUIPanel
    {
        [SerializeField]
        private TextMeshProUGUI killCountText;
        [SerializeField]
        private TextMeshProUGUI surviveTimeText;
        
        private int _killCount;
        private float _surviveTime;
        [SerializeField]
        private bool canRecordTime = true;
        
        private void Start()
        {
            GameFacade.Instance.RegisterEvent<GetRecordPanelData>(e =>
            {
                e.Result.Invoke(new RecordPanelData()
                {
                    KillCount = _killCount,
                    SurviveTime = (int)_surviveTime
                });
            });


            GameFacade.Instance.RegisterEvent<OnMasterDead>(e =>
            {
                _killCount++;
                killCountText.text ="击杀数 "+ _killCount.ToString();
            });

            GameFacade.Instance.RegisterEvent<OnPlayerDead>(e =>
            {
                canRecordTime = false;
            });
            
        }

        private void FixedUpdate()
        {
            if (canRecordTime)
            {
                _surviveTime += Time.deltaTime;
                surviveTimeText.text = "存活时间 "+ ((int) _surviveTime).ToString();
            }
        }


        public override void OnOpen()
        {
            
        }

        protected override void Onclose()
        {
        }
    }
}