using System;
using Script.Event.CharacterMove;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UIElements;

namespace _core.Script.UI
{
    public class UIINT : MonoBehaviour,IPointerEnterHandler,IPointerExitHandler
    {
        //interrupt the weapon click when the mouse is over this GUI 

   
        public void OnPointerEnter(PointerEventData eventData)
        {
            Debug.Log("OnMouseEnter");
            GameFacade.Instance.SendEvent(new ChangeWeaponState()
            {
                IsCanAttack = false
            });
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            Debug.Log("OnMouseExit");
            
            GameFacade.Instance.SendEvent(new ChangeWeaponState()
            {
                IsCanAttack = true
            });
        }
    }
}