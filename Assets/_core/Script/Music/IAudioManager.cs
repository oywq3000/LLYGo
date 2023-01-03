namespace _core.Script.Music
{
    public interface IAudioManager
    {
      void  PlayBGM(string clipName, bool isLoop = true);
      void PlayOneClip(string clipName);
      void SetVolume();
    }
}