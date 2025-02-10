using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 속도에 영향을 주는 요소
// 1. 파일이 위치한 폴더의 경로가 길경우 ( 로드되는 속도가 느립니다. )
// 2. 파일의 사이즈가 크거나 파일의 개수가 많을경우.
// 3. 파일이 생성되면서 다른 파일을 로드할 경우 ( 파티클 )

// 아래의 클래스는 로드하는 역할만 수행하는 클래스입니다.
public class LoaderAsync : MonoBehaviour
{
    
    public void LoadAsync<T>(
                             int spawnID,
                             int tableID,
                             int LV,
                             string path, 
                              Vector3 position,
                              Quaternion rotation,
                              System.Action<int,int, int, T, Vector3, Quaternion> action) where T : UnityEngine.Object
    {
        StartCoroutine(IELoadAsync<T>(spawnID, tableID, LV, path, position, rotation ,action));
    }

    
    private IEnumerator IELoadAsync<T>(
                                    int spawnID,
                                    int tableID,
                                    int LV,
                                    string path,
                                    Vector3 position,
                                    Quaternion rotation,
                                    System.Action<int, int, int, T, Vector3, Quaternion> action = null ) 
                                    where T : UnityEngine.Object

    {
        // 차후 일정한 시간까지 로드하지 못했다면 종료하는 코드를 작성하자
        
        ResourceRequest request = Resources.LoadAsync(path);
        
        while (request.isDone == false)
            yield return null;

        
        GameObject obj = request.asset as GameObject;

       

        if (action != null)
            action(spawnID, tableID , LV, obj.GetComponent<T>(), position, rotation );
    }



}

