using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIHp : BaseUI
{
    public Transform HpPivot;
    SpriteColor spriteColor;
    public ShareInfo info;
    private bool isDie = false;

    RectTransform back;
    float backSize = 180f;
    float beforBackSize = 180f;

    private float elapseed = 0;

    private bool SizeControlState = false;




    Slider slider;

    #region BASEUI로부터 상속받은 함수목록
    public override void Init()
    {

        spriteColor = GetComponentInChildren<SpriteColor>();
        if (spriteColor != null)
        {
            spriteColor.Init();
            spriteColor.SetColor(Color.red);
        }
            

        slider = GetComponent<Slider>();

        Transform t = transform.Find("Fill Area/Back");
        if (t != null) back = t.GetComponent<RectTransform>();
    }

    public void SetColor(Color color)
    {
        spriteColor.SetColor(color);
    }
    
    public override void Run()
    {
        if (isDie)
        {

            return;
        }


        if (info != null && HpPivot != null)
        {
            
            setValue(info.lastState[2], info.currHp);
            targetChase(HpPivot.position);
        }
        else
        {
            
            Close();
        }
            
    }

    public IEnumerator  SizeControl(float size)
    {
        SizeControlState = true;
        Vector2 start = back.sizeDelta;
        Vector2 end = new Vector2(size, back.sizeDelta.y);
        elapseed = 0;

        while(elapseed < 1)
        {
            elapseed += Time.deltaTime * 2;
            back.sizeDelta = Vector2.Lerp(start, end, elapseed);

           yield return null;
        }

        back.sizeDelta = end;
        beforBackSize = size;
        SizeControlState = false;
    }
    public override void Open()
    {
    }
    public override void Close()
    {
        
        isDie = true;
        Destroy(this.gameObject);
    }
    #endregion BASEUI로부터 상속받은 함수목록

    public void setValue(int hp , int currHp)
    {
        float hpValue = (float)currHp / hp;

        hpValue = Mathf.Floor(hpValue * 10000) / 10000f;


        slider.value = hpValue;

        backSize = 180f * hpValue;
        if(backSize < 0)
            backSize = 0;

        if (backSize != beforBackSize && SizeControlState == false)
        {
            StartCoroutine(SizeControl(backSize));
        }

        if (hpValue <= 0 && isDie == false)
        {
            
            spriteColor.Execute(Color.red, new Color(0, 0, 0, 0), ColorMode.One, 0.5f, true);
            Invoke("Close", 0.5f);
        }
            
    }

    

    public void targetChase(Vector3 tarPos)
    {
        if (Camera.main != null)
        {
            Vector3 viewPos = Camera.main.WorldToScreenPoint(tarPos);
            transform.position = viewPos;
        }

    }


}

