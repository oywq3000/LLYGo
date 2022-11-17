using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;

namespace Script.UI
{
    [RequireComponent(typeof(CanvasGroup))]
    public abstract class AbstractUIPanel : MonoBehaviour, IUIPanel
    {
        private bool _isFirstLoad = true;

        //provide a attribute outside to signify whether this panel is open?
        public bool isOnOpen { get; set; } = false;

        private CanvasGroup _canvasGroup;

        public virtual async void Init()
        {
            //initiate variable
            isOnOpen = true;


            //call for every loads
            gameObject.SetActive(true);

            await DefaultPreOpen();

            OnOpen();
        }


        protected virtual async UniTask DefaultPreOpen()
        {
            _canvasGroup = GetComponent<CanvasGroup>();
            _canvasGroup.alpha = 0;
            await _canvasGroup.DOFade(1, 0.5f).SetEase(Ease.Linear).ToUniTask();
        }

        protected virtual async UniTask DefaultPreClose()
        {
            await _canvasGroup.DOFade(0, 0.3f).SetEase(Ease.Linear).ToUniTask();
        }

        public void SetUILayer(UILayer layer)
        {
            transform.SetParent(GameObject.Find($"UIRoot/{layer.ToString()}").transform);

            if (_isFirstLoad)
            {
                transform.localPosition = Vector3.zero;
                transform.localScale = Vector3.one;
                _isFirstLoad = false;
            }
        }


        public virtual async void Disable()
        {
            Onclose();

            isOnOpen = false;
            
            Debug.Log("Disable");
            await DefaultPreClose();
            
            Debug.Log("Close");
            
            
            
            gameObject.SetActive(false);
           
        }


        public abstract void OnOpen();


        protected abstract void Onclose();
    }
}