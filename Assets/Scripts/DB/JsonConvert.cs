using System;
using System.IO;
using UnityEngine;

public static class JsonConvert
{
    [Serializable]
    private class Wrapper<T>
    {
        public T[] Items;
    }

    public static T[] ArrayFromJson<T>(string json)
    {
        Wrapper<T> wrapper = JsonUtility.FromJson<Wrapper<T>>(json);
        return wrapper.Items;
    }

    public static T FromJson<T>(string json)
    {
        return JsonUtility.FromJson<T>(json);
    }

    public static string ToJson<T>(T item, bool prettyPrint = true)
    {
        return JsonUtility.ToJson(item, prettyPrint);
    }

    public static string ToJson<T>(T[] array)
    {
        Wrapper<T> wrapper = new Wrapper<T>();
        wrapper.Items = array;
        return JsonUtility.ToJson(wrapper);
    }

    public static string ToJson<T>(T[] array, bool prettyPrint = true)
    {
        Wrapper<T> wrapper = new Wrapper<T>();
        wrapper.Items = array;

        // 두번째 매개변수로 true값을 넣어주면 사용자가 보기 쉬운 형태의 json 파일로 만든다.
        // ( 행이 개행처리됩니다. )
        return JsonUtility.ToJson(wrapper, prettyPrint);
    }
}
