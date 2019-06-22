using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DungeonToMain : MonoBehaviour
{
    public void ToMain()
    {
        Time.timeScale = 1.0f;
        DungeonManager.Instance.DungeonToMain();
        GameManager.Instance.BetweenDungeon = true;
        SceneManager.LoadScene("ClickerGame");
    }
}
