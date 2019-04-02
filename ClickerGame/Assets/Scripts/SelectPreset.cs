using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectPreset : MonoBehaviour
{
    int index;

    void Start()
    {
        int.TryParse(name.Substring(name.Length - 1, 1), out index);
        index--;
        gameObject.GetComponent<Button>().onClick.AddListener(Select);
    }

    void Select()
    {
        for (int i = 0; i < GameManager.Instance.characters.Length; i++)
            GameManager.Instance.characters[i].SetActive(false);
        GameManager.Instance.characters[index].SetActive(true);

        GameManager.Instance.CharacterPreset = index;
    }
}
