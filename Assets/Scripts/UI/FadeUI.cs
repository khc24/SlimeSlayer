using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeUI : BaseUI
{
    private Image fadeImage;
    // ������Ʈ ������ üũ�ϱ� ���� ����
    [SerializeField]
    private bool update = false;

    #region BASEUI�κ��� ��ӹ��� �Լ����
    public override void Init()
    {
        fadeImage = this.Find<Image>(transform, "Image");
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
    #endregion BASEUI�κ��� ��ӹ��� �Լ����

    private IEnumerator IEFade(Color start,
                                Color end,
                                bool afterState = false,
                                float targetTime = 1)
    {
        update = true;
        float elapsedTime = 0;
        while (update)
        {
            elapsedTime += Time.deltaTime / targetTime;
            elapsedTime = Mathf.Clamp01(elapsedTime);
            fadeImage.color = Color.Lerp(start, end, elapsedTime);

            if (elapsedTime >= 1.0f)
            {
                gameObject.SetActive(afterState);
                update = false;
            }
            yield return null;
        }
    }
    private void Fade(Color start,
                       Color end,
                       bool afterState = false,
                       float targetTime = 1.0f)
    {
        SetActive(true);

        // ������̶�� ( ������Ʈ ���̶�� ) �Լ��� ����
        if (update)
            return;

        fadeImage.color = start;

        StartCoroutine(IEFade(start, end, afterState, targetTime));

    }
    public void FadeIn(bool afterState, float targetTime = 1.0f)
    {
        Fade(end: new Color(0, 0, 0, 0),
             start: Color.black,
             afterState: afterState,
             targetTime: targetTime);
    }
    public void FadeOut(bool afterState, float targetTime = 1.0f)
    {
        Fade(end: Color.black,
             start: new Color(0, 0, 0, 0),
             afterState: afterState,
             targetTime: targetTime);
    }



}
