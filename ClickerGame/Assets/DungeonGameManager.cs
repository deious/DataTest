using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using System.Diagnostics;

public class DungeonGameManager : MonoBehaviour
{
    //List<object> CharacterData = new List<object>();
    private static DungeonGameManager instance;
    public static DungeonGameManager Instance {
        get { return instance; }
    }
    // 스태틱이 2개라 그런가 확인해보았으나 아닌것으로 판명
    
    public Manage manage;
    public CharacterList[] characterList;
    //public GameObject[] CharacterPrefab;

    int gold;
    public int Gold
    {
        get { return gold; }
        set { gold = value; }
    }
    int food;
    public int Food
    {
        get { return food; }
        set { food = value; }
    }
    int jewelry;
    public int Jewelry
    {
        get { return jewelry; }
        set { jewelry = value; }
    }
    
    void Awake()
    {
        if(instance)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;

        DontDestroyOnLoad(gameObject);

        //GameManager.Instance.characterList[0].CharacterPrefab;
        /*List<GameObject> CharacterList = new List<GameObject>();
        for(int i = 0; i< CharacterPrefab.Length; i++)
        {
            CharacterList.Add(CharacterPrefab[i]);
        }*/
    }

    void Start()
    {
        for(int i = 0; i < manage.controller.Length; i++)
        {
            manage.controller[i].gameObject.SetActive(true);
        }

        // 방안 1
        for (int i = 0; i < GameManager.Instance.characterList.Length; i++)
        {
            Debug.Log(GameManager.Instance.characterList.Length);
            characterList[i].CharacterPrefab = GameManager.Instance.characterList[i].CharacterPrefab;
            characterList[i].AttackDamage = GameManager.Instance.characterList[i].AttackDamage;
            for(int j = 0; j < 3; j++)
            {
                characterList[i].SkilDamage[j] = GameManager.Instance.characterList[i].SkilDamage[j];
            }
            characterList[i].Hp = GameManager.Instance.characterList[i].Hp;
            //characterList[j].CharacterPrefab.SetActive(false);
            //리스트 값 집어넣기
        }

        // 방안 2 가져오기
        /*for (int j = 0; j < GameManager.Instance.Prefabs.Length; j++)
        {
            characterList[j].CharacterPrefab = GameManager.Instance.Prefabs[j];
        }*/


        // 리스트 값을 데이터베이스로 올리고 내리는 메소드가 필요 나중에 물어볼것
        // 시작 시 리스트 값을 내려 받기
        // 리스트의 인덱스를 이용하여 해당 값들이 변경 가능함을 확인하였음
        //CharacterData.Add(characterList[0]);
        //characterList[0].AttackDamage = 100;
    }

    // 다른 씬으로 데이터를 보내기 위한 함수
    /*public CharacterList[] SendInfo()
    {
        return characterList;
    }*/

    // 다른 씬에서 보낸 데이터를 받기위한 함수
    // 이 함수에서는 돈을 받음
    // 어차피 메인매니저에서 돈을 관리할때 직접 접근이 가능할텐데 이 함수가 필요한가?
    public void ReceiveInfo()
    {

    }

    [System.Serializable]
    public class Manage
    {
        public GameObject[] controller;
    }

    [System.Serializable]
    public class CharacterList
    {
        public GameObject CharacterPrefab;
        public Sprite character;
        //public AnimationClip[] Anim;
        public float AttackDamage;
        public float[] SkilDamage;
        public float Hp;
    }
}