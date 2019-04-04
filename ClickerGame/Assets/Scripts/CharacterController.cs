using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterController : MonoBehaviour
{
    //public Animator animator;

    public Character[] character;
    public ButtonControll Btn;
    public static int selected = 0;
    public int skilIndex;
    public static int characterCount = 3;
    int temp = 0;
    int SkilNum = 0;
    GameObject gameObject;
    //AnimationClip ani;

    /*void Awake()
    {
        for(int i = 0; i< character.Length; i++)
        {
            character[i].character = DungeonGameManager.Instance.characterList[i].CharacterPrefab;
        }
    }*/

    void Start()
    {
        characterCount = 3;
        //character[selected].character.SetActive(true);
        for (int i = 0; i < DungeonGameManager.Instance.characterList.Length; i++)
        {
            //character[i].character = DungeonGameManager.Instance.characterList[i].CharacterPrefab;
            //character[i].character = Instantiate(character[selected].character);
            character[i].character = Instantiate(DungeonGameManager.Instance.characterList[i].CharacterPrefab);
        }
        //Instantiate(character[selected].character);
        //SkilNum = character[selected].character.transform.childCount;
        for(int i = 1; i < 5; i++)
        {
            character[selected].character.transform.GetChild(i).GetComponent<BoxCollider2D>().gameObject.SetActive(false);
        }
        //Debug.Log(character[selected].character);
        //gameObject = Instantiate(character[selected].character);
        character[selected].character.SetActive(true);
        //gameObject.SetActive(true);
        //Btn.Board.transform.GetChild(2).GetComponent<Button>().gameObject.SetActive(false);
    }

    public void Change()
    {
        character[temp].character.SetActive(false);
        for (int i = 1; i < SkilNum; i++)
        {
            character[temp].character.transform.GetChild(i).GetComponent<BoxCollider2D>().gameObject.SetActive(false);
        }
        character[selected].character.SetActive(true);
        temp = selected;
    }

    public void Select(int num)
    {
        selected = num;
        if (selected != temp)
        {
            Change();
        }
        //selected = num;
        //Change();
    }
    
    public void DeathChange()
    {
        for(int i = 0; i < character.Length; i++)
        {
            //리스트 사용 방법으로 변경 가능하면 바꿀것
            //character[1].character.SetActive(true);   // 이렇게 하면 되긴 하는데 의미가 없다 로그를 찍어보자
            //Debug.Log(character[i].character.gameObject);

            for(int j = 0; j<character.Length; j++)
            {
                if (character[j].character.gameObject == null || character[j].character.gameObject.transform.Find("Canvas").Find("Health Slider").GetComponent<HpControl>().hp <= 0)
                {
                    //Btn.Board.transform.GetChild(j).GetComponent<Button>().gameObject.SetActive(false);
                    Btn.Board.transform.GetChild(j).GetComponent<Button>().interactable = false;
                }
            }

            if (character[i].character.gameObject == null || character[i].character.gameObject.transform.Find("Canvas").Find("Health Slider").GetComponent<HpControl>().hp <= 0)
            {
                //Debug.Log(Btn.Board.transform.GetChild(i).GetComponent<Button>().gameObject);
                //Btn.Board.transform.GetChild(i).GetComponent<Button>().gameObject.SetActive(false);
                continue;
            }

            character[i].character.SetActive(true);
            temp = i;
            selected = i;
            break;
        }
    }

    //캐릭터 클래스안에 3개 버튼 스킬 및, 캐릭터 별 내장 스킬을 집어 넣고 인덱스를 통해 접근하여 스킬을 실행시키자
    public void Attack()    // 공격시 근접 무기라면 그냥 공격을 하고 원거리 무기라면 instantiate를 사용하여 발사시키는 형식으로 함수만들것
    {
        character[temp].character.GetComponent<Animator>().Play("Attack");
        //GameManager.Instance.characterList[0].Anim[4]
        StartCoroutine(AttackDelay(0.3f));
    }

    public void Shield()
    {
        character[temp].character.GetComponent<Animator>().Play("Shield");
        StartCoroutine(ShieldTime());
    }

    public void Special()
    {
        character[temp].character.GetComponent<Animator>().Play("Special");
        //character[temp].character.GetComponent<Collider2D>().enabled = false;
    }
    // 캐릭터 별로 스킬을 등록하자 -> 그냥 각각의 인덱스로 구분하면 될듯
    public void Skil(int num)
    {
        //character[temp].character.GetComponent<Animator>().Play("skill_1"); // 이렇게 이름으로 하면 고정되버린다
        //character[temp].character.GetComponent<Animation>().Animations[num];
        //character[temp].character.GetComponent<Animation>().Play(character[temp].characterSkil[num]);
        //ani = character[temp].characterSkil[num];
    }

    // 위에 구현막혀서 임시 구현
    public void Skil_1()
    {
        character[temp].character.GetComponent<Animator>().Play("Skil_1");
        StartCoroutine(SkilDelay_1(0.3f));
    }
    public void Skil_2()
    {
        character[temp].character.GetComponent<Animator>().Play("Skil_2");
        StartCoroutine(SkilDelay_2(0.3f));
    }
    public void Skil_3()
    {
        character[temp].character.GetComponent<Animator>().Play("Skil_3");
        StartCoroutine(SkilDelay_3(0.3f));
    }

    IEnumerator Delay()
    {
        yield return new WaitForSeconds(0.3f);
        DeathChange();
    }

    IEnumerator AttackDelay(float time)
    {
        character[selected].character.transform.GetChild(1).GetComponent<BoxCollider2D>().gameObject.SetActive(true);
        yield return new WaitForSeconds(time);
        character[selected].character.transform.GetChild(1).GetComponent<BoxCollider2D>().gameObject.SetActive(false);
    }

    IEnumerator SkilDelay_1(float time)
    {
        character[selected].character.transform.GetChild(2).GetComponent<BoxCollider2D>().gameObject.SetActive(true);
        yield return new WaitForSeconds(time);
        character[selected].character.transform.GetChild(2).GetComponent<BoxCollider2D>().gameObject.SetActive(false);
    }

    IEnumerator SkilDelay_2(float time)
    {
        character[selected].character.transform.GetChild(3).GetComponent<BoxCollider2D>().gameObject.SetActive(true);
        yield return new WaitForSeconds(time);
        character[selected].character.transform.GetChild(3).GetComponent<BoxCollider2D>().gameObject.SetActive(false);
    }

    IEnumerator SkilDelay_3(float time)
    {
        character[selected].character.transform.GetChild(4).GetComponent<BoxCollider2D>().gameObject.SetActive(true);
        yield return new WaitForSeconds(time);
        character[selected].character.transform.GetChild(4).GetComponent<BoxCollider2D>().gameObject.SetActive(false);
    }

    IEnumerator ShieldTime()
    {
        character[temp].character.transform.tag = "Enemy";
        yield return new WaitForSeconds(1.0f);
        character[temp].character.transform.tag = "Player";
    }

    /*public void SkilSet(int skilIndex)
    {
        character[temp].character.GetComponent<Animation>().Play(character[temp].characterSkil[skilIndex])
    }*/
    // 인스펙터에서 애니메이션이 들어가지않는다
    [System.Serializable]
    public class Character
    {
        public GameObject character;
    }

    [System.Serializable]
    public class ButtonControll
    {
        public GameObject Board;
    }
}