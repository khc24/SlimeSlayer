using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{

    public ItemInfo info;
    public SpriteRenderer weaponSprite;
    public SpriteColor spriteColor;
    public float R;
    public float G;
    public float B;
    public Color color = Color.black;

    public void Init()
    {
        weaponSprite = GetComponent<SpriteRenderer>();
        Transform t = transform.parent;
        t = t.parent;
        
        spriteColor = t.GetComponentInChildren<SpriteColor>(true);
        if (spriteColor != null)
            spriteColor.Init();
        


    }

    



    public void SetInfo(int uniqueID, Job job)              
    {
        info = GameDB.GetItem(uniqueID);

        if(info != null)
        {
            weaponSprite.sprite = DataManager.items[info.iconCount];
            color = info.color;
            if (spriteColor != null)
            {
                
                spriteColor.SetColor(info.color);
            }
                
        }
        else
        {
            float R = 0;
            float G = 0;
            float B = 0;
            if (job == Job.WARRIOR)
            {
                weaponSprite.sprite = GameDB.itemDicAll[1].sprite;
                color = GameDB.itemDicAll[1].color;

            }
            else if(job == Job.WIZARD)
            {
                weaponSprite.sprite = GameDB.itemDicAll[9].sprite;
                color = GameDB.itemDicAll[9].color;
            }




            

            if (spriteColor != null)
                spriteColor.SetColor(color);
        }

        
     
    }


    
}
