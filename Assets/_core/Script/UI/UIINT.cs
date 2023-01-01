using System;
using Script.Event.CharacterMove;
using Unity.Burst;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UIElements;

namespace _core.Script.UI
{
    public class UIINT : MonoBehaviour,IPointerEnterHandler,IPointerExitHandler,IPointerUpHandler
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
          
        }


        public void OnPointerUp(PointerEventData eventData)
        {
            GameFacade.Instance.SendEvent(new ChangeWeaponState()
            {
                IsCanAttack = false
            });
        }
    }
}