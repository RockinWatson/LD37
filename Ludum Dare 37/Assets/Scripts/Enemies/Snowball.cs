using UnityEngine;

namespace Assets.Scripts.Enemies
{
    class Snowball : Enemy
    {
        public float _spinSpeed = 200.0f;

        void Update()
        {
            transform.Rotate(Vector3.forward * Time.deltaTime * _spinSpeed);
        }
    }
}
