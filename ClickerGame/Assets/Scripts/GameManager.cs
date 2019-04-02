using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public CharacterList[] characterList;
    public object[] Prefab;
    //public static GameManager Instance;
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
        //foodCount = 0ul;
        //goldAmount = 0ul;
        //jewelryCount = 0ul;


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
    GoodsCount goodsCount;

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

    // Manager간 통신(방법1)
    // GameManager에서 각 Scene의 Manager에게 필요한 정보를 취합하여 보냄
    public GMBTInfo ToTestManager()
    {
        Debug.Log(goodsCount[0] + ", " + goodsCount[1] + ", " + goodsCount[2]);
        return new GMBTInfo(goodsCount[0], goodsCount[1], goodsCount[2]);
    }

    public void ToFarmManager()
    {

    }

    public void ToDungeonManager()
    {

    }

    public void ToHistoricSiteManager()
    {

    }

    public class GMBTInfo
    {
        //public GoodsCount goodsCount;

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

        private ulong jewelryCount;
        public ulong JewelryCount
        {
            get { return jewelryCount; }
            set { jewelryCount = value; }
        }

        public GMBTInfo(ulong goodsCount1, ulong goodsCount2, ulong goodsCount3)
        {
            //goodsCount = new GoodsCount();

            //goodsCount[0] = goodsCount1;
            //goodsCount[1] = goodsCount2;
            //goodsCount[2] = goodsCount3;

            this.foodCount = goodsCount1;
            this.goldAmount = goodsCount2;
            this.jewelryCount = goodsCount3;
        }
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
