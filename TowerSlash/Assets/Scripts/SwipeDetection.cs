using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SwipeDirection
{
    Up, // 0 
    Down, // 1
    Left, // 2
    Right, // 3
    Null // 4
}

public class SwipeDetection : MonoBehaviour
{
    public SwipeDirection GetOppositeDirection(SwipeDirection currentDirection)
    {
        switch(currentDirection)
        {
            case SwipeDirection.Up:
            return SwipeDirection.Down;
            
            case SwipeDirection.Down:
            return SwipeDirection.Up;
            
            case SwipeDirection.Left:
            return SwipeDirection.Right;

            case SwipeDirection.Right:
            return SwipeDirection.Left;
        }
        return SwipeDirection.Null;
    }

    private Vector2 _startPos = Vector2.zero;
    private Vector2 _endPos = Vector2.zero;
    private float _startTime = 0.0f;
    private float _endTime = 0.0f;
    private SwipeDirection _swipeDirection = SwipeDirection.Null;
    private PlayerController _player;
    private float _minSwipeDist = 50.0f;
    private float _maxSwipeDur = 0.3f;
    public static SwipeDetection instance;

    private void Start()
    {
        SwipeDetection.instance = this;
        _player = FindFirstObjectByType<PlayerController>();
    }

    private void Update()
    {
    #if UNITY_EDITOR_WIN
        MouseInput();
    #elif UNITY_ANDROID
        TouchInput();
    #endif
    }
    
    private void TouchInput()
    {
        if(Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            switch(touch.phase)
            {
                case TouchPhase.Began:
                SwipeStart(touch.position, Time.time);
                break;

                case TouchPhase.Ended:
                SwipeEnd(touch.position, Time.time);
                break;
            }
            
        }
    }

    private void MouseInput()
    {
        if(Input.GetMouseButtonDown(0))
        {
            SwipeStart(Input.mousePosition, Time.time);
        }

        if(Input.GetMouseButtonUp(0))
        {
            SwipeEnd(Input.mousePosition, Time.time);
        }

    }

    private void SwipeStart(Vector2 pos, float time)
    {
        _startPos = pos;
        _startTime = time;
    }

    private void SwipeEnd(Vector2 pos, float time)
    {
        _endPos = pos;
        _endTime = time;
        DetectSwipe();
    }

    private void DetectSwipe()
    {
        float swipeDistance = Vector2.Distance(_startPos, _endPos);
        float swipeTime = _endTime - _startTime;

        if (swipeDistance >= _minSwipeDist && swipeTime <= _maxSwipeDur)
        {
            Vector2 direction = (_endPos - _startPos).normalized;
            _swipeDirection = Swiped(direction);

            if(_player._canAttack)
            {
                //Attacked
                _player.Attack(_swipeDirection);
            }
        }
        else if (swipeDistance <= _minSwipeDist && swipeTime <= _maxSwipeDur)
        {
            //Tap Leap
            _player.transform.position += new Vector3(0.0f, 1.0f, 0.0f);
            ScoreTracker.instance.AddScore(1);
        }
    }

    private SwipeDirection Swiped(Vector2 direction)
    {
        float angle = Vector2.SignedAngle(Vector2.up, direction);

        if (angle < 30 && angle > -30)
            return SwipeDirection.Up; 
        
        else if (angle > 60 && angle < 120)
             return SwipeDirection.Left; 
        
        else if (angle < -150 || angle > 150)
            return SwipeDirection.Down; 
        
        else if(angle < -60 && angle > -120)
            return SwipeDirection.Right; 
        
        return SwipeDirection.Null;

    }

}
