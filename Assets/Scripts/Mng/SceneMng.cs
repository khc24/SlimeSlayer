using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;




public class SceneMng : Mng<SceneMng>
{
    // �ε�ǰ� �ִ� ���¸� ����ų ���º���
    [SerializeField]
    private bool loading = false;

    // ���� ����ϱ� ���� ����
    private Dictionary<SceneType, Scene> sceneDic =
        new Dictionary<SceneType, Scene>();

    // ���� ��
    private SceneType current = SceneType.None;

    public SceneType Current
        { get { return current; } }
    // ��ü�� ��ȸ�ϸ鼭 �Ű������� ���� ���� üũ�ڽ���
    // Ȱ��ȭ�ϴ� �Լ�


    #region // Mng �߻� �޼ҵ� ���Ǻ�


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
        // ���� ��ϵǾ� ���� �ʴٸ� ���� ������Ʈ�� �����,
        // �� ��ũ��Ʈ�� ������ �� �Ŵ����� ���
        if (sceneDic.ContainsKey(sType) == false)
        {
           
            T t = this.CreateObject<T>(transform, true);
            
            t.enabled = state;
            sceneDic.Add(sType, t);
            return t;
        }
        sceneDic[sType].enabled = state;
        // ���� ��ϵǾ� �ִ� ���¶�� ã�Ƽ� ����
        return sceneDic[sType] as T;
    }

    public void Enable(SceneType scene,
                        int idx = 0,
                        bool falseLoading = false,
                        float targetTime = 2.0f)
    {
        // �̵��ϰ����ϴ� ���� ��ϵ� ���¶�� ó��
        if (sceneDic.ContainsKey(scene))
        {
            // �ش�Ǵ� ���� ��ũ��Ʈ�� ���ش�.
            Enable(scene);

            // ���� �񵿱�� �ε�
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
        // �񵿱�� ���� �ε�
        // ���� �ε��ϴ� �Լ��� ����ϰ� �Ǹ�, 
        // �ſ� ��ġ�Ǿ��ִ� ��ü���� ����
        AsyncOperation operation =
                SceneManager.LoadSceneAsync(nextScene.ToString() + idx);

        

        bool state = false;

        while (state == false)
        {
            if (sceneDic.ContainsKey(nextScene))
                sceneDic[nextScene].Progress(operation.progress);

            // ���� ��� �ε�� ���¶�� ó��
            if (operation.isDone)
            {
                state = true;
                // ���� ���� Exit�Լ��� ȣ��
                if (sceneDic.ContainsKey(current))
                    sceneDic[current].Exit();

                // ����Ǵ� ���� Enter�Լ��� ȣ��
                if (sceneDic.ContainsKey(nextScene))
                    sceneDic[nextScene].Enter();
                // ���� ���� �����մϴ�.
                current = nextScene;

                loading = false;
            }

            yield return null;
        }
    }

    // �ȵ���̵��� ��Ŀ���� ������ ��� ȣ��Ǵ� �Լ�
    private void OnApplicationFocus(bool focus)
    {

    }
    // �ȵ���̵��� ȨŰ�� �������� ȣ��Ǵ� �Լ�
    private void OnApplicationPause(bool pause)
    {

    }
    // ���α׷��� ����ɶ� Release�Լ��� ȣ��
    private void OnApplicationQuit()
    {
        Release();
    }
}
