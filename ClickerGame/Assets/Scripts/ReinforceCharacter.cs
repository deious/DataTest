﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

// 구조 변경 예정
public class ReinforceCharacter : MonoBehaviour
{
    public GameObject charcaterScript;
    public Text effectText;

    int presetNum;

    //  todo : 차후 게임 시작할때 조합 확인 후 반영할 수 있도록 변경
    void Start()
    {
        presetNum = 1;
    }

    //  프리셋을 확인
    public void CheckPreset_Buff()
    {
        string presetName = EventSystem.current.currentSelectedGameObject.name;
        int.TryParse(presetName.Substring(presetName.Length - 1, 1), out presetNum);

        CheckCharacter_Buff();
    }

    //  캐릭터 선택시 버프 적용
    public void CheckCharacter_Buff()
    {
        //characterSelect.Test();
        //Debug.Log(character[presetNum - 1].GetComponent<Image>().sprite.name);

          string effect = "buff test character : ";

        if (GameManager.Instance.IsNotCharacterSelect(presetNum - 1))
            for (int i = 0; i < GameManager.Instance.CharacterPicks[presetNum - 1].Length; i++)
            {
                effect += GameManager.Instance.CharacterPicks[presetNum - 1][i];
                if (i != GameManager.Instance.CharacterPicks[presetNum - 1].Length - 1) effect += ", ";
            }
        else
            effect = " set character";
        effectText.text = effect;
    }
}
