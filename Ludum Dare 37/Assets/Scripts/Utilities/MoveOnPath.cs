using UnityEngine;

namespace Assets.Scripts.Utilities
{
    class MoveOnPath : MonoBehaviour
    {
        public PathFollower PathToFollow;

        public int CurrentWayPointID = 0;
        public float speed;
        public float rotationSpeed = 5.0f;
        public string pathName;

        private float reachDistance = 1.0f;

        Vector3 last_position;
        Vector3 current_position;

        void Start()
        {
            last_position = transform.position;
        }

        void Update()
        {
            float distance = Vector3.Distance(PathToFollow.path_objs[CurrentWayPointID].position, transform.position);
            transform.position = Vector3.MoveTowards(transform.position, PathToFollow.path_objs[CurrentWayPointID].position, Time.deltaTime * speed);

            //Look Rotation if needed
            var rotation = Quaternion.LookRotation(PathToFollow.path_objs[CurrentWayPointID].position - transform.position);
            transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * rotationSpeed);

            if (distance <= reachDistance)
            {
                CurrentWayPointID++;
            }

            if (CurrentWayPointID >= PathToFollow.path_objs.Count)
            {
                CurrentWayPointID = 0;
            }
        }
    }
}
