using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public abstract class UIBase : MonoBehaviour
{
    Dictionary<Type, UnityEngine.Object[]> _objects = new Dictionary<Type, UnityEngine.Object[]>();

    public abstract void Init();

    private void Start()
    {
        Init();
    }

    protected void Bind<T>(Type type) where T : UnityEngine.Object
    {
        string[] names = Enum.GetNames(type);
        UnityEngine.Object[] objects = new UnityEngine.Object[name.Length];
        _objects.Add(typeof(T), objects);

        for (int i = 0; i < names.Length; i++)
        {
            if (typeof(T) == typeof(GameObject))
                objects[i] = Utils.FindChild(gameObject, names[i], true);
            else
                objects[i] = Utils.FindChild<T>(gameObject, names[i], true);
            if (objects[i] == null)
                Debug.Log($"Faild Bind {names[i]}");
        }
    }

    protected T Get<T>(int idx) where T : UnityEngine.Object
    {
        UnityEngine.Object[] objects = null;
        if (_objects.TryGetValue(typeof(T), out objects) == false)
            return null;

        return objects[idx] as T;
    }

    // Start is called before the first frame update


    protected GameObject GetObject(int idx)
    {
        return Get<GameObject>(idx);
    }
    protected Text GetText(int idx)
    {
        return Get<Text>(idx);
    }
    protected Button GetButton(int idx)
    {
        return Get<Button>(idx);
    }
    protected Image GetImage(int idx)
    {
        return Get<Image>(idx);
    }
    public static void AddUIEvent(GameObject go, Action<PointerEventData> action, Define.UIEvent evtType = Define.UIEvent.Click)
    {
        UIEventHandler evt = Utils.GetOrAddComponent<UIEventHandler>(go);

        switch (evtType)
        {
            case Define.UIEvent.Click:
                evt.onClickHandler -= action;
                evt.onClickHandler += action;
                break;
            case Define.UIEvent.Drag:
                evt.onDragHandler -= action;
                evt.onDragHandler += action;
                break;
            default:
                break;
        }

        evt.onDragHandler += ((PointerEventData data) => { evt.gameObject.transform.position = data.position; });
    }
}
