using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPivot : MonoBehaviour
{
    public eGizmoType gizmoType;
    public float radius = 0.1f;
    public Color color = Color.red;
    public Vector3 size = Vector3.one;
   




    // �� ȭ��󿡼� �׻� ����
    private void OnDrawGizmos()
    {

        Gizmos.color = color;

        Matrix4x4 rotationMatrix = transform.localToWorldMatrix;
        Gizmos.matrix = rotationMatrix;


        switch (gizmoType)
        {
            case eGizmoType.WireCube:
                Gizmos.DrawWireCube(Vector3.zero, size);
                break;

            case eGizmoType.Sphere:
                Gizmos.DrawSphere(Vector3.zero, radius);
                break;
        }


    }

    
    
}
