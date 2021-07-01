using UnityEngine;
using DG.Tweening;

[DisallowMultipleComponent]
public class FloatingText : MonoBehaviour
{
    public float destroyTime = 2f;
  
    void Awake()
    {
        Destroy(gameObject, destroyTime);
        Vector2 viewportPosition = Camera.main.WorldToViewportPoint(FindObjectOfType<PlayerController>().transform.position);
        transform.GetComponent<RectTransform>().anchoredPosition = viewportPosition;
    }

   
}
