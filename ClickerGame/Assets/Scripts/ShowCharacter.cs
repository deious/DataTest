using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowCharacter : MonoBehaviour
{
    int index;

    void Start()
    {
        int.TryParse(name.Substring(name.Length - 1, 1), out index);
        index--;
        gameObject.GetComponent<Button>().onClick.AddListener(Show);
    }

    void Show()
    {
        GameManager.Instance.CharacterSelect = index;
    }
}
