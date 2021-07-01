using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[DisallowMultipleComponent]
public class EventManager : MonoBehaviour
{

    public static EventManager instance;
    public static event Action onDie;
    public static event Action<float> onCollisionWithBumper;

    void Awake()
    {
        instance = this;
    }

    public static void OnDie()
    {
        if(onDie != null)
        {
            onDie();
        }
    }

    public static void OnCollisionWithBumper(float value)
    {
        if(onCollisionWithBumper != null)
        {
            onCollisionWithBumper(value);
        }
    }


}
