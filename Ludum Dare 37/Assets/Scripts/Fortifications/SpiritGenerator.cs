using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiritGenerator : BaseFortification
{
    [SerializeField]
    private Transform _spiritOrigin;

    [SerializeField]
    private List<Transform> _spiritDestinations;

    [SerializeField]
    private float _timeToNewSpirit = 10.0f;
    private float _timer = 0.0f;

    private void Start()
    {
        _timer = 0.0f;
    }

    private void Update()
    {
        if (CanUpdate())
        {
            UpdateSpiritGeneration();
        }
    }

    private void UpdateSpiritGeneration()
    {
        if (_timer >= _timeToNewSpirit)
        {
            CreateNewSpirit();

            _timer = 0.0f;
        }
        else
        {
            _timer += Time.deltaTime;
        }
    }

    private void CreateNewSpirit()
    {
        GameObject go = (GameObject)GameObject.Instantiate(Resources.Load("spirit"));
        Spirit spirit = go.GetComponent<Spirit>();
        spirit.Initialize(_spiritOrigin.position, GetSpiritDestination());
    }

    private Vector3 GetSpiritDestination()
    {
        int index = Random.Range(0, _spiritDestinations.Count);
        return _spiritDestinations[index].position;
    }
}
