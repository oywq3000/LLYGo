using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;

namespace Script.UI
{
    public abstract class AbstractUIPanel:MonoBehaviour,IUIPanel
    {
        private bool _isFirstLoad = true;
        
        public async virtual void Init()
        {
            if (_isFirstLoad)
            {
                Debug.Log("Init");
                transform.SetParent(GameObject.Find("UIRoot/Common").transform);
                transform.localPosition = Vector3.zero;
                transform.localScale = Vector3.one;
                _isFirstLoad = false;
            }
            
            //call for every loads
            gameObject.SetActive(true);

            #region DefaultDotwenn
            var canvasGroup = GetComponent<CanvasGroup>();
            canvasGroup.alpha = 0;
            await canvasGroup.DOFade(1, 0.5f).SetEase(Ease.Linear).ToUniTask();
            #endregion
            Debug.Log("OnOpen");
            OnOpen();
        }
        public virtual void Disable()
        {
            gameObject.SetActive(false);
            Onclose();
        }


        public abstract void OnOpen();
        protected abstract void Onclose();

    }
}