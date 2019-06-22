using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterChangeButton : MonoBehaviour
{
    public GameObject[] button;
    int selected;
    int temp;

    public void Select(int selected)
    {
        this.selected = selected;
        Change();
    }

    void Change()
    {
        button[temp].SetActive(false);
        button[selected].SetActive(true);
        temp = selected;
    }
}
