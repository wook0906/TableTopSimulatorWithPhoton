using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class InputManager
{
    public Action keyAction = null;
    public Action<Define.MouseEvent> mouseEventAction = null;
    //public Action<Vector2> mouseAimAction = null;

    bool _leftMousePressed = false;
    public bool _rightMousePressed = false;
    float _leftKeepPressedTime = 0f;
    float _rightKeepPressedTime = 0f;

    public void OnUpdate()
    {
        if (EventSystem.current.IsPointerOverGameObject()) return;

        if (Input.anyKey && keyAction != null)
            keyAction.Invoke();
        //if (mouseAimAction != null && _rightMousePressed)
        //{
        //    mouseAimAction.Invoke(new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y")));
        //}
        if (mouseEventAction != null)
        {
            if (Input.GetMouseButton(0))
            {
                if (!_leftMousePressed)
                {
                    mouseEventAction.Invoke(Define.MouseEvent.LeftPointerDown);
                    _leftKeepPressedTime = Time.time;
                }
                mouseEventAction.Invoke(Define.MouseEvent.LeftPress);
                _leftMousePressed = true;
            }
            else
            {

                if (_leftMousePressed)
                {
                    if (Time.time < _leftKeepPressedTime + 0.2f)
                    {
                        mouseEventAction.Invoke(Define.MouseEvent.LeftClick);
                    }
                    mouseEventAction.Invoke(Define.MouseEvent.LeftPointerUp);
                }
                _leftMousePressed = false;
                _leftKeepPressedTime = 0f;
            }
            if (Input.GetMouseButton(1))
            {
                if (!_rightMousePressed)
                {
                    mouseEventAction.Invoke(Define.MouseEvent.RightPointerDown);
                    _rightKeepPressedTime = Time.time;
                }
                mouseEventAction.Invoke(Define.MouseEvent.RightPress);
                _rightMousePressed = true;
            }
            else
            {

                if (_rightMousePressed)
                {
                    if (Time.time < _rightKeepPressedTime + 0.2f)
                    {
                        mouseEventAction.Invoke(Define.MouseEvent.RightClick);
                    }
                    mouseEventAction.Invoke(Define.MouseEvent.RightPointerUp);
                }
                _rightMousePressed = false;
                _rightKeepPressedTime = 0f;
            }
        }
    }

    public void Clear()
    {
        keyAction = null;
        mouseEventAction = null;
    }
    
}
