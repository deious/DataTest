using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonManager : MonoBehaviour
{
    private static DungeonManager instance;
    public static DungeonManager Instance {
        get { return instance; }
    }

    public CharacterController cc;
    public MapEnemyController mc;

    public CharacterList[] characterLists;
    public GameObject GameOver;
    public GameObject GameClear;

    public GameManager.GBDInfo infos;         // 메인에 통합시 필요 

    ulong goldAmount;

    public void Injured(float Damage)
    {
        cc.GetInjured(Damage);
    }

    public void BossInjured(float Damage)
    {
        mc.BossGetInjured(Damage);
    }

    void Awake()
    {
        if (instance)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
    }

    void Start()
    {
        // 메인에서 선택한 맵에 대한 정보를 받아오는것이 필요
        //GameManager.CharacterList[] characterList = GameManager.Instance.ThrowDataToDungeon();
        infos = GameManager.Instance.ToDungeonManager();
        for (int i = 0; i < characterLists.Length; i++)
        {
            characterLists[i].CharacterPrefab = infos.useCharacters[i].CharacterPrefab;
            characterLists[i].AttackDamage = infos.useCharacters[i].AttackDamage;
            characterLists[i].Hp = infos.useCharacters[i].Hp;
            characterLists[i].SkilDamage[0] = infos.useCharacters[i].SkilDamage[0];
            characterLists[i].SkilDamage[1] = infos.useCharacters[i].SkilDamage[1];
            characterLists[i].SkilDamage[2] = infos.useCharacters[i].SkilDamage[2];
        }
    }

    // 게임 클리어를 호출하기 위한 메소드
    public void CallGameClear()
    {
        GameClear.SetActive(true);
        Time.timeScale = 0.0f;
    }

    // 게임 패배시 게임오버를 호출하기 위한 메소드
    public void CallGameOver()
    {
        GameOver.SetActive(true);
        Time.timeScale = 0.0f;
    }

    public void DungeonToMain()
    {
        GameManager.Instance.FromDungeonManager(goldAmount);
    }

    // 캐릭터의 정보를 담기위한 클래스
    [System.Serializable]
    public class CharacterList
    {
        public GameObject CharacterPrefab;  // 캐릭터의 프리팹(애니메이션, 애니메이터가 내장)
        public float AttackDamage;          // 캐릭터 기본공격 데미지
        public float[] SkilDamage;          // 캐릭터가 사용하는 3개의 스킬에 해당하는 데미지
        public float Hp;                    // 캐릭터의 체력
        public float Reinforcement = 1;     // 캐릭터 강화 횟수
        public bool life;                   // 캐릭터 생존 여부를 구분하기 위한 플래그값
        public Sprite CharacterImage;       // 캐릭터 변경시 사용할 이미지
        public Sprite[] SkillImages;        // 캐릭터가 사용하는 3개의 스킬에 해당하는 이미지
        public int Type;                    // 캐릭터가 해당하는 속성 (ex) 0 = 불. 1 = 물, 2 = 풀
        public int AvoidCount;              // 회피 가능 횟수
        public bool[] SkillFlag;            // 캐릭터 스킬 연속 사용방지 위한 플래그 기본공격, 스킬1~3
    }
}