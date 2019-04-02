using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestGameManager : MonoBehaviour
{
    private static TestGameManager instance;
    public static TestGameManager Instance
    {
        get { return instance; }
    }

    void Awake()
    {
        if (instance)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;

        //GameManager.CharacterList[] characterList = GameManager.Instance.SendInfo();
        DontDestroyOnLoad(gameObject);
    }
}
