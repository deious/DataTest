using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeTest : MonoBehaviour
{
    void Start()
    {
        gameObject.GetComponent<Button>().onClick.AddListener(Select);
    }

    void Select()
    {
        if (gameObject.name.Equals("TestBtn1"))
        {
            GameManager.Instance.CountChange = 100;
            GameManager.Instance.Accumulate(0);
        }
        else if (gameObject.name.Equals("TestBtn2"))
        {
            GameManager.Instance.CountChange = 100;
            GameManager.Instance.Consume(0);
        }
        else if (gameObject.name.Equals("TestBtn3"))
        {
            GameManager.Instance.CountChange = 100;
            GameManager.Instance.Accumulate(1);
        }
        else if (gameObject.name.Equals("TestBtn4"))
        {
            GameManager.Instance.CountChange = 100;
            GameManager.Instance.Consume(1);
        }
        else if (gameObject.name.Equals("TestBtn5"))
        {
            GameManager.Instance.CountChange = 100;
            GameManager.Instance.Accumulate(2);
        }
        else if (gameObject.name.Equals("TestBtn6"))
        {
            GameManager.Instance.CountChange = 100;
            GameManager.Instance.Consume(2);
        }
    }
}
