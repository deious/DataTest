using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MapController : MonoBehaviour
{
    public Button[] mainMap;
    public Button[] subStage;
    public GameObject mainMapImage;
    public Sprite[] mainImage;
    public Button[] difficultybtn;
    public Button Startbtn;
    public GameObject WarningPanel;
    public bool diff;

    int mainMapSelected;
    int temp;
    int mainTemp = 0;
    int subTemp = 0;
    int clearCount;

    void Start()
    {
        clearCount = GameManager.Instance.stageControll.clearCount;
        temp = clearCount;
        difficultybtn[1].enabled = false;
        NormalChange(1);
    }

    public void MainMapClick(int selected)
    {
        GameManager.Instance.stageControll.MainSelected = selected;
        SubLockCheck(selected);
        ChangeMainColor(selected);
        ChangeMapImage(selected);
        ChangeText(selected);
        if (selected != clearCount / 10)
        {
            SubStageClick(0);
        }
        else
        {
            SubStageClick(clearCount%10);
        }
    }

    public void SubStageClick(int selected)
    {
        ChangeSubColor(selected);
        GameManager.Instance.stageControll.SubSelected = selected;
    }

    public void NormalChange(int selected)
    {
        diff = true;
        clearCount = temp;
        ChangeNormalColor();
        
        if (clearCount >= 70)
        {
            clearCount = temp;
            MainLockCheck();
            MainMapClick(mainMap.Length - 1);
            SubStageClick(subStage.Length - 1);
        }
        else
        {
            Debug.Log(clearCount);
            MainLockCheck();
            clearCount = temp;
            MainMapClick(clearCount / 10);
            SubStageClick(clearCount % 10);
        }
        GameManager.Instance.stageControll.DifficultySelected = selected;
    }

    public void HardChange(int selected)
    {
        diff = false;
        ChangeHardColor();
        MainLockCheck();
        MainMapClick(clearCount/10);
        SubStageClick(clearCount % 10);
        GameManager.Instance.stageControll.DifficultySelected = selected;
    }

    public void MainLockCheck()
    {
        if (clearCount >= 70)
        {
            //difficultybtn[1].transform.GetChild(2).gameObject.SetActive(false);
            difficultybtn[1].transform.Find("HardLevelLockPanel").gameObject.SetActive(false);
            difficultybtn[1].enabled = true;
            if(diff == true)
            {
                for (int i = 0; i < mainMap.Length; i++)
                {
                    //mainMap[i].transform.GetChild(2).gameObject.SetActive(false);
                    mainMap[i].transform.Find("MainMapLockPanel").gameObject.SetActive(false);
                    mainMap[i].enabled = true;
                }
            }
            else
            {
                clearCount -= 70;
                for(int i=0; i<= clearCount/10; i++)
                {
                    //mainMap[i].transform.GetChild(2).gameObject.SetActive(false);
                    mainMap[i].transform.Find("MainMapLockPanel").gameObject.SetActive(false);
                }

                for(int i = (clearCount / 10) + 1; i<mainMap.Length; i++)
                {
                    mainMap[i].enabled = false;
                    mainMap[i].transform.Find("MainMapLockPanel").gameObject.SetActive(true);
                    //mainMap[i].transform.GetChild(2).gameObject.SetActive(true);
                }
            }
        }
        else
        {
            Debug.Log(clearCount);
            for (int i = 0; i < (clearCount / 10) + 1; i++)
            {
                mainMap[i].enabled = true;
                mainMap[i].transform.Find("MainMapLockPanel").gameObject.SetActive(false);
                //mainMap[i].transform.GetChild(2).gameObject.SetActive(false);
            }

            for (int i = (clearCount / 10) + 1; i < mainMap.Length; i++)
            {
                mainMap[i].enabled = false;
                mainMap[i].transform.Find("MainMapLockPanel").gameObject.SetActive(true);
                //mainMap[i].transform.GetChild(2).gameObject.SetActive(true);
            }
        }
    }

    public void SubLockCheck(int mainMapSelected)
    {   
        if(mainMapSelected < (clearCount/10))
        {
            for (int i = 0; i < subStage.Length; i++)
            {
                //subStage[i].transform.GetChild(4).gameObject.SetActive(false);
                subStage[i].transform.Find("SubStageLockPanel").gameObject.SetActive(false);
                subStage[i].enabled = true;
            }
        }
        else
        {
            for(int i = 0; i < (clearCount%10)+1; i++)
            {
                //subStage[i].transform.GetChild(4).gameObject.SetActive(false);
                subStage[i].transform.Find("SubStageLockPanel").gameObject.SetActive(false);
            }
            for(int i = (clearCount % 10) + 1; i<subStage.Length; i++)
            {
                subStage[i].enabled = false;
                subStage[i].transform.Find("SubStageLockPanel").gameObject.SetActive(true);
                //subStage[i].transform.GetChild(4).gameObject.SetActive(true);
            }
        }
    }

    public void StartCheck()
    {
        int difficulty = GameManager.Instance.stageControll.DifficultySelected;
        if (GameManager.Instance.stageControll.MainSelected > 6)
        {
            WarningPanel.SetActive(true);
            WarningPanel.transform.Find("Text").GetComponent<Text>().text = "메인 맵이 올바르지 않습니다";
            //WarningPanel.transform.GetChild(0).GetComponent<Text>().text = "메인 맵이 올바르지 않습니다";
            return;
        }
        else if(GameManager.Instance.stageControll.SubSelected > 9)
        {
            WarningPanel.SetActive(true);
            WarningPanel.transform.Find("Text").GetComponent<Text>().text = "서브스테이지가 올바르지 않습니다";
            //WarningPanel.transform.GetChild(0).GetComponent<Text>().text = "서브스테이지가 올바르지 않습니다";
            return;
        }
        else if(!((difficulty ==1)||(difficulty == 2)))
        {
            WarningPanel.SetActive(true);
            WarningPanel.transform.Find("Text").GetComponent<Text>().text = "난이도가 선택되지 않았습니다";
            //WarningPanel.transform.GetChild(0).GetComponent<Text>().text = "난이도가 선택되지 않았습니다";
            return;
        }
        else
        {
            ChanageScene();
        }

    }

    void ChanageScene()
    {
        SceneManager.LoadScene(1);
    }

    void ChangeText(int n)
    {
        for(int i = 0; i<subStage.Length; i++)
        {
            subStage[i].transform.GetChild(2).GetComponent<Text>().text = "Stage " + (n+1) + "-" + (i+1);
        }
    }

    void ChangeMainColor(int selected)
    {
        //mainMap[mainTemp].transform.GetChild(0).GetComponent<Image>().color = new Color(1f, 1f, 1f, 1f);
        mainMap[mainTemp].transform.Find("Background").GetComponent<Image>().color = new Color(1f, 1f, 1f, 1f);
        mainTemp = selected;
        mainMap[mainTemp].transform.Find("Background").GetComponent<Image>().color = new Color(0.500f, 0.500f, 0.500f, 1f);
        //mainMap[mainTemp].transform.GetChild(0).GetComponent<Image>().color = new Color(0.500f, 0.500f, 0.500f, 1f);
    }

    void ChangeSubColor(int selected)
    {
        //subStage[subTemp].transform.GetChild(0).GetComponent<Image>().color = new Color(1f, 1f, 1f, 1f);
        subStage[subTemp].transform.Find("Background").GetComponent<Image>().color = new Color(1f, 1f, 1f, 1f);
        subTemp = selected;
        subStage[subTemp].transform.Find("Background").GetComponent<Image>().color = new Color(0.500f, 0.500f, 0.500f, 1f);
        //subStage[subTemp].transform.GetChild(0).GetComponent<Image>().color = new Color(0.500f, 0.500f, 0.500f, 1f);;
    }

    void ChangeNormalColor()
    {
        //difficultybtn[0].transform.GetChild(0).GetComponent<Image>().color = new Color(0.500f, 0.500f, 0.500f, 1f);
        difficultybtn[0].transform.Find("Background").GetComponent<Image>().color = new Color(0.500f, 0.500f, 0.500f, 1f);
        difficultybtn[1].transform.Find("Background").GetComponent<Image>().color = new Color(1f, 1f, 1f, 1f);
        //difficultybtn[1].transform.GetChild(0).GetComponent<Image>().color = new Color(1f, 1f, 1f, 1f);
    }

    void ChangeHardColor()
    {
        //difficultybtn[1].GetComponentInChildren<Image>().color = new Color(0.500f, 0.500f, 0.500f, 1f);   // 자식오브젝트가 아니라 자기자신에게 적용되어버림
        //difficultybtn[0].GetComponentInChildren<Image>().color = new Color(1f, 1f, 1f, 1f);
        //difficultybtn[1].transform.GetChild(0).GetComponent<Image>().color = new Color(0.500f, 0.500f, 0.500f, 1f);
        //difficultybtn[0].transform.GetChild(0).GetComponent<Image>().color = new Color(1f, 1f, 1f, 1f);
        difficultybtn[1].transform.Find("Background").GetComponent<Image>().color = new Color(1f, 1f, 1f, 1f);
        difficultybtn[0].transform.Find("Background").GetComponent<Image>().color = new Color(0.500f, 0.500f, 0.500f, 1f);
    }

    void ChangeMapImage(int selected)
    {
        mainMapImage.GetComponent<Image>().sprite = mainImage[selected];
    }
}
