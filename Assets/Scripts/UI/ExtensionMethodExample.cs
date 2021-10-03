using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public static class ExtensionMethodExample
{
    public static void BindEvent(this GameObject go, Action<PointerEventData> action, Define.UIEvent evtType = Define.UIEvent.Click)
    {
        UIBase.AddUIEvent(go, action, evtType);
    }
    public static T GetOrAddComponent<T>(this GameObject go) where T : UnityEngine.Component
    {
        return Utils.GetOrAddComponent<T>(go);
    }
    public static bool IsValid(this GameObject go)
    {
        return go != null && go.activeSelf;
    }
}
