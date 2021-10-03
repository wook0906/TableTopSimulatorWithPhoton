using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceManager
{
    public T Load<T>(string path) where T : Object
    {
        if(typeof(T) == typeof(GameObject))
        {
            string name = path;
            int idx = name.LastIndexOf('/'); //마지막 / 검색
            if (idx >= 0)
                name = name.Substring(idx + 1);

            GameObject go = Managers.Pool.GetOriginal(name);
            if (go != null)
                return go as T;

            //풀에 이미 존재하면 해당 오브젝트 반환
        }

        return Resources.Load<T>(path);
    }
    
    public GameObject Instantiate(string path, Transform parent = null)
    {
        GameObject original = Load<GameObject>($"Prefabs/{path}");
        if(original == null)
        {
            Debug.Log($"Failed to load prefab : {path}");
            return null;
        }

        //pooling대상인지?
        if (original.GetComponent<Poolable>() != null)
            return Managers.Pool.Pop(original, parent).gameObject;


        GameObject go = Object.Instantiate(original, parent);
        go.name = original.name;

        return go;
    }

    public void Destroy(GameObject go)
    {
        if(go == null)
        {
            return;
        }

        //Pooling 대상이라면 삭제하지 않고 풀에 넣어둔다.
        Poolable poolable = go.GetComponent<Poolable>();
        if(poolable != null)
        {
            Managers.Pool.Push(poolable);
            return;
        }

        Object.Destroy(go);
    }
}
