using System;
using Script.Event;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace _core.Script.Music
{
    public class AudioManager : MonoBehaviour
    {
        public static AudioManager Instance;

        [SerializeField]
        private AudioSource musicSource;
        [SerializeField]
        private AudioSource soundSource;

        private IAssetFactory _assetFactory;

        private void Awake()
        {
            var find = GameObject.Find("MusicPlayer");
            //avoid the generate the same object
            if (find != null && find != this.gameObject)
            {
                Destroy(this.gameObject);
           
            }
            else
            {
                Instance = this;
                DontDestroyOnLoad(gameObject);
            }
            _assetFactory = GameFacade.Instance.GetInstance<IAssetFactory>();
        }

        private void Start()
        {
            musicSource.playOnAwake = false;
            soundSource.playOnAwake = false;
            
            //init volume 
            musicSource.volume = PlayerPrefs.GetFloat("MusicVolume");
            soundSource.volume = PlayerPrefs.GetFloat("SoundVolume");
            
            //register the volume change event 
            GameFacade.Instance.RegisterEvent<OnMusicVolumeChanged>(e =>
            {
                musicSource.volume = e.Volume;
            });
            GameFacade.Instance.RegisterEvent<OnSoundVolumeChanged>(e =>
            {
                soundSource.volume = e.Volume;
            });

            
        }

        public void PlayBGM(string clipName, bool isLoop = true)
        {
            var audioClip = _assetFactory.LoadAsset<AudioClip>(clipName);
            musicSource.clip = audioClip;
            musicSource.loop = isLoop;
            musicSource.Play();
        }

        public void StopBGM()
        {
            musicSource.Stop();
        }
        public void PlayOneClip(string clipName)
        {
            var audioClip = _assetFactory.LoadAsset<AudioClip>(clipName);
            soundSource.PlayOneShot(audioClip);
        }
        
    }
}