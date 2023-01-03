using System;
using UnityEngine;

namespace _core.Script.Music
{
    [RequireComponent(typeof(AudioSource))]
    public class PointSound : MonoBehaviour
    {
        private AudioSource _audioSource;
        private IAssetFactory _assetFactory;
        private void Start()
        {
            _audioSource = GetComponent<AudioSource>();
            _assetFactory = GameFacade.Instance.GetInstance<IAssetFactory>();
            
            //init 
            _audioSource.playOnAwake = false;
            _audioSource.spatialBlend = Single.MaxValue;
        }

        public void PlayOneShot(string clipName)
        {
            var loadAsset = _assetFactory.LoadAsset<AudioClip>(clipName);
            _audioSource.PlayOneShot(loadAsset);
        }
    }
}