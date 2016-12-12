using UnityEngine;

public class MenuButton : MonoBehaviour
{
    [SerializeField]
    GameObject _pauseMenu;

    private void OnMouseDown()
    {
        LaunchPauseMenu();
    }

    private void LaunchPauseMenu()
    {
        Time.timeScale = 0.0f;
        Instantiate(_pauseMenu);
    }
}
