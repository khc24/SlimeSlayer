using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public enum ColorMode
{
    One,
    Inverse,
    NumberOfTimes,
    Pingpong,
    PingpongCount
}
public class SpriteColor : MonoBehaviour
{
    private Material rgbMaterial;
    private ColorMode colorMode = ColorMode.One;

    private List<SpriteRenderer> renderers 
        = new List<SpriteRenderer>();

    private List<Image> images
        = new List<Image>();

    // 업데이트 여부
    private bool execute = false;
    private float speed = 1.0f;
    private float elapsed = 0;
    private Color start;
    private Color end;

    private int count = 1;

    // 알파값을 유지할 것인지
    private bool keepAlpha = false;

    public void Init()
    {
        renderers.AddRange(
            GetComponentsInChildren<SpriteRenderer>(true));
        images.AddRange(GetComponentsInChildren<Image>(true));
        

        // 0x80
        Material mat 
            = Resources.Load<Material>("Materials/RGBShader");

        
        // 0x400 = 0x80 동일한 메모리를 만든다음 카피한다.        
        rgbMaterial = Instantiate(mat);

        for( int i = 0; i< renderers.Count; ++ i )
        {
            if( renderers[i].tag != "Weapon" )
                renderers[i].material = rgbMaterial;
        }

        foreach(var value in images)
        {
            value.material = rgbMaterial;
        }
    }

    
    public void SetColor(Color color)
    {
        rgbMaterial.SetFloat("_R", color.r);
        rgbMaterial.SetFloat("_G", color.g);
        rgbMaterial.SetFloat("_B", color.b);
        rgbMaterial.SetFloat("_A", color.a);
    }
    public void Execute( Color start, 
                  Color end, 
                  ColorMode colorMode,
                  float speed,
                  bool keepAlpha = false,
                  int count = 1)
    {
         if (execute)
            return;
        this.start = start;
        this.end = end;
        this.colorMode = colorMode;
        this.speed = speed;
        this.keepAlpha = keepAlpha;
        this.count = count;
        if( colorMode == ColorMode.PingpongCount )
            this.count *= 2;
        execute = true;
        elapsed = 0;
        if(colorMode == ColorMode.Inverse)
        {
            Color temp =  this.start;
            this.start = this.end;
            this.end = temp;
        }

    }

    
    void Update()
    {
        if (execute == false)
            return;

        elapsed += Time.deltaTime / speed;
        
        Color color = Color.Lerp(start, end, elapsed);
        rgbMaterial.SetFloat("_R", color.r);
        rgbMaterial.SetFloat("_G", color.g);
        rgbMaterial.SetFloat("_B", color.b);
        rgbMaterial.SetFloat("_A", color.a);

        if( elapsed >= 1 )
        {
            elapsed = 0;
            // 시점에서 종점으로 종점에서 시점으로가는 방식
            if (keepAlpha == false)
            {
                rgbMaterial.SetFloat("_R", 0);
                rgbMaterial.SetFloat("_G", 0);
                rgbMaterial.SetFloat("_B", 0);
                rgbMaterial.SetFloat("_A", 1);
            }
            switch( colorMode )
            {
                // 한번씩만 실행되는 모드
                case ColorMode.Inverse:
                case ColorMode.One:
                    execute = false;
                    break;
                    // 반복 실행되는 모드
                case ColorMode.Pingpong:
                    {
                        Color temp = start;
                        start = end;
                        end = temp;
                    }
                    break;

                    // 여러번( 사용자가 지정한 수대로 )
                    // 실행되는 모드
                case ColorMode.NumberOfTimes:
                    --count;
                    if (count <= 0)
                    {
                        execute = false;
                    }
                    break;
                case ColorMode.PingpongCount:
                    {
                        Color temp = start;
                        start = end;
                        end = temp;
                        --count;
                        if (count <= 0)
                        {
                            execute = false;
                        }
                    }
                    break;
            }
        }

    }
}

