using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;




public class SceneMng : Mng<SceneMng>
{
    // 로드되고 있는 상태를 가리킬 상태변수
    [SerializeField]
    private bool loading = false;

    // 신을 등록하기 위한 변수
    private Dictionary<SceneType, Scene> sceneDic =
        new Dictionary<SceneType, Scene>();

    // 현재 신
    private SceneType current = SceneType.None;

    public SceneType Current
        { get { return current; } }
    // 전체를 순회하면서 매개변수와 같은 신의 체크박스만
    // 활성화하는 함수


    #region // Mng 추상 메소드 정의부


    public override void Run()
    {
        if ((GameDB.MngEnabled & (int)MngType.SceneMng) != (int)MngType.SceneMng)
            return;
    }

    public override void FixRun()
    {

    }

    public override void LateRun()
    {

    }
    public override void Init()
    {
        mngType = MngType.SceneMng;
        
    }

    public override void OnActive()
    {
      
    }
    public override void OnDeactive()
    {
       
    }
    public override void OnGameEnable()
    {
       
    }
    public override void OnGameDisable()
    {
       
    }

    public override void SetActive(bool state)
    {
        if (state)
        {
            OnActive();
        }
        else
        {
            OnDeactive();
        }
        gameObject.SetActive(state);
    }
    public override void SetEnable(bool state)
    {
        if (state)
        {
            OnGameEnable();
        }
        else
        {
            OnGameDisable();
        }
        enabled = state;
    }

    #endregion



    public void Enable(SceneType scene)
    {
        foreach (var pair in sceneDic)
        {
            if (pair.Key != scene)
            {
                pair.Value.enabled = false;
            }
            else
                pair.Value.enabled = true;
        }
    }
    public T AddScene<T>(SceneType sType, bool state = false)
        where T : Scene
    {
        // 신이 등록되어 있지 않다면 게임 오브젝트를 만들고,
        // 신 스크립트를 연결후 신 매니저에 등록
        if (sceneDic.ContainsKey(sType) == false)
        {
           
            T t = this.CreateObject<T>(transform, true);
            
            t.enabled = state;
            sceneDic.Add(sType, t);
            return t;
        }
        sceneDic[sType].enabled = state;
        // 신이 등록되어 있는 상태라면 찾아서 리턴
        return sceneDic[sType] as T;
    }

    public void Enable(SceneType scene,
                        int idx = 0,
                        bool falseLoading = false,
                        float targetTime = 2.0f)
    {
        // 이동하고자하는 신이 등록된 상태라면 처리
        if (sceneDic.ContainsKey(scene))
        {
            // 해당되는 신의 스크립트만 켜준다.
            Enable(scene);

            // 신을 비동기로 로드
            LoadAsync(scene, idx);
        }
    }
    private IEnumerator IEEnableDelay(float delayTime,
                                         SceneType scene,
                                         int idx = 0,
                                        bool falseLoading = false,
                                        float targetTime = 2.0f)
    {
        
        yield return new WaitForSeconds(delayTime);
        Enable(scene,idx ,falseLoading, targetTime);
    }

    public void EnableDelay(float delayTime,
                            SceneType scene,
                            int idx = 0,
                            bool falseLoading = false,
                            float targetTime = 2.0f)
    {
        
        StartCoroutine(IEEnableDelay(delayTime,
                                    scene, idx,falseLoading, targetTime));
    }

    public void LoadAsync(SceneType sceneType , int idx = 0)
    {
        if (loading)
            return;
        loading = true;
        StartCoroutine(IELoadAsync(sceneType, idx));
    }

    private IEnumerator IELoadAsync(SceneType nextScene, int idx = 0)
    {
        // 비동기로 신을 로드
        // 신을 로드하는 함수를 사용하게 되면, 
        // 신에 배치되어있는 물체들은 삭제
        AsyncOperation operation =
                SceneManager.LoadSceneAsync(nextScene.ToString() + idx);

        

        bool state = false;

        while (state == false)
        {
            if (sceneDic.ContainsKey(nextScene))
                sceneDic[nextScene].Progress(operation.progress);

            // 신이 모두 로드된 상태라면 처리
            if (operation.isDone)
            {
                state = true;
                // 이전 신의 Exit함수를 호출
                if (sceneDic.ContainsKey(current))
                    sceneDic[current].Exit();

                // 변경되는 신의 Enter함수를 호출
                if (sceneDic.ContainsKey(nextScene))
                    sceneDic[nextScene].Enter();
                // 현재 신을 변경합니다.
                current = nextScene;

                loading = false;
            }

            yield return null;
        }
    }

    // 안드로이드등에서 포커스가 맞춰질 경우 호출되는 함수
    private void OnApplicationFocus(bool focus)
    {

    }
    // 안드로이드등에서 홈키를 눌렀을때 호출되는 함수
    private void OnApplicationPause(bool pause)
    {

    }
    // 프로그램이 종료될때 Release함수를 호출
    private void OnApplicationQuit()
    {
        Release();
    }
}
