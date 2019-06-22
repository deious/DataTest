using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ArtifactReinforce : MonoBehaviour
{
    // 강화 현황, 강화 비용, 강화 수치 start시에 초기화
    public Text[] artifactReinforce;
    // todo:
    // 버튼에 있는 것을 조정할 것인지, 아니면 따로 텍스트를 활용할 것인지
    public Text[] artifactReinforceGold;
    public Text[] artifactAbility;
    public Button[] artifactBtn;
    // Start is called before the first frame update
    // todo: 
    // 잠금 표시를 할것인지, 아니면 추가하는 식으로 갈지
    void Start()
    {
        // init reinforce
        for(int i =0; i<GameManager.Instance.artifactInfo.artifactActive.Length; i++)
        {
            if(GameManager.Instance.artifactInfo.artifactActive[i])
            {
                artifactReinforce[i].text = GameManager.Instance.artifactInfo.artifactUpgrade[i].ToString();
                artifactReinforceGold[i].text = (GameManager.Instance.artifactInfo.artifactUpgradePrice[i] * GameManager.Instance.artifactInfo.artifactUpgrade[i]).ToString();
                artifactAbility[i].text = GameManager.Instance.artifactInfo.artifactEnhance[i].ToString();
            }
        }
        // init reinforce gold
        // init ability
    } 
    //
    public void ReinforceToArtifact(int selected)
    {
        long money;
        long.TryParse(GameManager.Instance.goodsText[1].ToString(), out money);
        if (money >= (long)(GameManager.Instance.artifactInfo.artifactUpgradePrice[selected] * GameManager.Instance.artifactInfo.artifactUpgrade[selected]))
        {
            artifactReinforce[selected].text = (++GameManager.Instance.artifactInfo.artifactUpgrade[selected]).ToString();
            artifactReinforceGold[selected].text = (GameManager.Instance.artifactInfo.artifactUpgradePrice[selected] * GameManager.Instance.artifactInfo.artifactUpgrade[selected]).ToString();
            artifactAbility[selected].text = GameManager.Instance.artifactInfo.artifactEnhance[selected].ToString();
        }
    }
}
