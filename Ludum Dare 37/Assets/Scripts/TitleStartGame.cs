using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.Scripts
{
    class TitleStartGame : MonoBehaviour
    {
        void OnMouseDown()
        {
            SceneManager.LoadScene("TestScene1");
        }
    }
}
