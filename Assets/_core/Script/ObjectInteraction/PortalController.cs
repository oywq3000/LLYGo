using System;
using SceneStateRegion;
using UnityEngine;

namespace _core.Script.ObjectInteraction
{
    public class PortalController : MonoBehaviour
    {
        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                GameLoop.Instance.Controller.SetState(new FlowerLawnState(GameLoop.Instance.Controller));
            }
        }
        
    }
}