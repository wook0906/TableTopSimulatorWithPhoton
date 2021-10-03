using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolManager
{
    #region Pool Class
    class Pool
    {
        public GameObject Original { get; private set; }
        public Transform Root { get; set; }

        Stack<Poolable> poolStack = new Stack<Poolable>();

        public void Init(GameObject original, int count = 5)
        {
            Original = original;
            Root = new GameObject().transform;
            Root.name = $"{original.name}_Root";

            for (int i = 0; i < count; i++)
            {
                Push(Create());
            }
        }

        Poolable Create()
        {
            GameObject go = Object.Instantiate<GameObject>(Original);
            go.name = Original.name;
            return go.GetOrAddComponent<Poolable>();
        }

        public void Push(Poolable poolable)
        {
            if (poolable == null) return;

            poolable.transform.parent = Root;
            poolable.gameObject.SetActive(false);
            poolable.isUsing = false;

            poolStack.Push(poolable);
        }
        
        public Poolable Pop(Transform parent)
        {
            Poolable poolable;
            if (poolStack.Count > 0)
                poolable = poolStack.Pop();
            else
                poolable = Create();

            poolable.gameObject.SetActive(true);

            //DontDestroy해제 용도
            if (parent == null)
                poolable.transform.parent = Managers.Scene.currentScene.transform;

            poolable.transform.parent = parent;
            poolable.isUsing = true;

            return poolable;
        }
    }
    #endregion

    Dictionary<string, Pool> _pool = new Dictionary<string, Pool>();

    Transform root;

    public void Init()
    {
        if(root == null)
        {
            root = new GameObject { name = "@Pool_Root" }.transform;
            Object.DontDestroyOnLoad(root);
        }    
    }
    public void CreatePool(GameObject original, int count = 5)
    {
        Pool pool = new Pool();
        pool.Init(original, count);
        pool.Root.parent = root;

        _pool.Add(original.name, pool);
    }

    public void Push(Poolable poolable)
    {
        string name = poolable.gameObject.name;

        if (_pool.ContainsKey(name) == false)
        {
            //전달받은 poolable이 들어갈 pool이 없는경우
            GameObject.Destroy(poolable.gameObject);
            return;
        }

        _pool[name].Push(poolable);
    }

    public Poolable Pop(GameObject originPrefab, Transform parent = null)
    {
        if (_pool.ContainsKey(originPrefab.name) == false)
            CreatePool(originPrefab);

        return _pool[originPrefab.name].Pop(parent);
    }
    public GameObject GetOriginal(string name)
    {
        if (_pool.ContainsKey(name) == false)
        {
            return null;
        }


        return _pool[name].Original;
    }

    public void Clear()
    {
        foreach (Transform child in root)
        {
            GameObject.Destroy(child.gameObject);
        }
        _pool.Clear();
    }
}
