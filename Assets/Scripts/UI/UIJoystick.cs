using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class UIJoystick : MonoBehaviour,
                          IPointerDownHandler,
                          IPointerUpHandler,
                          IDragHandler
{
    private Image joystick;
    private Transform joystickBody;
    private Transform button;
    private Transform range;
    private Vector2 joystickBodyStartPos;
    private Vector2 touchPos;
    private float distance;

    private bool readCheck = false;
    public static bool downOn = false;

    private List<Image> renderers
        = new List<Image>();




    private Vector2 direction = Vector2.zero;

    public Vector2 Dir2D
    {
        get { return direction; }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        
        joystick.raycastTarget = false;
        touchPos = Input.mousePosition;
        readCheck = true;
      
        foreach (var value in renderers)
        {
            Color color = value.color;
            color.a += 0.3f;
            value.color = color;
        }

        downOn = true;

    }

    public void OnPointerUp(PointerEventData eventData)
    {

        if (downOn == false)
            return;


        direction = Vector2.zero;
        joystickBody.position = joystickBodyStartPos;
        button.transform.localPosition = Vector3.zero;
        joystick.raycastTarget = true;

        if(readCheck == true)
        {
            GameDB.targetRead = true;
        }
        readCheck = false;
        
        foreach (var value in renderers)
        {
            Color color = value.color;
            color.a -= 0.3f;
            value.color = color;
        }

        downOn = false;

    }

    public void OnDrag(PointerEventData eventData)
    {

        readCheck = false;

        joystickBody.position = touchPos;
   
            Vector2 direction = eventData.position - touchPos;
            this.direction = direction.normalized;
            float currDist = Vector2.Distance(eventData.position, touchPos);
            button.transform.position = eventData.position;
            if (currDist > distance)
            {
                // 길이가 1인 방향 벡터로 만든다.
                direction.Normalize();
                button.transform.position = touchPos + direction * distance;
            }

    }


    public void touchCansle()
    {
        if (downOn == false)
            return;

        
        direction = Vector2.zero;
        joystickBody.position = joystickBodyStartPos;
        button.transform.localPosition = Vector3.zero;
        joystick.raycastTarget = true;

        
        readCheck = false;

        foreach (var value in renderers)
        {
            Color color = value.color;
            color.a -= 0.3f;
            value.color = color;
        }

        downOn = false;

    }






    public void Init()
    {
  
        joystickBody = transform.Find("JoystickBody");
        button = transform.Find("JoystickBody/Button");
        range = transform.Find("JoystickBody/Range");
        if (range != null)
        {
            // 조이스틱 버튼과 Range와의 거리를 구한다
            distance = Vector3.Distance(joystickBody.position, range.position);
        }

        joystickBodyStartPos = joystickBody.position;
        joystick = GetComponent<Image>();

        renderers.AddRange(joystickBody.GetComponentsInChildren<Image>());




    }

    
 
}