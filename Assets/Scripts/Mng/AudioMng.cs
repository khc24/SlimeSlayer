using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioMng : Mng<AudioMng>
{

    // ��������� �÷����ϱ� ���� ����� �ҽ�
    private AudioSource background;

    // ui�� Ŭ�������� �÷����ϱ� ���� ����� �ҽ�
    private AudioSource uiSource;

    // ������ �÷����ϴ� ����Ʈ ( ��Ȱ���ϱ� ���� ����Ʈ )
    private List<AudioSource> audioSources = new List<AudioSource>();

    // ���� ���� ����Ʈ
    private Dictionary<string, AudioClip> audioClipDic
        = new Dictionary<string, AudioClip>();

    public void SetBackVolume(float back)
    {
        if(background != null)
            background.volume = back * 0.3f;
       
    }

    // �⺻���� ������ ó��
    public void SoundProperty(AudioSource audio,
                              bool loop = false,
                              float spatialBlend = 0,
                              int priority = 0,
                              bool playOnAwake = false,
                              float volume = 1.0f,
                              AudioClip clip = null)
    {
        audio.loop = loop;
        audio.spatialBlend = spatialBlend;
        audio.priority = priority;
        audio.playOnAwake = playOnAwake;
        audio.clip = clip;
        audio.volume = volume;
    }

    public AudioClip LoadClip(string path, string name)
    {
        AudioClip clip = null;
        if (!audioClipDic.ContainsKey(name))
        {
            clip = Resources.Load<AudioClip>(path + name);
            if (clip != null)
                audioClipDic.Add(name, clip);
        }
        else
            clip = audioClipDic[name];
        return clip;
    }

    public void PlayUI(string name,
                        float volume = 1.0f)
    {
        AudioClip clip = LoadClip("Sound/", name);
        uiSource.clip = clip;
        if(UIMenu.effectSlider != null)
        {
            uiSource.volume = UIMenu.effectSlider.value * volume;
        }
        else
        {
            uiSource.volume = volume;
        }
        
        uiSource.Play();
    }
    void Deactive(AudioSource audio)
    {
        StartCoroutine(DeactiveAudio(audio));
    }

    // ����� Ŭ���� ���̸�ŭ ����ϰ� �ִٰ�, 
    // ����� �ҽ��� ��Ȱ��ȭ�ϴ� �Լ�
    IEnumerator DeactiveAudio(AudioSource audio)
    {
        if (audio != null && audio.clip != null)
        {
            yield return new WaitForSeconds(audio.clip.length);
            audio.gameObject.SetActive(false);
        }
        yield return null;
    }

    // Ȱ��ȭ�� AudioSource�� �ִٸ� ã�Ƽ� �Ѱ��ְ�,
    // ������ �����ؼ� �Ѱ��ش�.
    private AudioSource Pooling(string audioName = "")
    {
        if (audioName.Equals(string.Empty))
            audioName = "AudioSource";

        AudioSource source = null;
        for (int i = 0; i < audioSources.Count; ++i)
        {
            // ��Ȱ��ȭ�� ����� �ҽ��� ã�� active���¸� ����
            if (audioSources[i].gameObject.activeSelf == false)
            {
                source = audioSources[i];
                source.gameObject.SetActive(true);
            }
        }
        if (source == null)
        {
            // ����� �ҽ��� �����ϰ�, ������ ������ҽ���
            // ���� ����Ǵ��� �ı����� �ʵ��� ����� �Ŵ�����
            // �ϴܺκп� ��ġ
            GameObject newObject = new GameObject(
                                audioName, typeof(AudioSource));


            newObject.transform.SetParent(transform);
            source = newObject.GetComponent<AudioSource>();
            audioSources.Add(source);
        }
        return source;
    }

    public void LoadBackground()
    {
        AudioClip[] clips = Resources.LoadAll<AudioClip>(
            "Sound/Background");
        foreach (AudioClip clip in clips)
        {
            if (audioClipDic.ContainsKey(clip.name) == false)
            {
                audioClipDic.Add(clip.name, clip);
            }
        }
    }

    public void Play2DEffect(string name,
                           float volume = 1.0f)
    {
        AudioClip clip = LoadClip("Sound/", name);
        AudioSource source = Pooling();

        if (UIMenu.effectSlider != null)
        {
            SoundProperty(source,
                      spatialBlend: 0,
                      volume: UIMenu.effectSlider.value * volume,
                      clip: clip);

        }
        else
        {
            SoundProperty(source,
                      spatialBlend: 0,
                      volume: volume,
                      clip: clip);
        }
        
        source.Play();
        Deactive(source);


    }
    public void PlayBackground(string name,
                                float volume = 1.0f,
                                float spatialBlend = 0)
    {
        if (audioClipDic.ContainsKey(name))
        {
            AudioClip clip = audioClipDic[name];
            background.clip = clip;
            background.spatialBlend = spatialBlend;

            if (UIMenu.backSlider != null)
            {
                background.volume = UIMenu.backSlider.value * volume;

            }
            else
            {
                background.volume = volume;
            }
            
            background.Play();
        }

    }

    

    #region // �߻� ���Ǻ�
    public override void Run()
    {

    }

    // �Ƚ��� ������Ʈ���� ������ ���
    public override void FixRun()
    {

    }

    public override void LateRun()
    {

    }

    public override void Init()
    {
        mngType = MngType.AudioMng;

        background = gameObject.AddComponent<AudioSource>();
        uiSource = gameObject.AddComponent<AudioSource>();

        SoundProperty(background, true, 0, 0, false);
        SoundProperty(uiSource, false, 0, 100, false);

        LoadBackground();
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

    }
    public override void SetEnable(bool state)
    {

    }

    #endregion
}
