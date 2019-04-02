using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeViewPanel : MonoBehaviour
{
    public GameObject[] Panel;
    public int selected;
    int temp;

    public void Select(int selected)
    {
        this.selected = selected;
        Change();
    }

    void Change()
    {
        Panel[temp].SetActive(false);
        Panel[selected].SetActive(true);
        temp = selected;
    }
}
