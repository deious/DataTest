using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HistoricSiteToMain : MonoBehaviour
{
    public void ToMain()
    {
        //R.Instance.FarmToMain();
        Time.timeScale = 1;
        RelicsManager.Instance.HistoricSiteClickerToMain();
        GameManager.Instance.BetweenFarm = true;
        SceneManager.LoadScene("ClickerGame");
    }
}
