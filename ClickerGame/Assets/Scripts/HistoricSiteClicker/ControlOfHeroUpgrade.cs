using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;


public class ControlOfHeroUpgrade : MonoBehaviour {
    public Button[] upBtn;
    public Text[] upText;
    //public GameObject moneyText;

    int selectUpgradeButton;

    // Use this for initialization
    //void Start()
    //{
    //    //  todo : 강화에 따른 현황 및 해금 변경
    //    //upBtn.text = "구매";
    //}

    //  unit upgrade
    public void OnUpgrade(int selectUpgradeButton)
    {
        this.selectUpgradeButton = selectUpgradeButton;

        long money = (long)RelicsManager.Instance.goldAmount;
        long cost = RelicsManager.Instance.relicMainHero.heroUpgradePrice[selectUpgradeButton] * (RelicsManager.Instance.relicMainHero.heroUpgrade[selectUpgradeButton] + 1);

        Debug.Log(money + "," + cost);
        if (money >= cost)
        {
            Debug.Log("Upgrade success");
            Upgrade(cost);
        }
        else
        {
            Debug.Log("Upgrade fail");
        }
    }
    //  강화
    void Upgrade(long cost)
    {
        RelicsManager.Instance.UseGold(cost);
        // 강화 수치 증가
        RelicsManager.Instance.relicMainHero.heroUpgrade[selectUpgradeButton]++;
        // 변경된 강화 비용 적용

        /*
         * hero upgrade에 관한 함수
         * - 기본 공격
         * - 크리티컬 발동 확률
         * - 크리티컬 
         * 차후 각 기능별 레벨 디자인이 달라짐으로, 구분 
         */
        switch (selectUpgradeButton)
        {
            // heroattacktype[0]
            case 0:
                NormalAttackDamageUpgrade();
                break;
            // heroattacktype[1]
            case 1:
                CriticalProbabilityUpgrade();
                break;
            // heroattacktype[2]
            case 2:
                CriticalDamageUpgrade();
                break;
            default:
                break;
        }
    }

    // todo: 레벨디자인이 필요함
    //  hero attack damage increase
    void NormalAttackDamageUpgrade()
    {
        RelicsManager.Instance.inHeroAttackType[selectUpgradeButton] =
            RelicsManager.Instance.relicMainHero.heroAttactType[selectUpgradeButton] * (RelicsManager.Instance.relicMainHero.heroUpgrade[selectUpgradeButton] + 1);
    }
    //  hero critical probability increase
    void CriticalProbabilityUpgrade()
    {
        RelicsManager.Instance.inHeroAttackType[selectUpgradeButton] =
            RelicsManager.Instance.relicMainHero.heroAttactType[selectUpgradeButton] * (RelicsManager.Instance.relicMainHero.heroUpgrade[selectUpgradeButton] + 1);
    }
    //  hero critical damage increase
    void CriticalDamageUpgrade()
    {
        RelicsManager.Instance.inHeroAttackType[selectUpgradeButton] =
            RelicsManager.Instance.relicMainHero.heroAttactType[selectUpgradeButton] * (RelicsManager.Instance.relicMainHero.heroUpgrade[selectUpgradeButton] + 1);
    }
}
