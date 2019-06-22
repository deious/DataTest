using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArtifactChangeBtn : MonoBehaviour
{
    public GameObject[] panel;
    int selected;
    int temp;

    public void Select(int selected)
    {
        this.selected = selected;
        Change();
    }

    void Change()
    {
        panel[temp].SetActive(false);
        panel[selected].SetActive(true);
        temp = selected;
    }
}
