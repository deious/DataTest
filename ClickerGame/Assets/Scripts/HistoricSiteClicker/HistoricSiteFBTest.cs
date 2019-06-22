using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HistoricSiteFBTest : MonoBehaviour
{
    void Start()
    {
        gameObject.GetComponent<Button>().onClick.AddListener(ButtonClick);
    }

    void ButtonClick()
    {
        FirebaseManager.Instance.SaveHeros();
    }
}
