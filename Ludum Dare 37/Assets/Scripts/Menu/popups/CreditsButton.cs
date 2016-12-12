using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.Scripts.Menu.popups
{
    class CreditsButton : MonoBehaviour
    {
        void OnMouseDown()
        {
            SceneManager.LoadScene("CreditsScene");
        }
    }
}
