using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DisallowMultipleComponent]
public class PlayGameButton : MonoBehaviour
{
    public void Click()
    {
            EventManager.StartGame();
    }
}
