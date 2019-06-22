using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectHero : MonoBehaviour
{
    public GameObject SelectedCharacterImage;
    public static int selected = 0;
    public Text[] Texts;
    public GameObject[] specialReinforceCharacterBtn;
    public Button[] tabBtn;
    public int maxReinforce;

    void Start()
    {
        ChangeData();
        tabBtn[1].enabled = false;
        if (GameManager.Instance.specialReinforceCheck == true)
        {
            tabBtn[1].transform.Find("TabLockPanel").gameObject.SetActive(false);
            tabBtn[1].enabled = true;
        }
    }

    public void Selected(int select)
    {
       selected = select;
    }

    public void ChangeData()
    {
        SelectedCharacterImage.GetComponent<Image>().sprite = GameManager.Instance.characterList[selected].CharacterImage;
        SelectedCharacterImage.GetComponent<Image>().preserveAspect = true;
        ChangeInfo();
    }

    public void ChangeInfo()
    {
        // 첫 번째에는 아무런 에러가 나지않으나 강화 후 불러오면 인덱스 아웃 오브 레인지가 나온다
        Debug.Log(selected);
        float Reinforce = GameManager.Instance.characterList[selected].Reinforcement;
        //Debug.Log(Texts[0].GetComponent<Text>().text);
        Texts[0].text = "강화      : " + GameManager.Instance.characterList[selected].Reinforcement.ToString() + "    ->   " + (Reinforce + 1).ToString();
        Texts[1].text = "공격력  : " + GameManager.Instance.characterList[selected].AttackDamage.ToString() + "   ->   " + (GameManager.Instance.characterList[selected].AttackDamage * 2).ToString();
        Texts[2].text = "스킬 1   : " + GameManager.Instance.characterList[selected].SkilDamage[0].ToString() + "   ->   " + (GameManager.Instance.characterList[selected].SkilDamage[0] * 2).ToString();
        Texts[3].text = "스킬 2   : " + GameManager.Instance.characterList[selected].SkilDamage[1].ToString() + " ->   " + (GameManager.Instance.characterList[selected].SkilDamage[1] * 2).ToString();
        Texts[4].text = "스킬 3   : " + GameManager.Instance.characterList[selected].SkilDamage[2].ToString() + " ->   " + (GameManager.Instance.characterList[selected].SkilDamage[2] * 2).ToString();
        Texts[5].text = "HP        : " + GameManager.Instance.characterList[selected].Hp.ToString() + "   ->   " + (GameManager.Instance.characterList[selected].Hp * 2).ToString();
    }

    public void Reinforce()
    {   // 성공과 실패를 구분해야함 성공할 경우 강화값이 올라가지만 실패할 경우 그대로 유지 되어야 함 -> 일단은 성공으로만 해놓자
        // 성공 확률을 강화 단계가 올라갈수록 낮아지게 해야함 필요한 알고리즘을 생각해보자
        if(GameManager.Instance.characterList[selected].Reinforcement == maxReinforce)
        {
            return;
        }
            
        GameManager.Instance.characterList[selected].Reinforcement++;
        GameManager.Instance.characterList[selected].AttackDamage = (float)(GameManager.Instance.characterList[selected].AttackDamage * 2);
        GameManager.Instance.characterList[selected].SkilDamage[0] = (float)(GameManager.Instance.characterList[selected].SkilDamage[0] * 2);
        GameManager.Instance.characterList[selected].SkilDamage[1] = (float)(GameManager.Instance.characterList[selected].SkilDamage[1] * 2);
        GameManager.Instance.characterList[selected].SkilDamage[2] = (float)(GameManager.Instance.characterList[selected].SkilDamage[2] * 2);
        GameManager.Instance.characterList[selected].Hp = (float)(GameManager.Instance.characterList[selected].Hp * 2);
        Debug.Log(selected);

        ChangeInfo();

        if (GameManager.Instance.characterList[selected].Reinforcement == maxReinforce)
        {
            GameManager.Instance.characterList[selected].maxReinforceCheck = true;
            GameManager.Instance.specialReinforceCheck = true;
            tabBtn[1].transform.Find("TabLockPanel").gameObject.SetActive(false);
            tabBtn[1].enabled = true;
        }
    }
}
