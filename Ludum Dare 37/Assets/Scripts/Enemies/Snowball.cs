using UnityEngine;

namespace Assets.Scripts.Enemies
{
    class Snowball : Enemy
    {
        public float _speed = 2.0f;
        public float _spinSpeed = 200.0f;

        void Update()
        {
            transform.Translate(Vector2.right * _speed * Time.deltaTime, Space.World);
            transform.Rotate(Vector3.forward * Time.deltaTime * _spinSpeed);
        }
    }
}
