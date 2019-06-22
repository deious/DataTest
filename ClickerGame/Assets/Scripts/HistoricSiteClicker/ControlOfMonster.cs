using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ControlOfMonster : MonoBehaviour
{

    public GameObject monsterObject;
    //public int monsterAmount;
    //List<GameObject> monsterObjects = null;

    private static ControlOfMonster instance;
    public static ControlOfMonster Instance
    {
        get { return instance; }
    }
    //[SerializeField]
    //private float lerpSpeed;

    void Awake()
    {
        instance = this;
    }


    public void Start()
    {
        MonsterHPInit();

    }

    //  공격 당함 
    //  todo : 
    public void BeAttacked(int damage)
    {
        RelicsManager.Instance.monsterHP = RelicsManager.Instance.monsterHP - damage;
        //  check monster die
        if (RelicsManager.Instance.monsterHP <= 0)
        {
            MonsterStageUp();
            MonsterHPInit();
            MonsterChange();
            MonsterStage();
            //Debug.Log("boss die and change");
            RelicsManager.Instance.endOfTurnTime = RelicsManager.Instance.endOfTimeDefalut;
            RelicsManager.Instance.turnEndTimeText.text = RelicsManager.Instance.endOfTimeDefalut.ToString();
        }
        //  change hp image
        ChangeHPImage(RelicsManager.Instance.monsterTotalHP, RelicsManager.Instance.monsterHP);
    }

    //  일정 시간안에 몬스터를 몹잡는 경우
    public void TimeOut()
    {
        MonsterStageDown();
        MonsterHPInit();
        MonsterChange();
        MonsterStage();

        ChangeHPImage(RelicsManager.Instance.monsterTotalHP, RelicsManager.Instance.monsterHP);
    }
    // check monster hp
    void ChangeHPImage(int totalHP, int currentHP)
    {
        float hpCurrent = (float)currentHP / (float)totalHP;
        //Debug.LogError(hpCurrent);
        RelicsManager.Instance.monsterHPImage.GetComponent<Image>().fillAmount = hpCurrent;
       
    }


    //  몬스터 체력 초기화(stage에 따른 체력 변화)
    void MonsterHPInit()
    {
        int hp = 10000 * (RelicsManager.Instance.currentStage + 1);// * stage;

        //  디버프 적용
        hp = hp - (int)(0.1 * RelicsManager.Instance.inRelicType[1]);
        RelicsManager.Instance.monsterTotalHP = RelicsManager.Instance.monsterHP = hp;
    }


    //  유적 단계 변화
    //  유적 파괴시 단계 증가
    void MonsterStageUp()
    {
        if (RelicsManager.Instance.currentStage >= RelicsManager.Instance.relicStage)
        {
            //Debug.Log(" Get Relics : " + "stage: " + stage + "----- stage%20 : " + stage % 20);
            RelicsManager.Instance.relicStage = RelicsManager.Instance.currentStage;
            GetRelicsPiece(RelicsManager.Instance.currentStage);
            
        }
        RelicsManager.Instance.currentStage++;
    }

    //  유적 단계 변화
    //  유적 파괴시 단계 증가
    void MonsterStageDown()
    {
        RelicsManager.Instance.currentStage--;
    }

    //  몬스터 변경
    //  스테이지 확인 후 변경
    void MonsterChange()
    {
        // 오브젝트 풀링
        //string monsterImagePath = string.Format("Images/Monster/{0}", RelicsManager.Instance.stage);
        //RelicsManager.Instance.monsterImage.GetComponent<Image>().sprite = Resources.Load<Sprite>(monsterImagePath);
        string monsterImagePath = "Animators/" +  RelicsManager.Instance.currentStage;
        RelicsManager.Instance.monsterImage.GetComponent<Animator>().runtimeAnimatorController = 
            (RuntimeAnimatorController)Instantiate(Resources.Load(monsterImagePath, typeof(RuntimeAnimatorController)));
    }
    //  유적지 단계 변경
    void MonsterStage()
    {
        RelicsManager.Instance.stageText.text = RelicsManager.Instance.currentStage.ToString();
    }
    void GetRelicsPiece(int stage)
    {
        //Debug.Log((stage / 10) - 1);
        //if (!RelicsManager.Instance.artifactCheckPiece[(stage / 10) - 1])
        //{
        //    RelicsManager.Instance.artifactCheckPiece[(stage / 10) - 1] = true;
        //    RelicsManager.Instance.artifactPieceCount++;
        //}
        if(RelicsManager.Instance.currentStage % 10 == 0)
        {
            RelicsManager.Instance.artifactPieceCount++;
        }
    }
}
