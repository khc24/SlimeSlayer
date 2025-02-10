using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public abstract class Scene : MonoBehaviour
{
    // �� ������ �ε尡 �Ϸ�Ǵ� ������ ȣ��Ǵ� �Լ�
    public abstract void Enter();

    // �� ������ �ٸ� �� ���Ϸ� ������ �Ϸ�Ǿ��� �� ȣ��Ǵ� �Լ�
    public abstract void Exit();


    // ������ �ε�ǰ� �������� ��Ȳ�� �����ֱ� ���� �Լ�
    // �Ʒ��� �Լ����� �ε� ui�� ���
    public abstract void Progress(float progress);

 
}
