using Firebase;
using Firebase.Database;
using Firebase.Unity.Editor;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirebaseManager : MonoBehaviour
{
    private static FirebaseManager instance;
    public static FirebaseManager Instance
    {
        get { return instance; }
    }
    //public static FirebaseManager Instance
    //{
    //    get
    //    {
    //        if (instance == null)
    //        {
    //            GameObject tempFM = new GameObject("FireManager");
    //            instance = tempFM.AddComponent<FirebaseManager>();
    //            DontDestroyOnLoad(tempFM);
    //        }
    //        return instance;
    //    }
    //}
    string UDID;

    void Awake()
    {
        if (instance)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
        UDID = SystemInfo.deviceUniqueIdentifier;

        DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
        goodsCount = new int[3];

        FirebaseApp.CheckAndFixDependenciesAsync().ContinueWith(task =>
        {
            var dependencyStatus = task.Result;
            if (dependencyStatus == DependencyStatus.Available)
                InitializeFirebase();
            else
                Debug.LogError(string.Format(
                  "Could not resolve all Firebase dependencies: {0}", dependencyStatus));
        });
    }

    void InitializeFirebase()
    {
        FirebaseApp app = FirebaseApp.DefaultInstance;
        app.SetEditorDatabaseUrl("https://pathfinder-77faf.firebaseio.com/");
        if (app.Options.DatabaseUrl != null)
            app.SetEditorDatabaseUrl(app.Options.DatabaseUrl);
    }

    public void StartDungeon()
    {

    }

    public void StartFarm(int[][] cropCount)
    {
        this.cropCount = cropCount;
    }

    public void StartHistoricSite()
    {

    }

    int[] goodsCount;

    int[][] cropCount;

    int[] heroUpgrade;

    public void SaveData()
    {
        DatabaseReference reference = FirebaseDatabase.DefaultInstance.GetReference(UDID);

        reference.RunTransaction(SaveDataTransaction).ContinueWith(task =>
        {
            if (task.Exception != null)
                Debug.Log(task.Exception.ToString());
            else if (task.IsCompleted)
            {
                Debug.Log("Transaction complete.");
            }
        });
    }

    TransactionResult SaveDataTransaction(MutableData mutableData)
    {
        Dictionary<string, object> datas = mutableData.Value as Dictionary<string, object>;
        if (datas == null) datas = new Dictionary<string, object>();

        Dictionary<string, object> newData = new Dictionary<string, object>();
        newData["Food"] = 10;
        newData["Gold"] = 20;
        newData["Jewelry"] = 30;

        ChangeData(ref datas, newData);

        mutableData.Value = datas;
        return TransactionResult.Success(mutableData);
    }

    //TransactionResult SaveDataTransaction(MutableData mutableData)
    //{
    //    Dictionary<string, object> datas = mutableData.Value as Dictionary<string, object>;
    //    if (datas == null) datas = new Dictionary<string, object>();

    //    Dictionary<string, object> newData = new Dictionary<string, object>();
    //    newData["Food"] = (int)goodsCount[0];
    //    newData["Gold"] = (int)goodsCount[1];
    //    newData["Jewelry"] = (int)goodsCount[2];

    //    ChangeData(ref datas, newData);
    //    //datas["Food"] = (int)goodsCount[0];
    //    //datas["Gold"] = (int)goodsCount[1];
    //    //datas["Jewelry"] = (int)goodsCount[2];

    //    mutableData.Value = datas;
    //    return TransactionResult.Success(mutableData);
    //}

    void ChangeData(ref Dictionary<string, object> originalData, Dictionary<string, object> newData)
    {
        foreach (KeyValuePair<string, object> data in newData)
            originalData[data.Key] = data.Value;
    }

    public void SaveFood(int foodCount)
    {
        goodsCount[0] = foodCount;

        DatabaseReference reference = FirebaseDatabase.DefaultInstance.GetReference(UDID);

        reference.RunTransaction(SaveFoodTransaction).ContinueWith(task =>
        {
            if (task.Exception != null)
                Debug.Log(task.Exception.ToString());
            else if (task.IsCompleted)
            {
                Debug.Log("Transaction complete.");
            }
        });
    }

    TransactionResult SaveFoodTransaction(MutableData mutableData)
    {
        Dictionary<string, object> datas = mutableData.Value as Dictionary<string, object>;
        if (datas == null) datas = new Dictionary<string, object>();

        datas["Food"] = goodsCount[0];

        mutableData.Value = datas;
        return TransactionResult.Success(mutableData);
    }

    public void SaveMoney(int moneyAmount)
    {
        goodsCount[1] = moneyAmount;

        DatabaseReference reference = FirebaseDatabase.DefaultInstance.GetReference(UDID);

        reference.RunTransaction(SaveMoneyTransaction).ContinueWith(task =>
        {
            if (task.Exception != null)
                Debug.Log(task.Exception.ToString());
            else if (task.IsCompleted)
            {
                Debug.Log("Transaction complete.");
            }
        });
    }

    TransactionResult SaveMoneyTransaction(MutableData mutableData)
    {
        Dictionary<string, object> datas = mutableData.Value as Dictionary<string, object>;
        if (datas == null) datas = new Dictionary<string, object>();

        datas["Money"] = goodsCount[1];

        mutableData.Value = datas;
        return TransactionResult.Success(mutableData);
    }

    public void SaveJewelry(int jewelryCount)
    {
        goodsCount[2] = jewelryCount;

        DatabaseReference reference = FirebaseDatabase.DefaultInstance.GetReference(UDID);

        reference.RunTransaction(SaveJewelryTransaction).ContinueWith(task =>
        {
            if (task.Exception != null)
                Debug.Log(task.Exception.ToString());
            else if (task.IsCompleted)
            {
                Debug.Log("Transaction complete.");
            }
        });
    }

    TransactionResult SaveJewelryTransaction(MutableData mutableData)
    {
        Dictionary<string, object> datas = mutableData.Value as Dictionary<string, object>;
        if (datas == null) datas = new Dictionary<string, object>();

        datas["Jewelry"] = goodsCount[2];

        mutableData.Value = datas;
        return TransactionResult.Success(mutableData);
    }

    public void SaveCrops()
    {
        cropCount = FarmManager.Instance.CropCount;

        DatabaseReference reference = FirebaseDatabase.DefaultInstance.GetReference(UDID);

        reference.RunTransaction(SaveCropsTransaction).ContinueWith(task =>
        {
            if (task.Exception != null)
                Debug.Log(task.Exception.ToString());
            else if (task.IsCompleted)
            {
                Debug.Log("Transaction complete.");
            }
        });
    }

    TransactionResult SaveCropsTransaction(MutableData mutableData)
    {
        Dictionary<string, object> datas = mutableData.Value as Dictionary<string, object>;
        if (datas == null) datas = new Dictionary<string, object>();

        Dictionary<string, object> cropData = new Dictionary<string, object>();
        for (int i = 0; i < cropCount.Length; i++)
            for (int j = 0; j < cropCount[i].Length; j++)
                cropData[(i + 1).ToString() + "_" + (j + 1).ToString()] = cropCount[i][j];
        datas["Crops"] = cropData;

        mutableData.Value = datas;
        return TransactionResult.Success(mutableData);
    }

    public void SaveHeros()
    {
        heroUpgrade = RelicsManager.Instance.relicMainHero.heroUpgrade;

        DatabaseReference reference = FirebaseDatabase.DefaultInstance.GetReference(UDID);

        reference.RunTransaction(SaveHerosTransaction).ContinueWith(task =>
        {
            if (task.Exception != null)
                Debug.Log(task.Exception.ToString());
            else if (task.IsCompleted)
            {
                Debug.Log("Transaction complete.");
            }
        });
    }

    TransactionResult SaveHerosTransaction(MutableData mutableData)
    {
        Dictionary<string, object> datas = mutableData.Value as Dictionary<string, object>;
        if (datas == null) datas = new Dictionary<string, object>();

        Dictionary<string, object> heroData = new Dictionary<string, object>();
        for (int i = 0; i < heroUpgrade.Length; i++)
            heroData[(i + 1).ToString()] = heroUpgrade[i];
        datas["Hero"] = heroData;

        mutableData.Value = datas;
        return TransactionResult.Success(mutableData);
    }
}
