using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ControlOfRelicsUpgrade : MonoBehaviour
{
    public Button[] upBtn;
    public Text[] upText;
    //public GameObject moneyText;

    int selectItem;
    public void OnUpgrade(int selectItem)
    {
        this.selectItem = selectItem;

        long money = (long)RelicsManager.Instance.goldAmount;
        long cost = RelicsManager.Instance.relicBuilding.relicUpgradePrice[selectItem] * (RelicsManager.Instance.relicBuilding.relicUpgrade[selectItem] + 1);

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
    void Upgrade(long cost)
    {
        RelicsManager.Instance.UseGold(cost);
        // 강화 수치 증가
        RelicsManager.Instance.relicBuilding.relicUpgrade[selectItem]++;
        // 변경된 강화 비용 적용
        switch (selectItem)
        {
            // enhance
            case 0:
                EhanceUpgrade();
                break;
            // weaken
            case 1:
                WeakenUpgrade();
                break;
            default:
                break;
        }
    }

    /*
     * 유적 강화
     * todo : 차후 수치에 따른 변화 적용
     */
    // ehance : 유적의 영향을 감소, 유닛의 능력 증가
    void EhanceUpgrade()
    {
        RelicsManager.Instance.inRelicType[selectItem] =
            RelicsManager.Instance.relicBuilding.relicType[selectItem] * (RelicsManager.Instance.relicBuilding.relicUpgrade[selectItem] + 1);
        //Debug.Log("Upgrade check " + RelicsManager.Instance.relicBuilding.relicUpgrade[selectItem]);
    }
    // weaken : 유적을 약화시켜, 유적의 체력 감소
    void WeakenUpgrade()
    {
        RelicsManager.Instance.inRelicType[selectItem] =
            RelicsManager.Instance.relicBuilding.relicType[selectItem] * (RelicsManager.Instance.relicBuilding.relicUpgrade[selectItem] + 1);
    }

}
