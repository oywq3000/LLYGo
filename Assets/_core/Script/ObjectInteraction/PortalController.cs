using System;
using UnityEngine;

namespace _core.Script.ObjectInteraction
{
    public class PortalController:MonoBehaviour
    {
        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                //Load new scene
                Debug.Log("Load new scene");
            }
        }

        private void Start()
        {
            
        }
    }
}