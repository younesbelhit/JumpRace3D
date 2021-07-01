using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DisallowMultipleComponent]
public class Finish : MonoBehaviour
{

    public GameObject confettiRainbowPrefab;
    public Transform[] transforms;

    void OnCollisionEnter(Collision col)
    {
        if (col.transform.CompareTag("Player") || col.transform.CompareTag("Opponent"))
        {

            for (int i = 0; i < transforms.Length; i++)
            {
                GameObject confettiRainbow = Instantiate(confettiRainbowPrefab, transforms[i].position, Quaternion.identity);
                confettiRainbow.transform.SetParent(transforms[i]);
            }
            StartCoroutine(GameManager.instance.GameWin());
        }
    }


    void OnEnable()
    {
        
    }

    void OnDisable()
    {
        
    }


}
