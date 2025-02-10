using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public abstract class Scene : MonoBehaviour
{
    // 신 파일이 로드가 완료되는 시점에 호출되는 함수
    public abstract void Enter();

    // 신 파일이 다른 신 파일로 변경이 완료되었을 때 호출되는 함수
    public abstract void Exit();


    // 파일이 로드되고 있을때의 상황을 보여주기 위한 함수
    // 아래의 함수에서 로딩 ui를 출력
    public abstract void Progress(float progress);

 
}
