using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[DisallowMultipleComponent]
public class MainMenuUI : UIPanel
{
    public GameObject tapToStart;
    public GameObject holdToGoForward;
    public TextMeshProUGUI currentLevelText;
    public TextMeshProUGUI nextLevelText;

    void Start()
    {

        holdToGoForward.SetActive(false);
        currentLevelText.text = GameManager.instance.level.ToString();
        nextLevelText.text = (GameManager.instance.level + 1).ToString();
        
    }

    public void StartGame()
    {
        Destroy(tapToStart);
        if(holdToGoForward != null)
        {
            holdToGoForward.SetActive(true);
        }
        Destroy(holdToGoForward, 3f);
        GameManager.instance.StartGame();
        FindObjectOfType<PlayerController>().GetComponent<Rigidbody>().isKinematic = false;
    }

}
