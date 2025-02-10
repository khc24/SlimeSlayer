using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// �ӵ��� ������ �ִ� ���
// 1. ������ ��ġ�� ������ ��ΰ� ���� ( �ε�Ǵ� �ӵ��� �����ϴ�. )
// 2. ������ ����� ũ�ų� ������ ������ �������.
// 3. ������ �����Ǹ鼭 �ٸ� ������ �ε��� ��� ( ��ƼŬ )

// �Ʒ��� Ŭ������ �ε��ϴ� ���Ҹ� �����ϴ� Ŭ�����Դϴ�.
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
        // ���� ������ �ð����� �ε����� ���ߴٸ� �����ϴ� �ڵ带 �ۼ�����
        
        ResourceRequest request = Resources.LoadAsync(path);
        
        while (request.isDone == false)
            yield return null;

        
        GameObject obj = request.asset as GameObject;

       

        if (action != null)
            action(spawnID, tableID , LV, obj.GetComponent<T>(), position, rotation );
    }



}

