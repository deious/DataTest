using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//  임시 매니저
public class RelicsManager : MonoBehaviour
{

    private static RelicsManager instance;
    public static RelicsManager Instance
    {
        get { return instance; }
    }
    void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        infos = GameManager.Instance.ToHistoricSiteManager();

        relicStage                          = infos.RelicStage;
        artifactPieceCount                  = infos.ArtifactPieceCount;
        relicMainHero                       = new RelicMainHero(infos.MainHero);
        relicColleagues                     = new RelicColleauges(infos.Colleagues);
        relicBuilding                       = new RelicBuilding(infos.Building);
        foodCount                           = infos.FoodCount;
        goldAmount                          = infos.GoldAmount;

        // stage
        if (relicStage < 10)
            currentStage = 0;
        else
            currentStage = relicStage - 10;
        stageText.text = currentStage.ToString();

        // hero init
        inHeroAttackType = new int[infos.MainHero.heroAttactType.Length];
        for (int i =0; i< infos.MainHero.heroAttactType.Length; i++)
        {
            inHeroAttackType[i] = 
                relicMainHero.heroAttactType[i] * (relicMainHero.heroUpgrade[i] + 1);
        }

        // colleague init
        inColleagueDamage = new int[infos.Colleagues.colleagueDamage.Length];
        for (int i =0; i< infos.Colleagues.colleagueDamage.Length; i++)
        {
            inColleagueDamage[i] =
                relicColleagues.colleagueDamage[i] * (relicColleagues.colleagueUpgrade[i] + 1);
        }

        // relics init
        inRelicType = new int[infos.Building.relicType.Length];
        for(int i =0; i< infos.Building.relicType.Length; i++)
        {
            inRelicType[i] =
                relicBuilding.relicType[i] * (relicBuilding.relicUpgrade[i] + 1);
        }

        // gold
        goldText.text = string.Format("{0}", Instance.goldAmount);
        
