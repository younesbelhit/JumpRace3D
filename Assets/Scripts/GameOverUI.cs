using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DisallowMultipleComponent]
public class GameOverUI : UIPanel
{



    public void TapToContinue()
    {
        GameManager.instance.RestartGame();
    }
    


    
}
