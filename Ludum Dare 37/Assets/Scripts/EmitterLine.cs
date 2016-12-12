using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmitterLine : MonoBehaviour {

    [SerializeField]
    private Transform _bound1;
    [SerializeField]
    private Transform _bound2;
    
    [SerializeField]
    private List<GameObject> _types;

    [SerializeField]
    public float _timeMin = 5.0f;

    [SerializeField]
    public float _timeMax = 15.0f;


    private float _timeToEmit;
    private float _timer = 0;

    private void Start()
    {
        PrepForNextEmit();
    }

    private void PrepForNextEmit()
    {
        _timer = 0.0f;
        _timeToEmit = Random.Range(_timeMin, _timeMax);
    }

    void Update()
    {
        _timer += Time.deltaTime;
        if (_timer > _timeToEmit)
        {
            EmitObject();
            PrepForNextEmit();
        }
    }

    private void EmitObject()
    {
        // Pick random position along bounds.
        Vector3 emitPosition = Vector3.Lerp(_bound1.position, _bound2.position, Random.Range(0.0f, 1.0f));

        // Pick random type to emit.
        int typeIndex = Random.Range(0, _types.Count);
        Instantiate(_types[typeIndex], emitPosition, Quaternion.identity);
    }
}
