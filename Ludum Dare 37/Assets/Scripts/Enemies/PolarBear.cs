using Assets.Scripts.Utilities;
using UnityEngine;

public class PolarBear : MonoBehaviour {

    [SerializeField]
    private int _health = 5;
    [SerializeField]
    private int _speed = 1;
    [SerializeField]
    private int _damage = 5;

    private bool canMove = true;

    private void Update()
    {
        if (canMove)
        {
            transform.Translate(Vector2.right * _speed * Time.deltaTime);
        }      
    }

    void OnTriggerEnter2D(Collider2D obj)
    {
        if (obj.gameObject.name == "EnemyStopper")
        {
            transform.Translate(new Vector3(0, 0, 0));
            canMove = false;
            Debug.Log("Hitten Dat Stopper");
        }
    }
}
