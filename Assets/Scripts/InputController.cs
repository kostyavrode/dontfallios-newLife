using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputController : MonoBehaviour
{
    public static event OnSwipeInput SwipeEvent;
    public delegate void OnSwipeInput(Vector2 direction);
    private Vector2 tapPosition;
    private Vector2 deltaSwipe;
    private bool isSwiping;
    private bool isMobile;
    private bool isGameStarted;
    private float deadZone = 60;
    public bool IsGameStarted
    {
        get { return isGameStarted; }
        set { isGameStarted = value; }
    }
    private void Start()
    {
        isMobile = Application.isMobilePlatform;
    }
    private void Update()
    {
        if (isGameStarted)
        {
            if (!isMobile)
            {
                if (Input.GetMouseButtonDown(0))
                {
                    isSwiping = true;
                    tapPosition = Input.mousePosition;
                }
                else if (Input.GetMouseButtonDown(0))
                {
                    ResetSwipe();
                }
            }
            else
            {
                if (Input.touchCount > 0)
                {
                    if (Input.GetTouch(0).phase == TouchPhase.Began)
                    {
                        isSwiping = true;
                        tapPosition = Input.GetTouch(0).position;
                    }
                    else if (Input.GetTouch(0).phase == TouchPhase.Canceled || Input.GetTouch(0).phase == TouchPhase.Ended)
                    {
                        ResetSwipe();
                    }
                }
            }
            CheckSwipe();
        }
    }
    private void CheckSwipe()
    {
        if (isSwiping)
        {
            if (!isMobile && Input.GetMouseButtonUp(0))
            {
                deltaSwipe = (Vector2)Input.mousePosition - tapPosition;
            }
            else if (Input.touchCount > 0)
            {
                deltaSwipe = Input.GetTouch(0).position - tapPosition;
            }
            if (deltaSwipe.magnitude>deadZone)
            {                   
                if (SwipeEvent!=null)
                {
                    if (Mathf.Abs(deltaSwipe.x)>Mathf.Abs(deltaSwipe.y))
                    {
                        if (deltaSwipe.x >0)
                        {
                            SwipeEvent?.Invoke(Vector2.right);
                        }
                        else
                        {
                            SwipeEvent?.Invoke(Vector2.left);
                        }
                    }
                    else
                    {
                        if (deltaSwipe.y>0)
                        {
                            SwipeEvent?.Invoke(Vector2.up);
                        }
                        else
                        {
                            SwipeEvent?.Invoke(Vector2.down);
                        }
                    }
                }
                ResetSwipe();
            }
        }
        if (Application.platform == RuntimePlatform.WindowsEditor)
        {
            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                SwipeEvent?.Invoke(Vector2.up);
            }
            if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                SwipeEvent?.Invoke(Vector2.down);
            }
            if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                SwipeEvent?.Invoke(Vector2.left);
            }
            if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                SwipeEvent?.Invoke(Vector2.right);
            }
        }
    }
    private void ResetSwipe()
    {
        isSwiping = false;
        tapPosition = Vector2.zero;
        deltaSwipe = Vector2.zero;
    }
}
