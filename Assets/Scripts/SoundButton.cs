using UnityEngine.UI;
using UnityEngine;

[DisallowMultipleComponent]
public class SoundButton : MonoBehaviour
{

    public Image buttonIcon;
    public Sprite soundOn;
    public Sprite soundOff;


    void Start()
    {
        buttonIcon.sprite = AudioManager.isSoundEnable ? soundOn : soundOff;
    }

    void OnEnable()
    {
        AudioManager.OnSoundStateChanged += AudioManagerOnSoundStateChanged;
    }

    void OnDisable()
    {
        AudioManager.OnSoundStateChanged -= AudioManagerOnSoundStateChanged;
    }

    void AudioManagerOnSoundStateChanged()
    {
        PrefManager.SetBool("isSoundEnable", !AudioManager.isSoundEnable);
        AudioManager.isSoundEnable = PrefManager.GetBool("isSoundEnable");
        buttonIcon.sprite = AudioManager.isSoundEnable ? soundOn : soundOff;
        AudioListener.pause = AudioManager.isSoundEnable ? false : true;
    }


    public void OnButtonClick()
    {
        AudioManager.OnSoundStateChange();
    }

}
