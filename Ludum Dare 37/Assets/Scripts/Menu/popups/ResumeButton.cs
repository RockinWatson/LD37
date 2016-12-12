using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResumeButton : MonoBehaviour
{
    private void OnMouseDown()
    {
        KillPauseMenu();
        ResumeGame();
    }

    private void KillPauseMenu()
    {
        GameObject[] popups = GameObject.FindGameObjectsWithTag("PopupMenu");
        foreach(GameObject popup in popups)
        {
            if (popup != null)
            {
                GameObject.Destroy(popup.gameObject);
            }
            else
            {
                Debug.LogError("PAUSE MENU SHOULD EXIST BUT DOESN'T!");
            }
        }
    }

    private void ResumeGame()
    {
        GameBoard._levelOver = false;
        GameBoard._globalTimer -= 1;
        Time.timeScale = 1.0f;
    }
}
