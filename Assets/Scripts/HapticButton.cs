using UnityEngine.UI;
using UnityEngine;

[DisallowMultipleComponent]
public class HapticButton : MonoBehaviour
{

    public Image buttonIcon;
    public Sprite vibrationOn;
    public Sprite vibrationOff;

    void Start()
    {
        buttonIcon.sprite = HapticManager.isHapticEnable ? vibrationOn : vibrationOff;
    }

    void OnEnable()
    {
        HapticManager.OnHapticStateChanged += HapticManagerStateChanged;
    }

    void OnDisable()
    {
        HapticManager.OnHapticStateChanged -= HapticManagerStateChanged;
    }

    void HapticManagerStateChanged()
    {
        PrefManager.SetBool("isVibrationEnable", !HapticManager.isHapticEnable);
        HapticManager.isHapticEnable = PrefManager.GetBool("isVibrationEnable");
        buttonIcon.sprite = HapticManager.isHapticEnable ? vibrationOn : vibrationOff;
        if (HapticManager.isHapticEnable) Handheld.Vibrate();
    }


    public void OnButtonClick()
    {
        HapticManager.OnHapticStateChange();
    }


}
