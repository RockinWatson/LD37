using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts
{
    public class PresentMovement : MonoBehaviour
    {
        [SerializeField]
        float _speed = 1.0f;

        [SerializeField]
        float _spinSpeed = 200.0f;

        // Update is called once per frame
        void Update()
        {
            MoveAndSpin();
        }

        private void MoveAndSpin()
        {
            Vector3 angle = new Vector3(-5, -4);
            transform.Translate(angle * _speed * Time.deltaTime, Space.World);
            transform.Rotate(Vector3.forward * Time.deltaTime * _spinSpeed);
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
