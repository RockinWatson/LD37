using UnityEngine;

public class QuitButton : MonoBehaviour {
	
	// Update is called once per frame
	void OnMouseDown () {
        Application.Quit();
	}
}
