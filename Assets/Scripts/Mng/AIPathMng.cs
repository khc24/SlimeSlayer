using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class AIPathMng : Mng<AIPathMng>
{
    private Dictionary<AIPathType, BaseAIPath> aiDic =
       new Dictionary<AIPathType, BaseAIPath>();

    private AIGrid2DRenderer AIgrid2DRenderer;
    public AIPathFinding2D AIpathFinding2D;

    

    // AINode2D �������� �о���� ���
    private readonly string path = "Prefab/AI/";


    #region // Mng �߻� �޼ҵ� ���Ǻ�


    public override void Run()
    {
        
        if ((GameDB.MngEnabled & (int)MngType.AIPathMng) != (int)MngType.AIPathMng)
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
        mngType = MngType.AIPathMng;

        Add(AIPathType.AIGrid2DRenderer, true);
        Add(AIPathType.AIPathFinding2D, true);

        AIgrid2DRenderer = Get<AIGrid2DRenderer>(AIPathType.AIGrid2DRenderer);
        AIpathFinding2D = Get<AIPathFinding2D>(AIPathType.AIPathFinding2D);

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


    public void modeChange()
    {
        
    }

    // �񵿱�� �ε��ϴ� �ڵ�� ���� �����ؼ� ������ ����
    public BaseAIPath Add(AIPathType ai, bool activeState = true)
    {
        // ui�� ��ϵǾ� �ִٸ� ��ϵǾ� �ִ� ui�� �Ѱ��ش�.
        if (aiDic.ContainsKey(ai))
        {
            aiDic[ai].SetActive(activeState);
            return aiDic[ai];
        }


        BaseAIPath baseAIPath = this.Instantiate<BaseAIPath>(path + ai,
                                                  Vector3.zero,
                                                  Quaternion.identity,
                                                  true,
                                                  transform);




        if (baseAIPath != null)
        {
            baseAIPath.SetActive(activeState);
            aiDic.Add(ai, baseAIPath);
        }
        return baseAIPath;
    }
    public void Del(AIPathType ai)
    {
        if (aiDic.ContainsKey(ai))
        {
            aiDic[ai].Destroy();
            aiDic.Remove(ai);
        }
    }
    
    public void CallEvent(AIPathType ai,
                           string function,
                           System.Object obj = null)
    {
        if (aiDic.ContainsKey(ai))
            aiDic[ai].SendMessage(function,
                obj,
                SendMessageOptions.DontRequireReceiver);

    }

   

    // aiDic�� ���ϴ� ai�� �ִٸ� �޾ƿ´�
    public T Get<T>(AIPathType ai) where T : BaseAIPath
    {
        if (aiDic.ContainsKey(ai))
            return aiDic[ai].GetComponent<T>();

        return null;
    }

    // Ư�� ai�� ã�Ƽ� ���¸� �����մϴ�.
    public void SetActive(AIPathType ai, bool state)
    {
        if (aiDic.ContainsKey(ai))
        {
            aiDic[ai].SetActive(state);
        }

    }

    public IEnumerator IEShowDelay(float targetTime, AIPathType ai)
    {
        yield return new WaitForSeconds(targetTime);
        SetActive(ai, true);
    }

    public void ShowDelay(float targetTime, AIPathType ai)
    {
        StartCoroutine(IEShowDelay(targetTime, ai));
    }

    public void Find(AIPath2D sk, AIPath2D t , List<AINode2D> sb = null)
    {
        
        if (AIpathFinding2D != null)
        {
            
            AIpathFinding2D.Find(sk, t, sb);
        } 
        
    }


    public List<AINode2D> tempWallCreate(Unit my , Unit targetUnit)
    {

        return AIgrid2DRenderer.tempWallCreate(my , targetUnit);
        
    }


  
}
