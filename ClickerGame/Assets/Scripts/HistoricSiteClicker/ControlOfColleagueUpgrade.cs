using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ControlOfColleagueUpgrade : MonoBehaviour {
    public GameObject[] upBtn;
    public Text[] upText;

    // 차후 moneyText가 아닌 manager에서 가져와서 사용하는 방식으로 적용
    //public GameObject moneyText;


    public void ColleagueUpgrade(int selectColleague)
    {
        long money = (long)RelicsManager.Instance.goldAmount;
        long cost = 
            RelicsManager.Instance.relicColleagues.colleagueUpgradePrice[selectColleague] * (RelicsManager.Instance.relicColleagues.colleagueUpgrade[selectColleague] + 1);

        if (money >= cost)
        {
            RelicsManager.Instance.UseGold(cost);
            //  강화 횟수 증가
            RelicsManager.Instance.relicColleagues.colleagueUpgrade[selectColleague]++;
            RelicsManager.Instance.inColleagueDamage[selectColleague] =
                RelicsManager.Instance.relicColleagues.colleagueDamage[selectColleague] * (RelicsManager.Instance.relicColleagues.colleagueUpgrade[selectColleague] + 1);
        }
        else
        {
            Debug.Log("Upgrade fail");
        }
    }
}
