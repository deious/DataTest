using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// TestManager : 각 Scene마다 가진 Manager라고 가정하고 테스트
public class TestManager : MonoBehaviour
{
    private static TestManager instance;
    public static TestManager Instance
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

    GameManager.GMBTInfo infos;

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
        // GameManager가 가지고 있는 공통 정보를 덤프뜸
        infos = GameManager.Instance.ToTestManager();

        // 고유한 정보들을 DB에서 가져옴
        // 초기화가 필요한 것들은 초기화
        testInt = 10;
        testString = "test";
    }

    // 각 씬마다 필요한 정보들 중 공통 정보를 여기서 가지고 있음
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

    // 각 씬마다 가지는 고유한 정보 또한 여기에 있음
    private int testInt;
    public int TestInt
    {
        get { return testInt; }
        set { testInt = value; }
    }

    private string testString;
    public string TestString
    {
        get { return testString; }
        set { testString = value; }
    }

    // Manager간 통신(방법2)
    // 해당 Scene에서 필요한 정보를 미리 받고, 그 정보를 사용
    class GMBTInfo
    {

    }

    public void Print()
    {
        Debug.Log(string.Format("Food Count : {0}\n" +
            "Gold Amount : {1}\n" +
            "Jewelry Count : {2}\n" +
            "Test Int : {3}\n" +
            "Test String : {4}\n", infos.FoodCount, infos.GoldAmount, infos.JewelryCount,
            testInt, testString));
    }
}
