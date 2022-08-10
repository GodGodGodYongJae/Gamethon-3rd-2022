using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SwipeDetection : MonoBehaviour
{
    //private Vector2 startPos;
    //public int pixelDistToDetect = 200;
    //private bool fingerDown;
    //private bool endTouch;
    public UnityEvent<bitFlags.PlayerMoveDirection> TouchEvent;

    private Vector2 touchBeganPos;
    private Vector2 touchEndedPos;
    private Vector2 touchDif;
    public float swipeSensitivity;
    public void OnTouchEvent(bitFlags.PlayerMoveDirection pd)
    {
        TouchEvent?.Invoke(pd);
    }


    private void Swipe1()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);


            if (touch.phase == TouchPhase.Began)
            {
                touchBeganPos = touch.position;
            }
            if (touch.phase == TouchPhase.Ended)
            {
                touchEndedPos = touch.position;
                touchDif = (touchEndedPos - touchBeganPos);

                //��������. ��ġ�� x�̵��Ÿ��� y�̵��Ÿ��� �ΰ������� ũ��
                if (Mathf.Abs(touchDif.y) > swipeSensitivity || Mathf.Abs(touchDif.x) > swipeSensitivity)
                {
                    if (touchDif.y > 0 && Mathf.Abs(touchDif.y) > Mathf.Abs(touchDif.x))
                    {
                        OnTouchEvent(bitFlags.PlayerMoveDirection.Front);
                    }
                    else if (touchDif.y < 0 && Mathf.Abs(touchDif.y) > Mathf.Abs(touchDif.x))
                    {
                        OnTouchEvent(bitFlags.PlayerMoveDirection.Back);
                    }
                    else if (touchDif.x > 0 && Mathf.Abs(touchDif.y) < Mathf.Abs(touchDif.x))
                    {
                        OnTouchEvent(bitFlags.PlayerMoveDirection.Right);
                    }
                    else if (touchDif.x < 0 && Mathf.Abs(touchDif.y) < Mathf.Abs(touchDif.x))
                    {
                        OnTouchEvent(bitFlags.PlayerMoveDirection.Left);
                    }
                }
                //��ġ.
                else
                {
                    OnTouchEvent(bitFlags.PlayerMoveDirection.Dash);
                }
            }
        }
    }
    private void Update()
    {
        Swipe1();

    }

    //if(fingerDown.Equals(false) && Input.touchCount > 0 && Input.touches[0].phase == TouchPhase.Moved)
    //   {
    //       startPos = Input.touches[0].position;
    //       fingerDown = true;
    //   }
    //else if (Input.touchCount > 0 && Input.touches[0].phase == TouchPhase.Began)
    //{
    //    //Debug.Log("Swipe T");
    //    OnTouchEvent(bitFlags.PlayerMoveDirection.Dash);
    //}

    //if (fingerDown && Input.touches.Length > 0)
    //{
    //    if (Input.touches[0].position.y > startPos.y + pixelDistToDetect)
    //    {
    //        Debug.Log(Input.touches[0].position.y + "," + startPos.y + pixelDistToDetect);
    //        fingerDown = false;
    //        OnTouchEvent(bitFlags.PlayerMoveDirection.Front);
    //    }
    //    else if (Input.touches[0].position.y <= startPos.y - pixelDistToDetect)
    //    {
    //        Debug.Log(Input.touches[0].position.y + "," + startPos.y + pixelDistToDetect);
    //        fingerDown = false;
    //        OnTouchEvent(bitFlags.PlayerMoveDirection.Back);
    //    }
    //    else if (Input.touches[0].position.x <= startPos.x - pixelDistToDetect)
    //    {
    //        fingerDown = false;
    //        OnTouchEvent(bitFlags.PlayerMoveDirection.Left);
    //    }
    //    else if (Input.touches[0].position.x >= startPos.x + pixelDistToDetect)
    //    {
    //        fingerDown = false;
    //        OnTouchEvent(bitFlags.PlayerMoveDirection.Right);
    //    }
    //else
    //{
    //    fingerDown = false;
    //    OnTouchEvent(bitFlags.PlayerMoveDirection.Dash);
    //}


    //if(fingerDown && Input.touchCount > 0 && Input.touches[0].phase == TouchPhase.Ended)
    //{
    //    fingerDown = false;
    //    endTouch = true;
    //}

    //Testing For Pc
    //if(fingerDown == false && Input.GetMouseButtonDown(0))
    //{
    //    startPos = Input.mousePosition;
    //    fingerDown = true;
    //}

    //if (fingerDown)
    //{
    //    if (Input.mousePosition.y >= startPos.y + pixelDistToDetect)
    //    {
    //        fingerDown = false;
    //    }

    //}

    //if (fingerDown && Input.GetMouseButtonUp(0))
    //{
    //    fingerDown = false;
    //}

}