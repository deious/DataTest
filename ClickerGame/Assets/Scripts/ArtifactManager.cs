using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ArtifactManager : MonoBehaviour
{
    public Artifact[] artifactData;
    public GameObject[] artifactObject;

    void Start()
    {
        //  artifactData 초기화

        
    }

    //  유물 정보
    //  - 활성화 여부
    //  - 업그레이드 횟수
    //  - 유물 설명
    //  - 유물 버프 타입
    //  - 유물 버프 수치
    //  - 유물 업그레이드 가격
    [System.Serializable]
    public class Artifact
    {
        private bool onActive;
        public bool OnActive
        {
            get { return onActive; }
            set { onActive = value; }
        }

        private uint upgradeCount;
        public uint UpgradeCount
        {
            get { return upgradeCount; }
            set { upgradeCount = value; }
        }
        private string explain;
        string Explain
        {
            get { return explain; }
        }
        private uint reinforceType;
        public uint ReinforceType
        {
            get { return reinforceType; }
        }
        private float reinforceValue;
        public float ReinforceValue
        {
            get { return reinforceValue; }
            set { reinforceValue = value; }
        }
        private uint upgradePrice;
        public uint UpgradePrice
        {
            get { return upgradePrice; }
            set { upgradePrice = value; }
        }

        public Artifact(bool onActive, uint upgradeConut, string explain, uint reinforceType, float reinforceValue, uint upgradePrice)
        {
            this.onActive = onActive;
            this.upgradeCount = upgradeConut;
            this.explain = explain;
            this.reinforceType = reinforceType;
            this.reinforceValue = reinforceValue;
            this.upgradePrice = upgradePrice;
        }
    }

    public void UpgradeArtifact()
    {
        //  활성화 여부 확인
            //  활성화가 안된경우
                //  ActiveArtifact함수 호출
        
        //  업그레이드 버튼을 기준으로, 업그레이드에 필요한 금전 확인
            //  업그레이드가 된경우
                //  UpgradeCount 증가
                //  ReinforeceValue 수치 변경
                //  UpgradePrice 증가

            //  업그레이드가 안된경우
                //  업그레이드 실패 알림
    }
    //  유물 조각을 다 모아야만 내부 패널 조작이 가능하다.
    void ActiveArtifact()
    {
        //  활성화에 필요한 금전 확인
            //  활성화 
                //  활성화 데이터값 true로 변경
                //  비용 변경
       
            //  활성화 실패(금전 부족)
                //  활성화 실패 알림
    }
}
