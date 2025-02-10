using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIPlayerHP : BaseUI
{
    SpriteColor spriteColor;
  
    Slider slider;

    #region BASEUI로부터 상속받은 함수목록
    public override void Init()
    {
        
        spriteColor = GetComponent<SpriteColor>();
        if (spriteColor != null)
            spriteColor.Init();

        slider = GetComponent<Slider>();
    }
    public override void Run()
    {
    }
    public override void Open()
    {
    }
    public override void Close()
    {
    }
    #endregion BASEUI로부터 상속받은 함수목록
 

  
    

    public void setValue(float hp)
    {
        if(hp >= 0.6f)
        {
            
            slider.value = hp;
            spriteColor.SetColor(Color.green);
        }
        else if(hp < 0.6f && hp >= 0.3f)
        {
            slider.value = hp;
            spriteColor.SetColor(Color.yellow);
        }
        else
        {
            slider.value = hp;
            spriteColor.SetColor(Color.red);
        }
        
    }


   
}
