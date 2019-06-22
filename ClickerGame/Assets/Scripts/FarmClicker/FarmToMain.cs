using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FarmToMain : MonoBehaviour
{
    public void ToMain()
    {
        FarmManager.Instance.FarmToMain();
        GameManager.Instance.BetweenFarm = true;
        SceneManager.LoadScene("ClickerGame");
    }
}
