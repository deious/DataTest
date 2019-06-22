using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class ArtifactGambling : MonoBehaviour
{
    public GameObject[] artifactImage;
    public GameObject[] artifactLock;
    List<int> artifactActiveCount;

    void Start()
    {
        // check artifactActive
        artifactActiveCount = new List<int>();
        for(int i =0; i<artifactImage.Length; i++)
        {
            if(GameManager.Instance.artifactInfo.artifactActive[i])
            {
                artifactImage[i].GetComponent<Image>().color = new Color(255, 255, 255, 255);
                artifactLock[i].SetActive(false);
            }
            else
            {
                artifactActiveCount.Add(i);
            }
        }
        // 
    }
    // todo:
    // 잠금 표시 또는 투명 표시
    public void GamblingArtifact(int needPieceCount)
    {
        uint pieceCount = GameManager.Instance.artifactPieceCount;
        //  활성화에 필요한 유물 조각 개수 확인
        if(pieceCount >= needPieceCount && artifactActiveCount.Count != 0)
        {
            GameManager.Instance.artifactPieceCount -= (uint)needPieceCount;
            // 무작위 뽑기
            // 무작위 선택, 선택된게 활성화 된것이면 다음 유물 활성화
            int randomValue = UnityEngine.Random.Range(0, artifactActiveCount.Count-1);
            GameManager.Instance.artifactInfo.artifactActive[artifactActiveCount[randomValue]] = true;
            artifactImage[artifactActiveCount[randomValue]].GetComponent<Image>().color = new Color(255, 255, 255, 255);
            artifactLock[artifactActiveCount[randomValue]].SetActive(false);
            artifactActiveCount.RemoveAt(randomValue);
            
            // 임시로 setActive true변경
            // todo : 이미지 변화

        }
        // 무작위 뽑기 실패(개수 부족)
        else
        {
            Debug.Log("Gambling fail");
        }
        
    }
}
