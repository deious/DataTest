using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapSelected : MonoBehaviour
{
    public int selected;

    public void Click(int selected)
    {
        this.selected = selected;
        GameManager.Instance.stageControll.MainSelected = selected;
    }

    public class Map
    {

    }
}
