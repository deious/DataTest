using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectCharacter : MonoBehaviour
{
    public GameObject duplicationWarning;
    public GameObject characterPanel;

    void Start()
    {
        gameObject.GetComponent<Button>().onClick.AddListener(Select);
    }

    void Select()
    {
        int characterPreset = GameManager.Instance.CharacterPreset;
        int characterSelect = GameManager.Instance.CharacterSelect;

        int selected;
        int.TryParse(name.Substring(name.Length - 1, 1), out selected);

        bool repeated = false;

        for (int i = 0; i < GameManager.Instance.CharacterPicks[characterPreset].Length; i++)
            if (GameManager.Instance.CharacterPicks[characterPreset][i] == selected)
            {
                repeated = true;
                break;
            }

        if (repeated && GameManager.Instance.CharacterPicks[characterPreset][characterSelect] != selected)
            duplicationWarning.SetActive(true);
        else
        {
            GameManager.Instance.CharacterImages[characterPreset][characterSelect * 2 + 1].sprite = GetComponentsInChildren<Image>()[1].sprite;
            transform.parent.parent.parent.gameObject.SetActive(false);

            GameManager.Instance.CharacterPicks[characterPreset][characterSelect] = selected;
            characterPanel.GetComponent<ReinforceCharacter>().CheckCharacter_Buff();
        }
    }
}