using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChange : MonoBehaviour
{
    public int selected;

    public void ChanageScene()
    {
        SceneManager.LoadScene(selected);
    }
}
