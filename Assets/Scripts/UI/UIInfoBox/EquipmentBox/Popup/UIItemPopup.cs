using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIItemPopup : BaseUI
{
    public ItemInfo itemInfo;

    public TMP_Text levelText;
    new public TMP_Text name;
    public TMP_Text atk;
    public TMP_Text def;
    public TMP_Text hp;

    public TMP_Text upLevelText;
    public TMP_Text upAtk;
    public TMP_Text upDef;
    public TMP_Text upHp;
    public TMP_Text upPrice;
    public TMP_Text upGamble;


    public TMP_Text wear;
    public TMP_Text explain;
    public TMP_Text price;
    public List<Image> star = new List<Image>();
    public int grade;
    public Image icon;
    public Sprite spirte;

    public Color maxColor = new Color(0.2f, 0.8f, 0.4f);

    
    public Button takeOnEquip;

   
    public Button takeOffEquip;


    public Button changeEquip;

  
    public Button takeOnBuy;

    
    public Button takeOnSell;

 
    public Button takeOnUpgrade;



  
    private System.Action<ItemInfo> offEquipDelegate;

    
    private System.Action<ItemInfo> onEquipDelegate;

  
    private System.Action<ItemInfo> changeEquipDelegate;

    
    private System.Action<ItemInfo> onBuyDelegate;


    private System.Action<ItemInfo> onSellDelegate;

    
    private System.Action<ItemInfo> onUpgradeDelegate;







    public void Open(ItemInfo info, bool onEquip = false, bool chEquip = false, bool offEquip = false, bool onBuy = false, bool onSell = false, bool onUpgrade = false)
    {
        
        itemInfo = info;
        if (info.level >= 100) levelText.color = maxColor;
        else levelText.color = Color.black;
        levelText.text =  "LV."+ info.level;
        name.text = info.name;
        atk.text = info.attack.ToString();
        def.text = info.defence.ToString();
        hp.text = info.hp.ToString();

        int tempInt = info.level;
        tempInt++;
        if (upLevelText != null)
        {
            if (info.level >= 100)
            {
                upLevelText.text = "";
            }
            else
            {
                upLevelText.text = "LV." + tempInt;
            }
        }



        tempInt = info.attack / info.level;
        tempInt = tempInt * (info.level + 1);
        if(upAtk != null) 
        {
            if(info.level >= 100)
            {
                upAtk.text = "";
            }
            else
            {
                upAtk.text = tempInt.ToString();
            }
        }
        tempInt = info.defence / info.level;
        tempInt = tempInt * (info.level + 1);
        if (upDef != null)
        {
            if(info.level >= 100)
            {
                upDef.text = "";
            }
            else
            {
                upDef.text = tempInt.ToString();
            }
            
        }
            
            

        tempInt = info.hp / info.level;
        tempInt = tempInt * (info.level + 1);
        if (upHp != null) 
        {
            if(info.level >= 100)
            {
                upHp.text = "";
            }
            else
            {
                upHp.text = tempInt.ToString();
            }
        }
        tempInt = info.grade * 1000;
        tempInt += (info.grade * 100) * (info.level - 1);
        if (info.level >= 100) tempInt = 0;

        if (upPrice != null) upPrice.text = string.Format("{0:#,0}", tempInt);

        tempInt = 100 - info.level;

        if (upGamble != null) upGamble.text = tempInt.ToString() + "%";

        int tempPrice = info.price;
        if(price != null)
        {
            if (onBuy)
            {

                price.text = string.Format("{0:#,0}", tempPrice);
            }
            else
            {

                tempPrice /= 2;

                price.text = string.Format("{0:#,0}", tempPrice);
            }
        }
       
        if(wear != null)
        {
            wear.text = "";

            if ((info.wearType & (int)JobBit.ALL) == (int)JobBit.ALL)
            {
                wear.text = "ALL";
            }
            else
            {
                if ((info.wearType & (int)JobBit.WARRIOR) == (int)JobBit.WARRIOR)
                {
                    wear.text = "전사";
                }
                if ((info.wearType & (int)JobBit.WIZARD) == (int)JobBit.WIZARD)
                {
                    if (wear.text == "")
                    {
                        wear.text = " | 마법사";
                    }
                    else
                    {
                        wear.text = "마법사";
                    }
                }
            }
        }
        

        if(explain != null) explain.text = info.explain;
        icon.sprite = info.sprite;

        
        icon.gameObject.SetActive(true);
        
        grade = info.grade;

        for (int i = star.Count - 1; i >= 0; --i)
        {
            if ((info.grade - 1) < i)
                star[i].gameObject.SetActive(false);
            else
                star[i].gameObject.SetActive(true);

        }

        if(takeOnEquip!=null) takeOnEquip.gameObject.SetActive(onEquip);
        if (takeOffEquip != null)  takeOffEquip.gameObject.SetActive(offEquip);
        if (this.changeEquip != null)  this.changeEquip.gameObject.SetActive(chEquip);
        if (takeOnBuy != null) takeOnBuy.gameObject.SetActive(onBuy);
        if (takeOnSell != null) takeOnSell.gameObject.SetActive(onSell);
        if (takeOnUpgrade != null) takeOnUpgrade.gameObject.SetActive(onUpgrade);

        if(takeOnUpgrade == null)
            SetActive(true);
    }

   
    public void OnClickEquip()
    {
        if (onEquipDelegate != null)
        {
            if (itemInfo != null)
                onEquipDelegate(itemInfo);
        }
    }

   
    public void OnClickOffEquip()
    {
        if (offEquipDelegate != null)
        {
            if (itemInfo != null)
                offEquipDelegate(itemInfo);
        }
    }

    public void OnClickChangeEquip()
    {
        if (changeEquipDelegate != null)
        {
            if (itemInfo != null)
                changeEquipDelegate(itemInfo);
        }
    }

   
    public void OnClickBuy()
    {
        if (onBuyDelegate != null)
        {
            if (itemInfo != null)
                onBuyDelegate(itemInfo);
        }
    }

    
    public void OnClickSell()
    {
        if (onSellDelegate != null)
        {
            if (itemInfo != null)
                onSellDelegate(itemInfo);
        }
    }

    public void OnClickUpgrade()
    {
        if (onUpgradeDelegate != null)
        {
            if (itemInfo != null)
                onUpgradeDelegate(itemInfo);
        }
    }


    public void SetOnEquipDelegate(System.Action<ItemInfo> function)
    {
        onEquipDelegate = function;
    }


    public void SetOffEquipDelegate(System.Action<ItemInfo> function)
    {
        offEquipDelegate = function;
    }

 
    public void SetChangeEquipDelegate(System.Action<ItemInfo> function)
    {
        changeEquipDelegate = function;
    }

    public void SetOnBuyDelegate(System.Action<ItemInfo> function)
    {
        onBuyDelegate = function;
    }

    
    public void SetOnSellDelegate(System.Action<ItemInfo> function)
    {
        onSellDelegate = function;
    }

    public void SetOnUpgradeDelegate(System.Action<ItemInfo> function)
    {
        onUpgradeDelegate = function;
    }



    #region //추상 함수 정의부
    public override void Init()
    {
        
        Transform t = transform.Find("Frontground");
        levelText = UtilHelper.Find<TMP_Text>(t, "LevelText",false, true);
        name = UtilHelper.Find<TMP_Text>(t, "Name", false, true);
        atk = UtilHelper.Find<TMP_Text>(t, "ATK", false, true);
        def = UtilHelper.Find<TMP_Text>(t, "DEF", false, true);
        hp = UtilHelper.Find<TMP_Text>(t, "HP", false, true);
        wear = UtilHelper.Find<TMP_Text>(t, "WEAR", false, true);
        explain = UtilHelper.Find<TMP_Text>(t, "ExplainBack/Explain", false, true);
        star.AddRange(UtilHelper.FindAll<Image>(t, "Grade", false, true));
        icon = UtilHelper.Find<Image>(t, "Icon", false, false);
        price = UtilHelper.Find<TMP_Text>(t, "Price/PriceText", false, true);

        for (int i = 0; i < star.Count; ++i)
        {
            star[i].gameObject.SetActive(false);
        }

        upLevelText = UtilHelper.Find<TMP_Text>(t, "UpLevelText", false, true);
        upAtk = UtilHelper.Find<TMP_Text>(t, "UpATK", false, true);
        upDef = UtilHelper.Find<TMP_Text>(t, "UpDEF", false, true);
        upHp = UtilHelper.Find<TMP_Text>(t, "UpHP", false, true);
        upGamble = UtilHelper.Find<TMP_Text>(t, "Gamble", false, true);
        upPrice = UtilHelper.Find<TMP_Text>(t, "UpPrice/PriceText", false, true);

        takeOnEquip = UtilHelper.FindButton(transform, "Frontground/ButtonHorizontal/OnEquip", OnClickEquip);
        takeOffEquip = UtilHelper.FindButton(transform, "Frontground/ButtonHorizontal/OffEquip", OnClickOffEquip);
        changeEquip = UtilHelper.FindButton(transform, "Frontground/ButtonHorizontal/OnChange", OnClickChangeEquip);
        takeOnBuy = UtilHelper.FindButton(transform, "Frontground/ButtonHorizontal/OnBuy", OnClickBuy);

        takeOnSell = UtilHelper.FindButton(transform, "Frontground/ButtonHorizontal/OnSell", OnClickSell);
        takeOnUpgrade = UtilHelper.FindButton(transform, "Frontground/OnUpgrade", OnClickUpgrade);


        isInit = true;
      
    }

    public override void Run()
    {

    }

    public override void Open()
    {

    }

    public override void Close()
    {
     if(levelText!= null) levelText.text = "LV.";
     if(name!= null) name.text = "";
     if(atk!= null) atk.text = "0";
     if(def!=null) def.text = "0";
     if(hp!=null) hp.text = "0";

        for (int i = 0; i < star.Count; ++i)
        {
            star[i].gameObject.SetActive(false);
        }
        if (icon != null) icon.gameObject.SetActive(false);


        if (upLevelText != null) upLevelText.text = "LV.";
     if (upAtk != null) upAtk.text = "0";
     if (upDef != null) upDef.text = "0";
     if (upHp != null) upHp.text = "0";
     if (upPrice != null) upPrice.text = "0";
     if (upGamble != null)   upGamble.text = "0";

        if(itemInfo != null)
            itemInfo.checkbox = false;
        itemInfo = null;
}

    #endregion

    public void GambleClose()
    {
        if (levelText != null) levelText.text = "LV.";
        if (name != null) name.text = "";
        if (atk != null) atk.text = "0";
        if (def != null) def.text = "0";
        if (hp != null) hp.text = "0";

        for (int i = 0; i < star.Count; ++i)
        {
            star[i].gameObject.SetActive(false);
        }
        if (icon != null) icon.gameObject.SetActive(false);


        if (upLevelText != null) upLevelText.text = "LV.";
        if (upAtk != null) upAtk.text = "0";
        if (upDef != null) upDef.text = "0";
        if (upHp != null) upHp.text = "0";
        if (upPrice != null) upPrice.text = "0";
        if (upGamble != null) upGamble.text = "0";

        if (itemInfo != null)
            itemInfo.checkbox = false;
        itemInfo = null;
        gameObject.SetActive(false);
    }

}
