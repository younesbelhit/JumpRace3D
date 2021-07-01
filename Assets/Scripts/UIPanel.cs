using UnityEngine;

public abstract class UIPanel : MonoBehaviour
{

    public bool isActive;

    public virtual void Awake()
    {
        if (!isActive)
            gameObject.SetActive(false);
        else
            gameObject.SetActive(true);
    }

    public virtual void Show()
    {
        gameObject.SetActive(true);
    }

    public virtual void Hide()
    {
        gameObject.SetActive(false);
    }

}
