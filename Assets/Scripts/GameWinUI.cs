using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DisallowMultipleComponent]
public class GameWinUI : UIPanel
{


    public void TapToContinue()
    {
        GameManager.instance.RestartGame();
    }


}
