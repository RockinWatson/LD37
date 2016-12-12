using UnityEngine;

namespace Assets.Scripts.Enemies
{
    class TitleEnemyMove : MonoBehaviour
    {
        [SerializeField]
        private float _speed = 0.5f;

        void Update()
        {
            transform.Translate(Vector2.left * _speed * Time.deltaTime);
        }

        void OnTriggerEnter2D(Collider2D obj)
        {
            if (obj.gameObject.name == "EnemyStopper")
            {
                Destroy(this.gameObject);
            }
        }
    }
}
