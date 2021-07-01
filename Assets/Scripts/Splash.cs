using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine;

[DisallowMultipleComponent]
public class Splash : MonoBehaviour
{
    public float duration = 2f;
    float totalTime = 0f;


    void Start()
    {
        StartCoroutine(SplashScreen());
    }

    IEnumerator SplashScreen()
    {

        while (totalTime < duration)
        {
            totalTime += Time.deltaTime;
            yield return null;
        }

        SceneManager.LoadScene("GAME");
        
    }

}
