using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager
{
    int order = 0;

    //Stack<UIPopup> popupStack = new Stack<UIPopup>();
    List<UIPopup> popupStack = new List<UIPopup>();

    UIScene sceneUI = null;

    public GameObject Root
    {
        get
        {
            GameObject root  = GameObject.Find("@UI_Root");
            if (root == null)
                root = new GameObject { name = "@UI_Root" };
            return root;
        }
    }

    public void SetCanvas(GameObject go, bool isNeedSort = true)
    {
        Canvas canvas = Utils.GetOrAddComponent<Canvas>(go);
        canvas.renderMode = RenderMode.ScreenSpaceOverlay;
        canvas.overrideSorting = true;

        if (isNeedSort)
        {
            canvas.sortingOrder = order;
            order++;
        }
        else
        {
            canvas.sortingOrder = 0;
        }
    }

    public T MakeWorldSpaceUI<T>(Transform parent = null, string name = null) where T : UIBase
    {
        if (string.IsNullOrEmpty(name))
            name = typeof(T).Name;
        GameObject go = Managers.Resource.Instantiate($"UI/WorldSpace/{name}");

        if (parent != null)
            go.transform.SetParent(parent);

        Canvas canvas = go.GetComponent<Canvas>();
        canvas.renderMode = RenderMode.WorldSpace;
        canvas.worldCamera = Camera.main;

        return Utils.GetOrAddComponent<T>(go);

    }

    public T MakeSubItem<T>(Transform parent = null, string name = null) where T : UIBase
    {
        if (string.IsNullOrEmpty(name))
            name = typeof(T).Name;
        GameObject go = Managers.Resource.Instantiate($"UI/SubItem/{name}");

        if (parent != null)
            go.transform.SetParent(parent);

        return Utils.GetOrAddComponent<T>(go);

    }
    public T ShowSceneUI<T>(string name = null) where T : UIScene
    {
        if (string.IsNullOrEmpty(name))
            name = typeof(T).Name;

        GameObject go = Managers.Resource.Instantiate($"UI/Scene/{name}");
        T _sceneUI = Utils.GetOrAddComponent<T>(go);
        sceneUI = _sceneUI;

        go.transform.SetParent(Root.transform);

        return _sceneUI;
    }
    public bool IsExistPopup<T>() where T : UIPopup
    {
        foreach (var item in popupStack)
        {
            if (item.name == typeof(T).Name)
                return true;
        }
        return false;
    }

    public T ShowPopupUI<T>(string name = null) where T : UIPopup
    {
        if (string.IsNullOrEmpty(name))
            name = typeof(T).Name;


        GameObject go = Managers.Resource.Instantiate($"UI/Popup/{name}");
        T popup = Utils.GetOrAddComponent<T>(go);
        //popupStack.Push(popup);
        popupStack.Add(popup);

        go.transform.SetParent(Root.transform);

        return popup;
    }

    public void ClosePopupUI()
    {
        if (popupStack.Count == 0)
            return;
        //UIPopup popup = popupStack.Pop();
        UIPopup popup = popupStack[popupStack.Count - 1];
        popupStack.Remove(popup);
        Managers.Resource.Destroy(popup.gameObject);
        popup = null;

        order--;
    }
    public void ClosePopupUI<T>() where T : UIPopup
    {
        if (popupStack.Count == 0)
            return;
        
        foreach (var item in popupStack)
        {
            if (item.name == typeof(T).Name)
            {
                UIPopup popup = item;
                popupStack.Remove(popup);
                Managers.Resource.Destroy(popup.gameObject);
                popup = null;
                order--;
                return;
            }
        }
    }
    public void ClosePopupUI(UIPopup popup)
    {
        if (popupStack.Count == 0) return;
        //if (popupStack.Peek() != popup)
        if (popupStack[popupStack.Count - 1] != popup)
        {
            Debug.Log("close Popup failed");
        }
        ClosePopupUI();
    }

    public void CloseAllPopupUI()
    {
        while (popupStack.Count>0)
        {
            ClosePopupUI();
        }
    }
    public void Clear()
    {
        CloseAllPopupUI();
        sceneUI = null;
    }
}
