using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FBTest : MonoBehaviour
{
    void Start()
    {
        gameObject.GetComponent<Button>().onClick.AddListener(Click);
    }

    void Click()
    {
        FirebaseManager.Instance.SaveFood(25);
    }
}
