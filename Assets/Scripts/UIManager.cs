using System;
using System.Collections.Generic;
using UnityEngine;


[DisallowMultipleComponent]
public class UIManager : MonoBehaviour
{
    public static UIManager instance;
    public List<UIPanel> uIPanels;
    Dictionary<Type, UIPanel> panelMap = new Dictionary<Type, UIPanel>();

    void Awake()
    {
        instance = this;
        foreach (var uIPanel in uIPanels)
        {
            if (uIPanel)
            {
                panelMap.Add(uIPanel.GetType(), uIPanel);
            }
        }
    }

    public void Show<T>() where T : UIPanel
    {
        panelMap[typeof(T)].Show();
    }


    public void Hide<T>() where T : UIPanel
    {
        panelMap[typeof(T)].Hide();
    }


}
