using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterController : MonoBehaviour
{
    public Transform canvas;
    public Transform CharacterSpawn;
    public ButtonControll[] Btn;
    public Transform HpBar;                                  // 캐릭터 체력 Hp바
    private GameObject go;
    public GameObject[] CharacterPrefabs;

    public int selected = 0;
    public int characterCount;
    public float []Hp;
    //public bool[] SkillFlag;
    public int temp = 0;

    delegate void Skill();
    Skill[] skills;

    void Start()
    {
        InitCharacter();
        ChangeHp();
    }

    public void InitCharacter()
    {
        characterCount = 3;
        for (int i = 0; i < characterCount; i++)
        {
            Hp[i] = DungeonManager.Instance.characterLists[i].Hp;
            //CharacterPrefabs[i].GetComponent<BoxCollider2D>().enabled = true;
            Btn[i].CharacterChangeBtn.interactable = true;
            DungeonManager.Instance.characterLists[i].life = true;
            for (int j = 0; j < DungeonManager.Instance.characterLists[i].SkillFlag.Length; j++)
            {
                DungeonManager.Instance.characterLists[i].SkillFlag[j] = true;
            }
            go = Instantiate(DungeonManager.Instance.characterLists[i].CharacterPrefab, CharacterSpawn.position, CharacterSpawn.rotation);
            go.transform.parent = canvas;
            Hp[i] = DungeonManager.Instance.characterLists[i].Hp;
            CharacterPrefabs[i] = go;
            go.SetActive(false);
        }
        CharacterPrefabs[selected].SetActive(true);

        skills = new Skill[]
        {
            new Skill(Skill_1),
            new Skill(Skill_2),
            new Skill(Skill_3)
        };
    }

    public void GetInjured(float Damage)
    {
        Hp[selected] -= Damage;
        HpBar.GetComponent<Slider>().value = Hp[selected];

        if (Hp[selected] <= 0)
        {
            DeathChange();
        }
    }

    public void ChangeHp()
    {
        HpBar.GetComponent<Slider>().maxValue = DungeonManager.Instance.characterLists[selected].Hp;
        HpBar.GetComponent<Slider>().value = Hp[selected];
    }

    public void Change()
    {
        CharacterPrefabs[temp].SetActive(false);
        CharacterPrefabs[selected].SetActive(true);
        ChangeHp();
        temp = selected;
    }

    public void Select(int num)
    {
        selected = num;
        if (selected != temp){ Change(); }
    }
    
    public void DeathChange()
    {
        --characterCount;
        CharacterPrefabs[selected].GetComponent<BoxCollider2D>().enabled = false;
        CharacterPrefabs[selected].GetComponent<Rigidbody2D>().AddForce(new Vector2(0, 10), ForceMode2D.Impulse);
        CharacterPrefabs[selected].GetComponent<Animator>().SetTrigger("Die_t");
        Btn[selected].CharacterChangeBtn.interactable = false;
        DungeonManager.Instance.characterLists[selected].life = false;
        CharacterPrefabs[selected].SetActive(false);

        //비효율적인 캐릭터 교체 방식임 캐릭터가 사망시 그 다음차레의 캐릭터로 변경하는 방식으로 변경
        // 자신 인덱스 기준으로 앞에 인덱스를 먼저 체크 앞 배열 인덱스에 캐릭터가 있을경우 교체 없을경우 뒤 인덱스를 체크 
        // 만약 자신이 0번 인덱스일 경우 해당 배열의 마지막 인덱스를 확인 있을 경우 그 캐릭터와 교체 아닐경우 뒤 인덱스를 체크
        // 만약 자신이 마지막 인덱스일 경우 앞에 배열 인덱스 캐릭터의 존재를 체크하고 없으면 해당 배열의 최초 인덱스 체크
        // 아래에 있는 if-else 문은 임시로써 변경 예정임

        if (characterCount == 0)
        {
            DungeonManager.Instance.CallGameOver();
        }
        else if(selected == 0)
        {
            if(DungeonManager.Instance.characterLists[2].life != false)
            {
                CharacterPrefabs[2].SetActive(true);
                selected = 2;
            }
            else
            {
                CharacterPrefabs[1].SetActive(true);
                selected = 1;
            }
        }
        else if(selected == 2)
        {
            if(DungeonManager.Instance.characterLists[1].life != false)
            {
                CharacterPrefabs[1].SetActive(true);
                selected = 1;
            }
            else
            {
                CharacterPrefabs[0].SetActive(true);
                selected = 0;
            }
        }
        else
        {
            if(DungeonManager.Instance.characterLists[0].life != false)
            {
                CharacterPrefabs[0].SetActive(true);
                selected = 0;
            }
            else
            {
                CharacterPrefabs[2].SetActive(true);
                selected = 2;
            }
        }
        temp = selected;
        ChangeHp();
    }

    //캐릭터 클래스안에 3개 버튼 스킬 및, 캐릭터 별 내장 스킬을 집어 넣고 인덱스를 통해 접근하여 스킬을 실행시키자
    public void Attack()
    {
        if (DungeonManager.Instance.characterLists[selected].SkillFlag[0] == true)
            StartCoroutine(AttackDelay(0.2f));
    }

    public void Shield()
    {
        CharacterPrefabs[selected].GetComponent<Animator>().SetTrigger("Shield_t");
        StartCoroutine(ShieldTime());
    }

    public void UseSkill(int index)
    {
        skills[index]();
    }

    // 델리게이트로 구성하였음
    void Skill_1()    // 스킬 딜레이가 적용되지않는 현상 발견
    {
        if (DungeonManager.Instance.characterLists[selected].SkillFlag[1] == true)
            StartCoroutine(SkilDelay_1(0.7f));
    }
    void Skill_2()
    {
        if (DungeonManager.Instance.characterLists[selected].SkillFlag[2] == true)
            StartCoroutine(SkilDelay_2(1.0f));
    }
    void Skill_3()
    {
        if (DungeonManager.Instance.characterLists[selected].SkillFlag[3] == true)
            StartCoroutine(SkilDelay_3(5.0f));
    }

    IEnumerator AttackDelay(float time)
    {
        DungeonManager.Instance.characterLists[selected].SkillFlag[0] = false;
        CharacterPrefabs[selected].GetComponent<Animator>().SetTrigger("Attack_t");
        CharacterPrefabs[selected].transform.GetChild(0).GetComponent<BoxCollider2D>().gameObject.SetActive(true);
        yield return new WaitForSeconds(0.2f);
        CharacterPrefabs[selected].transform.GetChild(0).GetComponent<BoxCollider2D>().gameObject.SetActive(false);
        yield return new WaitForSeconds(time);
        DungeonManager.Instance.characterLists[selected].SkillFlag[0] = true;
    }
    // 딜레이 부분 하나로 통합 할수있지 않을까?
    IEnumerator SkilDelay_1(float time)
    {
        DungeonManager.Instance.characterLists[selected].SkillFlag[1] = false;
        CharacterPrefabs[selected].GetComponent<Animator>().SetTrigger("Skill_1_t");
        CharacterPrefabs[selected].transform.GetChild(1).GetComponent<BoxCollider2D>().gameObject.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        CharacterPrefabs[selected].transform.GetChild(1).GetComponent<BoxCollider2D>().gameObject.SetActive(false);
        yield return new WaitForSeconds(time);
        DungeonManager.Instance.characterLists[selected].SkillFlag[1] = true;
    }

    IEnumerator SkilDelay_2(float time)
    {
        DungeonManager.Instance.characterLists[selected].SkillFlag[2] = false;
        CharacterPrefabs[selected].GetComponent<Animator>().SetTrigger("Skill_2_t");
        CharacterPrefabs[selected].transform.GetChild(2).GetComponent<BoxCollider2D>().gameObject.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        CharacterPrefabs[selected].transform.GetChild(2).GetComponent<BoxCollider2D>().gameObject.SetActive(false);
        yield return new WaitForSeconds(time);
        DungeonManager.Instance.characterLists[selected].SkillFlag[2] = true;
    }

    IEnumerator SkilDelay_3(float time)
    {
        DungeonManager.Instance.characterLists[selected].SkillFlag[3] = false;
        CharacterPrefabs[selected].GetComponent<Animator>().SetTrigger("Skill_3_t");
        CharacterPrefabs[selected].transform.GetChild(3).GetComponent<BoxCollider2D>().gameObject.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        CharacterPrefabs[selected].transform.GetChild(3).GetComponent<BoxCollider2D>().gameObject.SetActive(false);
        yield return new WaitForSeconds(time);
        DungeonManager.Instance.characterLists[selected].SkillFlag[3] = true;
    }

    // 회피 방식 변경할것
    IEnumerator ShieldTime()
    {
        //((GameObject)CharacterPrefabs[temp]).transform.tag = "Enemy";
        CharacterPrefabs[selected].transform.tag = "Enemy";
        yield return new WaitForSeconds(1.0f);
        //((GameObject)CharacterPrefabs[temp]).transform.tag = "Player";
        CharacterPrefabs[selected].transform.tag = "Player";
    }

    [System.Serializable]
    public class ButtonControll
    {
        public Button CharacterChangeBtn;
    }
}

// 캐릭터 컨트룰에 HP 컨트룰도 합칠수 있지 않을까?