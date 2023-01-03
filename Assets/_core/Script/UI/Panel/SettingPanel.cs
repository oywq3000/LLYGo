using SceneStateRegion;
using Script.Event;
using Script.UI;
using UnityEngine;
using UnityEngine.UI;

public class SettingPanel : AbstractUIPanel
{
    public Button closeBtn;
    public Button returnMenuBtn;

    public Slider musicSlider;
    public Slider soundSlider;

    private void Start()
    {
        //register event
        closeBtn.onClick.AddListener(CloseSelf);

        returnMenuBtn.onClick.AddListener(() =>
        {
            GameLoop.Instance.Controller.SetState(new MenuState(GameLoop.Instance.Controller), false);
        });


        //Init Volume
        musicSlider.value = PlayerPrefs.GetFloat("MusicVolume");
        soundSlider.value = PlayerPrefs.GetFloat("SoundVolume");
        
        //on adding 
        musicSlider.onValueChanged.AddListener(value =>
        {
            PlayerPrefs.SetFloat("MusicVolume", value);
            GameFacade.Instance.SendEvent(new OnMusicVolumeChanged()
            {
                Volume = value
            });
        });

        soundSlider.onValueChanged.AddListener(value =>
        {
            PlayerPrefs.SetFloat("SoundVolume", value);
            GameFacade.Instance.SendEvent(new OnSoundVolumeChanged()
            {
                Volume = value
            });
        });
    }


    public override void OnOpenInstantly()
    {
        Cursor.visible = true;
        GameFacade.Instance.SendEvent(new FreezingCharacter(){IsFreezing = true});
    }

    public override void OnOpen()
    {
       
    }

    protected override void Onclose()
    {
        Cursor.visible = false;
        GameFacade.Instance.SendEvent(new FreezingCharacter(){IsFreezing = false});
    }
}