using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Option : MonoBehaviour
{
    public GameObject panel;

    public void OpenPanel()
    {
        panel.SetActive(true);
        Time.timeScale = 0.0f;
    }

    public void Continue()
    {
        panel.SetActive(false);
        Time.timeScale = 1.0f;
    }

    public void Exit()
    {
        SceneManager.LoadScene("MainName");
    }
}
