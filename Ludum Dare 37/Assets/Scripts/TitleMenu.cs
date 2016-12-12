using UnityEngine;

namespace Assets.Scripts
{
    class TitleMenu : MonoBehaviour
    {
        private float _timer = 30f;

        public GameObject[] _elves;
        public GameObject[] _santaurs;

        private bool _canCreateSantaur = true;

        void Update()
        {
            _timer -= Time.deltaTime;
        }

        void FixedUpdate()
        {
            if ((int)_timer == 20)
            {
                Destroy(_elves[0]);
            }
            if ((int)_timer == 10)
            {
                Destroy(_elves[1]);
                SantaurHead();
            }
            if ((int)_timer == 5)
            {
                Destroy(GameObject.FindGameObjectWithTag("SantaurSpeech1"));
            }
        }

        private void SantaurHead()
        {
            if (_canCreateSantaur == true)
            {
                Instantiate(_santaurs[0]);
                Instantiate(_santaurs[1]);
                _canCreateSantaur = false;
            }         
        }
    }
}
