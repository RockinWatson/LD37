using UnityEngine;

namespace Assets.Scripts.Utilities
{
    class GetPath : MonoBehaviour
    {
        public GameObject[] allPaths;

        void Start()
        {
            int num = Random.Range(0, allPaths.Length);

            transform.position = allPaths[num].transform.position;
            MoveOnPath thePath = GetComponent<MoveOnPath>();
            thePath.pathName = allPaths[num].name;
        }
    }
}
