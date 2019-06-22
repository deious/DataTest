using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MapEnemyController : MonoBehaviour
{
    public Transform MonsterPool;           // 몬스터 풀의 위치를 맞춰놓고 거기서 생성하도록 한다 
    public Transform BossHpBar;
    public Map[] map;
    Renderer[] rd = new Renderer[3];        // 3개의 랜더러가 필요함 원경, 중경, 근경
    public int selected;             // 맵 선택에 따라서 값이 바뀜
    GameObject EnemyObject;
    GameObject BossObject;
    public Queue<GameObject> Pool = new Queue<GameObject>();
    public Transform SpawnPosition;
    static int temp;
    float TotalEnemyCount;

    void Start()
    {
        selected = DungeonManager.Instance.infos.mainStage;

        BossHpBar.GetComponent<Slider>().maxValue = map[selected].boss.BossHp;
        BossHpBar.GetComponent<Slider>().value = map[selected].boss.BossHp;

        for (int i = 0; i < map[selected].enemy.Length; i++)
        {
            TotalEnemyCount += map[selected].enemy[i].NormalEnemyCount;
        }

        temp = selected;
        map[selected].mapObject.SetActive(true);
        for (int i = 0; i < 3; i++)
        {
            rd[i] = map[selected].mapObject.transform.GetChild(i).GetComponent<Renderer>();
        }

        MakeEnemy();
        StartCoroutine(PushEnemy());
    }

    void Update()
    {
        map[selected].offset = Time.time * map[selected].speed[0];

        for (int i = 0; i < map[selected].speed.Length; i++)
        {
            rd[i].material.SetTextureOffset("_MainTex", new Vector2(map[selected].offset * map[selected].speed[i], 0));
        }
    }

    public void BossGetInjured(float Damage)
    {
        map[selected].boss.BossHp -= Damage;
        BossHpBar.GetComponent<Slider>().value = map[selected].boss.BossHp;

        if (map[selected].boss.BossHp <= 0)
        {
            Destroy(BossObject, 0.5f);
            DungeonManager.Instance.CallGameClear();
        }
    }

    public void ChangeMap()
    {
        Time.timeScale = 1.0f;
        ++selected; // 맵 넘어가는지 확인하기 위해 임시로 넣은 값임 실제에서는 밖에서 선택한 selected 값으로 맵이 바뀔것
        map[temp].mapObject.SetActive(false);
        map[selected].mapObject.SetActive(true);
        for (int i = 0; i < 3; i++)
        {
            rd[i] = map[selected].mapObject.transform.GetChild(i).GetComponent<Renderer>();
        }

        ClearPool();
        MakeEnemy();
        StartCoroutine(PushEnemy());
    }

    public void ClearPool()
    {
        Pool.Clear();
    }

    public void Retry()
    {
        Time.timeScale = 1.0f;
        DungeonManager.Instance.cc.characterCount = 3;
        DungeonManager.Instance.cc.CharacterPrefabs[DungeonManager.Instance.cc.selected].SetActive(false);
        DungeonManager.Instance.cc.selected = 0;
        DungeonManager.Instance.cc.temp = DungeonManager.Instance.cc.selected;
        for(int i=0; i< 3; i++)
        {
            DungeonManager.Instance.cc.Hp[i]= DungeonManager.Instance.characterLists[i].Hp;
            DungeonManager.Instance.characterLists[i].life = true;
            DungeonManager.Instance.cc.CharacterPrefabs[i].GetComponent<BoxCollider2D>().enabled = true;
            DungeonManager.Instance.cc.Btn[i].CharacterChangeBtn.interactable = true;
        }
        DungeonManager.Instance.cc.CharacterPrefabs[DungeonManager.Instance.cc.selected].SetActive(true);
        DungeonManager.Instance.cc.ChangeHp();

        ClearPool();
        MakeEnemy();
        StartCoroutine(PushEnemy());
    }


    // 안정성을 위해 전체 다 생성으로 변경
    void MakeEnemy()
    {
        for (int i = 0; i < map[selected].enemy.Length; i++)
        {
            for (int j = 0; j < (map[selected].enemy[i].NormalEnemyCount); j++)
            {
                EnemyObject = Instantiate(map[selected].enemy[i].NormalEnemy, SpawnPosition.transform.position, SpawnPosition.transform.rotation);
                EnemyObject.transform.parent = MonsterPool;
                EnemyObject.SetActive(false);
                Pool.Enqueue(EnemyObject);
            }
        }
    }

    IEnumerator PushEnemy()
    {
        for(int i = 0; i< TotalEnemyCount; i++)
        {
            EnemyObject = Pool.Dequeue();
            EnemyObject.SetActive(true);
            yield return new WaitForSeconds(3.5f);
        }
        MakeBoss();
    }

    // 보스 몬스터 생성 
    // 풀에 있는 몬스터들이 필요한 횟수만큼 나가게 하는 함수 안에서 호출하도록 함 
    public void MakeBoss()
    {
        BossHpBar.gameObject.SetActive(true);
        BossObject = Instantiate(map[selected].boss.BossPrefab, SpawnPosition.transform.position, SpawnPosition.transform.rotation);
        BossObject.SetActive(true);
    }

    [System.Serializable]
    public class Map
    {
        public GameObject mapObject;        // 완성된 맵 오브젝트(근경, 중경, 원경을 가지고 있음)
        public float[] speed;               // 원경, 중경, 근경의 속도
        public float offset = 1;            // 오프셋

        public Enemy[] enemy;
        public Boss boss;
    }

    [System.Serializable]
    public class Enemy
    {
        public GameObject NormalEnemy;      // 일반 몬스터 프리팹
        public float EnemyDamage;           // 일반 몬스터 데미지
        public float NormalEnemyCount;      // 일반 몬스터 수
        public float NormalEnemyHp;         // 일반 몬스터 체력
        public float MoveSpeed;             // 움직임 속도
        public float DropGold;              // 사망시 주는 골드
        public float AntiElasticity;        // 반탄력
    }

    [System.Serializable]
    public class Boss
    {
        public GameObject BossPrefab;       // 보스 프리팹
        public float BossDamage;            // 보스 데미지
        public float BossHp;                // 보스 체력
        public float MoveSpeed;             // 움직임 속도
        public float DropGold;              // 사망시 주는 골드
        public float AntiElasticity;        // 반탄력
    }
}
