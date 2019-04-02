using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TestModal : MonoBehaviour
{
    public GameObject testPanel;

    void Start()
    {
        gameObject.GetComponent<Button>().onClick.AddListener(Test);
    }

    void Test()
    {
        testPanel.SetActive(true);
    }
}
