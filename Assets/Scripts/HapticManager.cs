using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[DisallowMultipleComponent]
public class HapticManager : MonoBehaviour
{

    [HideInInspector] public static bool isHapticEnable;
    public static event Action OnHapticStateChanged;

    void Awake()
    {
        isHapticEnable = PrefManager.GetBool("isHapticEnable", true);
    }


    public static void OnHapticStateChange()
    {
        OnHapticStateChanged?.Invoke();
    }
   

    public static void Vibrate()
    {
        if (isHapticEnable)
            Handheld.Vibrate();
        else
            Debug.Log("No Vibration!");
    }

}
