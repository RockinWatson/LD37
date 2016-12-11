using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SniperElf : MonoBehaviour
{
    [SerializeField]
    private Transform _shotOrigin;

    [SerializeField]
    private float _timeToNewShot = 3.0f;
    private float _timer = 0.0f;

    // Use this for initialization
    private void Start()
    {
        _timer = 0.0f;
    }

    // Update is called once per frame
    private void Update()
    {
        UpdateCoalShot();
    }

    private void UpdateCoalShot()
    {
        if (_timer >= _timeToNewShot)
        {
            CreateNewShot();

            _timer = 0.0f;
        }
        else
        {
            _timer += Time.deltaTime;
        }
    }

    private void CreateNewShot()
    {
        GameObject go = (GameObject)GameObject.Instantiate(Resources.Load("sniper_shot"));
        SniperShot shot = go.GetComponent<SniperShot>();
        shot.Initialize(_shotOrigin.position);
    }
}
