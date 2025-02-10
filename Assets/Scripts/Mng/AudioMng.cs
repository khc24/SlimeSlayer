using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioMng : Mng<AudioMng>
{

    // 배경음악을 플레이하기 위한 오디오 소스
    private AudioSource background;

    // ui를 클릭했을때 플레이하기 위한 오디오 소스
    private AudioSource uiSource;

    // 음원을 플레이하는 리스트 ( 재활용하기 위한 리스트 )
    private List<AudioSource> audioSources = new List<AudioSource>();

    // 음원 파일 리스트
    private Dictionary<string, AudioClip> audioClipDic
        = new Dictionary<string, AudioClip>();

    public void SetBackVolume(float back)
    {
        if(background != null)
            background.volume = back * 0.3f;
       
    }

    // 기본적인 설정을 처리
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

    // 오디오 클립의 길이만큼 대기하고 있다가, 
    // 오디오 소스를 비활성화하는 함수
    IEnumerator DeactiveAudio(AudioSource audio)
    {
        if (audio != null && audio.clip != null)
        {
            yield return new WaitForSeconds(audio.clip.length);
            audio.gameObject.SetActive(false);
        }
        yield return null;
    }

    // 활성화된 AudioSource가 있다면 찾아서 넘겨주고,
    // 없으면 생성해서 넘겨준다.
    private AudioSource Pooling(string audioName = "")
    {
        if (audioName.Equals(string.Empty))
            audioName = "AudioSource";

        AudioSource source = null;
        for (int i = 0; i < audioSources.Count; ++i)
        {
            // 비활성화된 오디오 소스를 찾고 active상태를 변경
            if (audioSources[i].gameObject.activeSelf == false)
            {
                source = audioSources[i];
                source.gameObject.SetActive(true);
            }
        }
        if (source == null)
        {
            // 오디오 소스를 생성하고, 생성된 오디오소스가
            // 신이 변경되더라도 파괴되지 않도록 오디오 매니저의
            // 하단부분에 배치
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

    

    #region // 추상 정의부
    public override void Run()
    {

    }

    // 픽스드 업데이트에서 구현할 기능
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
