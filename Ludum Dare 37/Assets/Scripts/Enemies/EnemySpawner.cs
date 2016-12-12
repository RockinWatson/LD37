using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Enemies
{
    class EnemySpawner : MonoBehaviour
    {
        public int _day4 = 1;
        public int _day3 = 2;
        public int _day2 = 3;
        public int _day1 = 4;

        bool isSpawning = false;
        public float minTime = 5.0f;
        public float maxTime = 15.0f;
        public GameObject[] enemies;  // Array of enemy prefabs.   

        IEnumerator SpawnObject(int index, float seconds)
        {
            //Debug.Log("Waiting for " + seconds + " seconds");

            yield return new WaitForSeconds(seconds);
            Instantiate(enemies[index], transform.position, transform.rotation);

            //We've spawned, so now we could start another spawn     
            isSpawning = false;
        }

        void Update()
        {
            //We only want to spawn one at a time, so make sure we're not already making that call
            if (!isSpawning && GameBoard.GetCurrentDay() == 5)
            {
                isSpawning = true; //Yep, we're going to spawn
                int enemyIndex = Random.Range(0, 1);
                StartCoroutine(SpawnObject(enemyIndex, Random.Range(minTime, maxTime)));
            }
            if (!isSpawning && GameBoard.GetCurrentDay() == 4)
            {
                isSpawning = true; //Yep, we're going to spawn
                int enemyIndex = Random.Range(0, 1);
                StartCoroutine(SpawnObject(enemyIndex, Random.Range(minTime - _day4, maxTime - _day4)));
            }
            if (!isSpawning && GameBoard.GetCurrentDay() == 3)
            {
                isSpawning = true; //Yep, we're going to spawn
                int enemyIndex = Random.Range(0, 1);
                StartCoroutine(SpawnObject(enemyIndex, Random.Range(minTime - _day3, maxTime - _day3)));
            }
            if (!isSpawning && GameBoard.GetCurrentDay() == 2)
            {
                isSpawning = true; //Yep, we're going to spawn
                int enemyIndex = Random.Range(0, 1);
                StartCoroutine(SpawnObject(enemyIndex, Random.Range(minTime - _day2, maxTime - _day2)));
            }
            if (!isSpawning && GameBoard.GetCurrentDay() == 1)
            {
                isSpawning = true; //Yep, we're going to spawn
                int enemyIndex = Random.Range(0, 1);
                StartCoroutine(SpawnObject(enemyIndex, Random.Range(minTime - _day1, maxTime - _day1)));
            }
        }
    }
}
