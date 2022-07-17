using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SwipeDetection : MonoBehaviour
{
    private Vector2 startPos;
    public int pixelDistToDetect = 50;
    private bool fingerDown;
    private bool endTouch;
    public UnityEvent<bitFlags.PlayerMoveDirection> TouchEvent;

    public void OnTouchEvent(bitFlags.PlayerMoveDirection pd)
    {
        TouchEvent?.Invoke(pd);
    }

    private void Update()
    {
        if(endTouch&&fingerDown == false && Input.touchCount > 0 && Input.touches[0].phase == TouchPhase.Moved)
        {
            startPos = Input.touches[0].position;
            fingerDown = true;
            endTouch = false;
        }
        if(fingerDown)
        {
            if(Input.touches[0].position.y >= startPos.y + pixelDistToDetect)
            {
                fingerDown = false;
                Debug.Log("Swipe Up");
                OnTouchEvent(bitFlags.PlayerMoveDirection.Front);
            }
            else if (Input.touches[0].position.y <= startPos.y - pixelDistToDetect)
            {
                fingerDown = false;
                Debug.Log("Swipe Down");
                OnTouchEvent(bitFlags.PlayerMoveDirection.Back);
            }
            else if(Input.touches[0].position.x <= startPos.x - pixelDistToDetect)
            {
                fingerDown = false;
                Debug.Log("Swipe Left");
                OnTouchEvent(bitFlags.PlayerMoveDirection.Left);
            }
            else if (Input.touches[0].position.x >= startPos.x + pixelDistToDetect )
            {
                fingerDown = false;
                Debug.Log("Swipe Right");
                OnTouchEvent(bitFlags.PlayerMoveDirection.Right);
            }
       
        }
        if(endTouch&&!fingerDown && Input.touchCount > 0 && Input.touches[0].phase == TouchPhase.Began)
        {
            //Debug.Log("Swipe T");
            OnTouchEvent(bitFlags.PlayerMoveDirection.Dash);
        }
        if(fingerDown && Input.touchCount > 0 && Input.touches[0].phase == TouchPhase.Ended)
        {
            fingerDown = false;
            endTouch = true;
        }

        //Testing For Pc
        if(fingerDown == false && Input.GetMouseButtonDown(0))
        {
            startPos = Input.mousePosition;
            fingerDown = true;
        }

        if (fingerDown)
        {
            if (Input.mousePosition.y >= startPos.y + pixelDistToDetect)
            {
                fingerDown = false;
                Debug.Log("Swipe Up");
            }

        }

        if (fingerDown && Input.GetMouseButtonUp(0))
        {
            fingerDown = false;
        }
    }
}
