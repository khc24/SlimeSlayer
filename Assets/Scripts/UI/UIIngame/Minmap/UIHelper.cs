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

    // RPG에서 배경이미지를 움직이기 위한 함수.
    public static void MarkOnTheRPGGame( Transform world,
                                         Transform uiBackground )
    {
        uiBackground.localPosition = WorldPosToMapPos(world.position) * -1;
    }

    // 비율 계산을 해서 3차원 좌표값을 ui상의 좌표로 변경해주는 함수
    // 월드맵 사이즈 : ui사이즈 = 월드 위치 : x
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
    // ui위치에서 3차원 좌표값으로 변경하는 함수입
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
