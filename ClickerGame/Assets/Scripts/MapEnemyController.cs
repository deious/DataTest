using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MapEnemyController : MonoBehaviour
{
    public Map[] map;
    Renderer[] renderer = new Renderer[3];  // 3개의 랜더러가 필요함 원경, 중경, 근경
    public static int selected = 0;    // 맵 선택에 따라서 값이 바뀜
    GameObject gameObject;
    public GameObject SpawnPosition;
    static int temp;

    void Start()
    {
        temp = selected;
        map[selected].mapObject.SetActive(true);
        for (int i = 0; i < 3; i++)
        {
            renderer[i] = map[selected].mapObject.transform.GetChild(i).GetComponent<Renderer>();
        }
        StartCoroutine(MakeEnemy());
    }

    void Update()
    {
        // for 문을 통해 컨트룰 할 수 있게 변경하자
        map[selected].offset = Time.time * map[selected].speed[selected];
        for (int i = 0; i < map[selected].speed.Length; i++)
        {
            renderer[i].material.SetTextureOffset("_MainTex", new Vector2(map[selected].offset * map[selected].speed[i], 0));
        }
        /*renderer[0].material.SetTextureOffset("_MainTex", new Vector2(map[selected].offset * map[selected].speed[0], 0));
        renderer[1].material.SetTextureOffset("_MainTex", new Vector2(map[selected].offset * map[selected].speed[1], 0));
        renderer[2].material.SetTextureOffset("_MainTex", new Vector2(map[selected].offset * map[selected].speed[2], 0));*/
    }

    public void ChangeMap()
    {
        Time.timeScale = 1.0f;
        ++selected; // 맵 넘어가는지 확인하기 위해 임시로 넣은 값임 실제에서는 밖에서 선택한 selected 값으로 맵이 바뀔것
        map[temp].mapObject.SetActive(false);
        map[selected].mapObject.SetActive(true);
        for (int i = 0; i < 3; i++)
        {
            renderer[i] = map[selected].mapObject.transform.GetChild(i).GetComponent<Renderer>();
        }
        StartCoroutine(MakeEnemy());
    }

    public void Retry()
    {
        /*SceneManager.LoadScene("Dungeon");*/
        Time.timeScale = 1.0f;
        temp = selected;
        map[temp].mapObject.SetActive(false);
        map[selected].mapObject.SetActive(true);
        for (int i = 0; i < 3; i++)
        {
            renderer[i] = map[selected].mapObject.transform.GetChild(i).GetComponent<Renderer>();
        }
        StartCoroutine(MakeEnemy());
    }

    IEnumerator MakeEnemy()
    {
        for (int i = 0; i < map[selected].NormalEnemy.Length; i++)
        {
            //Debug.Log(map[selected].NormalEnemy.Length);
            for (int j = 0; j < map[selected].NormalEnemyCount[i]; j++)
            {
                gameObject = Instantiate(map[selected].NormalEnemy[i], SpawnPosition.transform.position, SpawnPosition.transform.rotation);
                gameObject.SetActive(true);
                yield return new WaitForSeconds(2.0f);
            }
        }
        gameObject = Instantiate(map[selected].Boss, SpawnPosition.transform.position, SpawnPosition.transform.rotation);
        gameObject.SetActive(true);
    }

    [System.Serializable]
    public class Map
    {
        public GameObject mapObject;    // 완성된 맵 오브젝트(근경, 중경, 원경을 가지고 있음)
        public float[] speed;           // 원경, 중경, 근경의 속도
        public float offset = 1;            // 오프셋

        public GameObject[] NormalEnemy;
        public float[] EnemyDamage;
        public float[] NormalEnemyCount;

        public GameObject Boss;
        public float BossDamage;
    }
}
