using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class MinimapHelper 
{
    private static float worldWidth;
    private static float worldHeight;
    private static float uiMapWidth;
    private static float uiMapHeight;

    public static void Setting(float width, float Height, float minimapWidth, float minimapHeight)
    {
        worldWidth = width;
        worldHeight = Height;
        uiMapWidth = minimapWidth;
        uiMapHeight = minimapHeight;

        
    }
    
    public static Vector2 WorldPosToMapPos( Vector3 worldPos )
    {

        return WorldPosToMapPos(worldPos, 
                                worldWidth,
                                worldHeight, 
                                uiMapWidth, 
                                uiMapHeight);
    }
    
    public static Vector3 MapPosToWorldPos(Vector3 uiPos)
    {
        return MapPosToWorldPos( uiPos, 
                                worldWidth,
                                worldHeight,
                                uiMapWidth,
                                uiMapHeight);
    }

    // RPG���� ����̹����� �����̱� ���� �Լ�.
    public static void MarkOnTheRPGGame( Transform world,
                                         Transform uiBackground )
    {
        uiBackground.localPosition = WorldPosToMapPos(world.position) * -1;
    }

    // ���� ����� �ؼ� 3���� ��ǥ���� ui���� ��ǥ�� �������ִ� �Լ�
    // ����� ������ : ui������ = ���� ��ġ : x
    private static Vector2 WorldPosToMapPos( Vector3 worldPos,
                                            float worldWidth,
                                            float worldHeight,
                                            float uiMapWidth,
                                            float uiMapHeight)
    {
        Vector3 result = Vector3.zero;
        result.x = (worldPos.x * uiMapWidth) / worldWidth;
        result.y = (worldPos.y * uiMapHeight) / worldHeight;
        return result;
    }
    // ui��ġ���� 3���� ��ǥ������ �����ϴ� �Լ���
    private static Vector3 MapPosToWorldPos( Vector3 uiPos,
                                            float worldWidth,
                                            float worldHeight,
                                            float uiMapWidth,
                                            float uiMapHeight)
    {
        Vector3 result = Vector3.zero;
        result.x = (uiPos.x * worldWidth) / uiMapWidth;
        result.z = (uiPos.y * worldHeight) / uiMapHeight;
        return result;
    }

    public static void LookAt( Transform worldPlayer, Transform uiPlayer )
    {
        float angleZ = Mathf.Atan2(
                        worldPlayer.forward.z, 
                        worldPlayer.forward.x) * Mathf.Rad2Deg;
        uiPlayer.eulerAngles = new Vector3(0, 0, angleZ- 90);
    }


}
