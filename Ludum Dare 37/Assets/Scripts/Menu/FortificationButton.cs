using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts;

public class FortificationButton : MonoBehaviour {

    [SerializeField]
    Fortification.Type _type;

    private void OnMouseDown()
    {
        Activate();
    }

    private void Activate()
    {
        Debug.Log("SETTING FORT TYPE TO: " + _type);
        GameBoard.Get().SetSelectedFortificationType(_type);
    }
}
