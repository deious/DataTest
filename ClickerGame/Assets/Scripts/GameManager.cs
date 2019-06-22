using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    //enum CharacterName { AxeWarrior, BatKnight, ElectricBattleMage, FlameSwordman , IceSummoner , KnifeTheif , MagicKnight , PoisonWarrior , WaterBattleMage };
    private static GameManager instance;
    public static GameManager Instance
    {
        get { return instance; }
    }
    //public static GameManager Instance
    //{
    //    get
    //    {
    //        if (instance == null)
    //        {
    //            GameObject tempGM = new GameObject("GameManager");
    //            instance = tempGM.AddComponent<GameManager>();
    //            DontDestroyOnLoad(tempGM);
    //        }
    //        return instance;
    //    }
    //}

    void Awake()
    {
        if (instance)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;

        DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
        goodsCount = new GoodsCount();

        characterImages = new Image[characters.Length][];
        characterPicks = new int[characters.Length][];
        int len = (characters[0].GetComponentsInChildren<Transform>().Length - 1) / 3;

        for (int i = 0; i < characters.Length; i++)
        {
            characterImages[i] = characters[i].GetComponentsInChildren<Image>();
            for (int j = 1; j < characterImages[i].Length; j += 2)
                characterImages[i][j].preserveAspect = true;

            characterPicks[i] = new int[len];
        }
    }

    void OnLevelWasLoaded(int level)
    {
        if (betweenDungeon || betweenFarm || betweenHistoricSite)
        {
            ResetDependency();
            betweenDungeon = false;
            betweenFarm = false;
            betweenHistoricSite = false;
        }
    }

    void ResetDependency()
    {
        Transform GoodsPanel = GameObject.Find("GoodsPanel").transform;
        goodsText[0] = GoodsPanel.Find("FoodPanel").Find("Text").GetComponent<Text>();
        goodsText[1] = GoodsPanel.Find("GoldPanel").Find("Text").GetComponent<Text>();
        goodsText[2] = GoodsPanel.Find("JewelryPanel").Find("Text").GetComponent<Text>();
        GetGoodsCount();

        Transform CharacterPanel = GameObject.Find("CharacterPanel").transform;
        characters[0] = CharacterPanel.Find("CharacterPanel1").gameObject;
        characters[1] = CharacterPanel.Find("CharacterPanel2").gameObject;
        characters[2] = CharacterPanel.Find("CharacterPanel3").gameObject;

        //
        int len = (characters[0].GetComponentsInChildren<Transform>().Length - 1) / 3;
        for (int i = 0; i < characters.Length; i++)
        {
            characterImages[i] = characters[i].GetComponentsInChildren<Image>();
            for (int j = 1; j < characterImages[i].Length; j += 2)
                characterImages[i][j].preserveAspect = true;
        
            characterPicks[i] = new int[len];
        }
    }

    public Text[] goodsText;

    class GoodsCount
    {
        // 재화가 3개일 경우를 가정
        private ulong[] goodsCount;

        public GoodsCount() { goodsCount = new ulong[3]; }

        public ulong this[int index]
        {
            get { return goodsCount[index]; }
            set { goodsCount[index] = value; }
        }
    }
    private GoodsCount goodsCount;

    //private ulong foodCount;
    //public ulong FoodCount
    //{
    //    get { return foodCount; }
    //    set { foodCount = value; }
    //}

    //private ulong goldAmount;
    //public ulong GoldAmount
    //{
    //    get { return goldAmount; }
    //    set { goldAmount = value; }
    //}

    //private ulong jewelryCount;
    //public ulong JewelryCount
    //{
    //    get { return jewelryCount; }
    //    set { jewelryCount = value; }
    //}

    private int countChange;
    public int CountChange
    {
        get { return countChange; }
        set { countChange = value; }
    }

    private int characterPreset;
    public int CharacterPreset
    {
        get { return characterPreset; }
        set { characterPreset = value; }
    }

    private int characterSelect;
    public int CharacterSelect
    {
        get { return characterSelect; }
        set { characterSelect = value; }
    }

    public void Accumulate(int index)
    {
        ulong currentCount;
        ulong.TryParse(goodsText[index].text, out currentCount);
        goodsCount[index] += (ulong)countChange;

        goodsText[index].text = parseUltoS(goodsCount[index]);
    }

    public void Consume(int index)
    {
        ulong currentCount;
        ulong.TryParse(goodsText[index].text, out currentCount);
        if (goodsCount[index] < (ulong)countChange) return;
        else goodsCount[index] -= (ulong)countChange;

        goodsText[index].text = parseUltoS(goodsCount[index]);
    }

    string parseUltoS(ulong ul)
    {
        string s = "";
        char unit = '`';
        int temp = 0;

        while (ul / 10000 != 0)
        {
            unit = (char)((int)unit + 1);
            temp = (int)(ul % 10000);
            ul /= 10000;
        }

        s += ul;
        if (unit > '`') s += "." + string.Format("{0:D2}", temp / 100) + unit;
        return s;
    }


    public GameObject[] characters;

    private Image[][] characterImages;
    public Image[][] CharacterImages
    {
        get { return characterImages; }
    }

    private int[][] characterPicks;
    public int[][] CharacterPicks
    {
        get { return characterPicks; }
    }

    public bool IsNotCharacterSelect(int index)
    {
        int select = 1;
        for (int i = 0; i < characterPicks[index].Length; i++)
            select *= characterPicks[index][i];

        if (select == 0) return false;
        else return true;
    }

    // 인덱서... 리플렉션... 람다식
    [System.Serializable]
    public class Potion
    {
        public Sprite potionSprite;
        public int potionCount;
        // test 코드(실제 효과를 담을 수 있는 코드가 필요)
        // GM 내부의 관리 변수를 이용해서 Control || 특수한 구조체 혹은 클래스를 생성해서 활용
        private string potionEffect = "test";
        public string PotionEffect
        {
            get { return potionEffect; }
        }
    }
    [SerializeField]
    private Potion[] potions;
    public Potion[] Potions
    {
        get { return potions; }
    }

    public Font mainFont;


    //  조합 버프
    //  1. 영웅 속성에 따른 조합 버프 -> 속성에 따른 상관관계를 설정
    //      - 상관 관계에 따라 버프 대상이 전체 또는 특정 대상으로 선정됨
    //  2. 특정 영웅 조합에 따른 조합 버프 -> 영웅 강화 뿐만 아니라 금전적이나 고기 소모의 비율 감소 버프
    //      - 특정 영웅 2명 또는 3명이 조합되는 경우 감소 버프 적용
    //      - a,b,c영웅이 선택된 상황에서 a,b와  b,c가 조합인 경우 a,b 버프와 b,c버프가 같이 적용

    private bool betweenDungeon;
    public bool BetweenDungeon
    {
        set { betweenDungeon = value; }
    }
    private bool betweenFarm;
    public bool BetweenFarm
    {
        set { betweenFarm = value; }
    }
    private bool betweenHistoricSite;
    public bool BetweenHistoricSite
    {
        set { betweenHistoricSite = value; }
    }

    void GetGoodsCount()
    {
        for (int i = 0; i < goodsText.Length; i++)
        {
            goodsText[i].text = goodsCount[i].ToString();
        }
    }

    // GameManager Between Dungeon Manager
    public class GBDInfo
    {
        // 필요한 데이터 던전 입장 시 소모할 식량, 현재 선택한 스테이지,
        // 캐릭터 정보 관련은 아래 useCharacter에서 받음
        public CharacterList[] useCharacters;
        public int mainStage;
        public int subStage;
        public int difficulty;

        // 스테이지 정보 넘기기
        // 버프 정보 넘기기
        //public int characterPreset;
        //public int[][] characterPicks;
        //public CharacterList[] characterList;

        public GBDInfo(int mainStage, int subStage, int difficulty)    // 현재 정상사용이 되지않음
        {
            useCharacters = new CharacterList[3];
            for (int i = 0; i < 3; i++)
            {
                useCharacters[i] = instance.characterList[instance.characterPicks[instance.characterPreset][i] - 1];
            }
            this.mainStage = mainStage;
            this.subStage = subStage;
            this.difficulty = difficulty;

            //return useCharacters;
        }
    }
    public GBDInfo ToDungeonManager()
    {
        return new GBDInfo(instance.stageControll.MainSelected, instance.stageControll.SubSelected, instance.stageControll.DifficultySelected);
    }

    public void FromDungeonManager(ulong goldAmount)
    {
        // 획득한 골드 량
        goodsCount[1] += goldAmount;
    }


    // GameManager Between Farm Manager
    public class GBFInfo
    {
        private ulong foodCount;
        public ulong FoodCount
        {
            get { return foodCount; }
            set { foodCount = value; }
        }

        private ulong goldAmount;
        public ulong GoldAmount
        {
            get { return goldAmount; }
            set { goldAmount = value; }
        }

        public GBFInfo(ulong goodsCount1, ulong goodsCount2)
        {
            this.foodCount = goodsCount1;
            this.goldAmount = goodsCount2;
        }
    }
    public GBFInfo ToFarmManager()
    {
        return new GBFInfo(goodsCount[0], goodsCount[1]);
    }

    public void FromFarmManager(ulong foodCount, ulong goldAmount)
    {
        goodsCount[0] = foodCount;
        goodsCount[1] = goldAmount;
    }


    
    /*===== ===== ===== =====
     * 유적 스테이지
     * 소유한 유물 조각 개수
     * 영웅 강화 현황
     * - 공격력, 치명타 확률, 치명타 데미지
     * 동료 강화 현황
     * - 동료0, 동료1
     * 유적 버프/디버프 강화 현황
     * - 유적 영웅 버프
     * - 유적 몬스터 디버프
     * 음식
     * 골드
     * todo:
     * - 영웅 반영
     */
    public class GBHInfo
    {
        private int relicStage;
        public int RelicStage
        {
            get { return relicStage; }
            set { relicStage = value; }
        }
        private uint artifactPieceCount;
        public uint ArtifactPieceCount
        {
            get { return artifactPieceCount; }
            set { artifactPieceCount = value; }
        }
        private RelicMainHero mainHero;
        public RelicMainHero MainHero
        {
            get { return mainHero; }
            set { mainHero = value; }
        }
        private RelicColleauges colleagues;
        public RelicColleauges Colleagues
        {
            get { return colleagues;  }
            set { colleagues = value; }
        }
        private RelicBuilding building;
        public RelicBuilding Building
        {
            get { return building; }
            set { building = value; }
        }
        private ulong foodCount;
        public ulong FoodCount
        {
            get { return foodCount; }
            set { foodCount = value; }
        }
        private ulong goldAmount;
        public ulong GoldAmount
        {
            get { return goldAmount; }
            set { goldAmount = value; }
        }
        private CharacterList[] useCharacters;
        public CharacterList[] UseCharacters
        {
            get { return useCharacters;  }
            set { useCharacters = value; }
        }
        public GBHInfo
            (
                int relicStage,             uint artifactPieceCount,    RelicMainHero mainHero,     RelicColleauges colleagues,
                RelicBuilding building,     ulong foodCount,            ulong goldAmount,           CharacterList[] characterList
            )
        {
            this.relicStage = relicStage;
            this.artifactPieceCount = artifactPieceCount;
            
            this.mainHero = mainHero;
            this.colleagues = colleagues;
            this.building = building;

            this.foodCount = foodCount;
            this.goldAmount = goldAmount;
            this.useCharacters = (CharacterList[])characterList.Clone();
            
        }
    }
    //  todo: 시작 화면을 구성해서 얼마의 시간동을 유적을 진행할것인지 추가
    public GBHInfo ToHistoricSiteManager()
    {
        return new GBHInfo
            (
                relicStage,     artifactPieceCount, relicMainHero, relicColleagues, 
                relicBuilding,  goodsCount[0],      goodsCount[1], characterList
            );
    }

    public void FromHistoricSiteManager
        (

            int relicStage, uint artifactPieceCount, int[] heroUpgrade, int[] colleaguesUpgrade,
            int[] buildingUpgrade,  ulong goldAmount
        )
    {
        instance.relicStage = relicStage;
        Instance.artifactPieceCount = artifactPieceCount;
        Instance.relicMainHero.heroUpgrade = heroUpgrade;
        Instance.relicColleagues.colleagueUpgrade = colleaguesUpgrade;
        Instance.relicBuilding.relicUpgrade = buildingUpgrade;
        Instance.goodsCount[1] = goldAmount;
    }
    // todo:
    //====================
    // 유물에 관하여
    
    // - 획득한 유물 조각
    // - 현재 가지고 있은 유물 조각
    public int relicStage;
    // todo : 차후 숨김
    public uint artifactPieceCount;
    [System.Serializable]
    public class Artifact
    {
        public bool[] artifactActive;             // - 유물 활성화
        public uint[] artifactUpgrade;        // - 유물 강화 횟수
        public ulong[] artifactUpgradePrice;       // - 유물 강화 비용
        public float[] artifactEnhance;           // - 유물 강화 내용
    }
    public Artifact artifactInfo;
    // 유적 강화에 관하여_메인 영웅 강화
    [System.Serializable]
    public class RelicMainHero
    {
        //  0 : 공격력, 1 : 치명타확률, 2 :치명타데미지
        public int[] heroAttactType;
        public int[] heroUpgrade;
        public int[] heroUpgradePrice;
    }
    public RelicMainHero relicMainHero;
    // 유적 강화에 관하여_동료
    [System.Serializable]
    public class RelicColleauges
    {
        // 0 : 첫번째 동료, 1 : 두번째 동료
        public int[] colleagueDamage;
        public int[] colleagueUpgrade;
        public int[] colleagueUpgradePrice;
    }
    public RelicColleauges relicColleagues;
    // 유적 강화에 관하여_유적
    //  0 : ehance
    //  1 : weaken
    //  relicsEhance는 유적의 저주를 약화 시켜 영웅을 강화 시킴
    //  relicsWeaken는 유적의 저주를 약화 시켜 유적을 약화 시킴
    [System.Serializable]
    public class RelicBuilding
    {
        public int[] relicType;
        public int[] relicUpgrade;
        public int[] relicUpgradePrice;
    }
    public RelicBuilding relicBuilding;


    /* 던전 */
    public bool specialReinforceCheck;

    [System.Serializable]
    public class CharacterList
    {
        public GameObject CharacterPrefab;  // 캐릭터의 프리팹
        public float AttackDamage;          // 캐릭터의 기본공격력
        public float[] SkilDamage;          // 캐릭터가 사용하는 스킬 3가지의 데미지
        public float Hp;                    // 캐릭터의 체력
        public float Reinforcement;         // 캐릭터 강화 횟수
        public int Type;                    // 속성을 의미 0 =불, 1 =물, 2 = 풀
        public int AvoidCount;              // 회피(방어)를 사용 할 수 있는 횟수
        public bool[] SkillFlag;            // 캐릭터 스킬 연속 스킬체크를 위한 플래그
        public bool maxReinforceCheck;      // 캐릭터 일반 강화가 Max인지 체크
        public Sprite CharacterImage;       // 메인에서 사용 할 이미지
        public Sprite[] SkillImages;        // 캐릭터 스킬 3가지 이미지
    }

    [System.Serializable]
    public class StageControll
    {
        public int MainSelected;
        public int SubSelected;
        public int DifficultySelected;
        //public Sprite[] mainImage;
        public int clearCount;
    }

    public CharacterList[] characterList;   // 헷갈리지 않도록 아래에 배치하였음 추후 위치는 이동가능
    public StageControll stageControll;
    //public CharacterList[] tempList;
    //public static CharacterList[] useCharacters = new CharacterList[3];

    //public List<GameObject> CharacterPresetSave = new List<GameObject>();
    // 고른 캐릭터를 저장할 임시 장소가 필요함 (Party를 구분할 수 있어야 함) 저장된 캐릭터를 던전 매니저로 보내주어야 함

   // public CharacterList[] ThrowDataToDungeon()
   // {
   //     // 아래 코드드 맞으나 현재 Missing 문제로 보류
   //     // int len = (characters[0].GetComponentsInChildren<Transform>().Length - 1) / 3;
   //     CharacterList[] useCharacters = new CharacterList[3];
   //
   //     for (int i = 0; i < 3; i++)
   //         useCharacters[i] = characterList[characterPicks[characterPreset][i] - 1];
   //
   //     return useCharacters;
   // }
}