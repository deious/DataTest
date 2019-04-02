using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public Stage[] stage;
    //public Queue<GameObject>[] poolList;
    //public Queue<GameObject> EnemyPool;
    public GameObject SpawnPosition;
    public int selected = 0;
    GameObject gameObject;

    /*void Start()                          // 일단 풀을 쓰지않고 복제 형식으로 구현해 보자
    {
        //poolList = new Queue<GameObject>[stage[selected].NormalEnemy.Length + 1];
        poolList = new Queue<GameObject>[2];

        poolList[0] = new Queue<GameObject>(SetEnemy(stage[selected].NormalEnemy[0], stage[selected].NormalEnemyCount[0], stage[selected].EnemyDamage[0], "Enemy_1"));
        poolList[1] = new Queue<GameObject>(SetEnemy(stage[selected].Boss, 1, stage[selected].BossDamage, "Boss_1"));
    }

    Queue<GameObject> SetEnemy(GameObject _Enemy, float EnemyCount, float EnemyDamage, string Name)
    {
        EnemyPool = new Queue<GameObject>();
        GameObject Stage = new GameObject(Name);

        for (int i = 0; i < EnemyCount; i++)
        {
            _Enemy.GetComponent<Enemy>().Damage = EnemyDamage;
            GameObject Enemy = Instantiate(_Enemy) as GameObject;
            Enemy.SetActive(false);
            Enemy.transform.parent = Stage.transform;
            EnemyPool.Enqueue(Enemy);
        }

        return EnemyPool;
    }*/

    void Start()
    {
        StartCoroutine(MakeEnemy());
    }

    IEnumerator MakeEnemy()
    {
        for (int i = 0; i < stage[selected].NormalEnemyCount[0]; i++)
        {
            gameObject = Instantiate(stage[selected].NormalEnemy[0],SpawnPosition.transform.position, SpawnPosition.transform.rotation);
            gameObject.SetActive(true);
            yield return new WaitForSeconds(2.0f);
        }
        gameObject = Instantiate(stage[selected].Boss, SpawnPosition.transform.position, SpawnPosition.transform.rotation);
        gameObject.SetActive(true);
    }

    [System.Serializable]
    public class Stage
    {
        public GameObject[] NormalEnemy;
        public float[] EnemyDamage;
        public float[] NormalEnemyCount;

        public GameObject Boss;
        public float BossDamage;
    }
}