        // timer
        StartCoroutine("PlayTime");
    }


    // maing data
    GameManager.GBHInfo infos;

    public void HistoricSiteClickerToMain()
    {
        GameManager.Instance.FromHistoricSiteManager
            (
                relicStage,                 artifactPieceCount,
                relicMainHero.heroUpgrade,  relicColleagues.colleagueUpgrade,
                relicBuilding.relicUpgrade, goldAmount
            );
    }


    // 금전 및 식량에 관하여
    public ulong foodCount;
    public ulong goldAmount;
    public Text goldText;
    // ========== ========== ========== ==========
    // Use Moeny
    public void UseGold(long useGold)
    {
        goldAmount = goldAmount - (ulong)useGold;
        goldText.text = string.Format("{0}", Instance.goldAmount);
    }

    // ========== ========== ========== ==========
    // Time Control
    public Text playTimeText;
    public Text turnEndTimeText;

    public float playTime;
    public float endOfTimeDefalut = 15.0f;
    public float endOfTurnTime;

    public GameObject endPanel;
    IEnumerator PlayTime()
    {
        Instance.playTime = foodCount * 0.1f;
        playTimeText.text = playTime.ToString();//string.Format("{0}",playTime);
        turnEndTimeText.text = endOfTimeDefalut.ToString();

        while (true)
        {
            yield return new WaitForSeconds(1.0f);   
            //  전체 시간
            //  todo : gamemanager에서 가져올 수 있도록 변경해야함.
            //  float.TryParse(playTimeText.text, out playTime);
            playTime -= 1.0f;
            playTimeText.text = playTime.ToString();

            if(playTime <= 0)
            {
                endPanel.SetActive(true);
                Time.timeScale = 0;
                Debug.Log("end==========================");
            }

            //  턴 시간
            //  float.TryParse(turnEndTimeText.text, out endOfTurnTime);
            endOfTurnTime -= 1.0f;
            if (endOfTurnTime <= 0)
            {
                //ControlOfMonster.Instance.TurnTimeOut();
                if (currentStage > 1)
                    ControlOfMonster.Instance.TimeOut();
                endOfTurnTime = endOfTimeDefalut;
            }
            turnEndTimeText.text = endOfTurnTime.ToString();

            //  종료 처리
        }
    }
    // 유적내에서 사용할 영웅의 정보
    public int[] GameHeroAttack;


    //====================
    // 유물에 관하여
    // - 획득한 유물 조각
    // - 현재 가지고 있은 유물 조각
    public int relicStage;
    //[HideInInspector]
    public int currentStage;
    // todo : 차후 숨김
    public uint artifactPieceCount;
    // 유적 강화에 관하여_메인 영웅 강화
    public class RelicMainHero
    {
        //  0 : 공격력, 1 : 치명타확률, 2 :치명타데미지
        public int[] heroAttactType;
        public int[] heroUpgrade;
        public int[] heroUpgradePrice;

        public RelicMainHero(GameManager.RelicMainHero mainHero)
        {
            this.heroAttactType = (int[])mainHero.heroAttactType.Clone();
            this.heroUpgrade = (int[])mainHero.heroUpgrade.Clone();
            this.heroUpgradePrice = (int[])mainHero.heroUpgradePrice.Clone();
        }
    }
    public RelicMainHero relicMainHero;
    // 유적 강화에 관하여_동료
    public class RelicColleauges
    {
        // 0 : 첫번째 동료, 1 : 두번째 동료
        public int[] colleagueDamage;
        public int[] colleagueUpgrade;
        public int[] colleagueUpgradePrice;

        public RelicColleauges(GameManager.RelicColleauges colleague)
        {
            this.colleagueDamage = (int[])colleague.colleagueDamage.Clone();
            this.colleagueUpgrade = (int[])colleague.colleagueUpgrade.Clone();
            this.colleagueUpgradePrice = (int[])colleague.colleagueUpgradePrice.Clone();
        }
    }
    public RelicColleauges relicColleagues;
    // 유적 강화에 관하여_유적
    //  0 : ehance
    //  1 : weaken
    //  relicsEhance는 유적의 저주를 약화 시켜 영웅을 강화 시킴
    //  relicsWeaken는 유적의 저주를 약화 시켜 유적을 약화 시킴
    public class RelicBuilding
    {
        public int[] relicType;
        public int[] relicUpgrade;
        public int[] relicUpgradePrice;

        public RelicBuilding(GameManager.RelicBuilding building)
        {
            this.relicType         = (int[])building.relicType.Clone();
            this.relicUpgrade      = (int[])building.relicUpgrade.Clone();
            this.relicUpgradePrice = (int[])building.relicUpgradePrice.Clone();   
        }
    }
    public RelicBuilding relicBuilding;
    // ========== ========== ========== ==========
    // Monster Control
    public GameObject monsterImage;
    public GameObject monsterHPImage;
    public Text stageText;
    public int monsterTotalHP;
    public int monsterHP;
    // ========== ========== ========== ==========
    // Hero Control
    // - hero animator;
    // - hero damage
    public Animator heroAnimator;
    [HideInInspector]
    public int[] inHeroAttackType;
    // ========== ========== ========== ==========
    // Colleague Control
    // todo : colleagueDamage 부분은 manager 통합후 변경되야함 + ControlOfColleagueAttack내부 코드도 변경되야함 ...흠 기본공격으로 초기화 해서 받아온다?
    // public int colleagueNum; -> test용도

    [HideInInspector]
    public int[] inColleagueDamage;
    // ========== ========== ========== ==========
    // Control Relic
    // - Control of Reclis 스크립트에서 해야할것
    //    -> start했을때 유물 현황에 따른 유물 변화
    //    -> stage 변화에 따른 유물 변화
    //    -> 유물 활성에 따른 변화

    //  내부 relics 내부 강화 적용
    //  relicsUpgrade는 db에서 강화 횟수 들고옴
    //  0 : ehance
    //  1 : weaken
    //  relicsEhance는 유적의 저주를 약화 시켜 영웅을 강화 시킴
    //  relicsWeaken는 유적의 저주를 약화 시켜 유적을 약화 시킴
    [HideInInspector]
    public int[] inRelicType;
}
