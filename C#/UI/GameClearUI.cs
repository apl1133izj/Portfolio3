using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class GameClearUI : MonoBehaviour
{
    public GameObject gameClearCanvas;
    Door door;
    // Update is called once per frame
    void Update()
    {door = FindObjectOfType<Door>();
        if (door.uiOnBool)
        {
            gameClearCanvas.SetActive(true);
        }
    }
}
