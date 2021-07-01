using UnityEngine;
using System;

[DisallowMultipleComponent]
public class AudioManager : Singleton<AudioManager>
{

    [HideInInspector] public static bool isSoundEnable;
    public static event Action OnSoundStateChanged;

    public override void Awake()
    {
        base.Awake();
        isSoundEnable = PrefManager.GetBool("isSoundEnable", true);
        AudioListener.pause = isSoundEnable ? false : true;
    }

    public static void OnSoundStateChange()
    {
        OnSoundStateChanged?.Invoke();
    }


}
