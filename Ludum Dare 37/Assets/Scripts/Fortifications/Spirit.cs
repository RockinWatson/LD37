using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spirit : MonoBehaviour {

    private enum State
    {
        MOVE_TO_DESTINATION = 0,
        FIXED = 1,
    };
    private State _state = State.MOVE_TO_DESTINATION;

    [SerializeField]
    const float _speed = 0.75f;

    [SerializeField]
    private SpriteRenderer _sprite = null;

    [SerializeField]
    private float _initialAlpha = 0.5f;

    [SerializeField]
    private int _score = 10;

    private Vector3 _origin;
    private Vector3 _destination;

    private void Start()
    {
        _state = State.MOVE_TO_DESTINATION;
    }

    public void Initialize(Vector3 origin, Vector3 destination)
    {
        _origin = origin;
        transform.position = origin;
        _destination = destination;

        Color color = _sprite.color;
        color.a = _initialAlpha;
        _sprite.color = color;
    }

    private void Update()
    {
        
        switch (_state)
        {
            case State.MOVE_TO_DESTINATION:
                MoveToDestination();
                break;
            case State.FIXED:
                UpdateFixedState();
                break;
            default:
                Debug.LogError("Unrecognized State.");
                break;
        }
    }

    private void MoveToDestination()
    {
        // We at the destination yet?
        if(transform.position.Equals(_destination))
        {
            _state = State.FIXED;
        }
        else
        {
            // Move toward destination.
            transform.position = Vector3.MoveTowards(transform.position, _destination, _speed * Time.deltaTime);
            //Vector3 dirToDest = (_destination - transform.position).normalized;
            //transform.position += (dirToDest * SPEED * Time.deltaTime);
        }

        UpdateAlpha();
    }

    private void UpdateAlpha()
    {
        float top = (transform.position - _origin).magnitude;
        float bot = (_destination - _origin).magnitude;
        float ratio = top / bot;
        float newAlpha = _initialAlpha + (ratio * (1.0f - _initialAlpha));
        Color tmp = _sprite.color;
        tmp.a = newAlpha;
        _sprite.color = tmp;
    }

    private void UpdateFixedState()
    {
    }

    public void Activate()
    {
        //Play Pickup Audio
        var spirit_audio = GameObject.Find("AudioController");
        spirit_audio.GetComponent<AudioController>().Spirit_Audio();

        GameBoard.Get().AddScore(_score);
        GameObject.Destroy(this.gameObject);        
    }

    private void OnMouseDown()
    {
        if (_state == State.FIXED)
        {
            
            Activate();
        }
    }
}
