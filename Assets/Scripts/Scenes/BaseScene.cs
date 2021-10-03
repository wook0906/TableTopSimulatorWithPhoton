using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using Photon.Pun;

public abstract class BaseScene : MonoBehaviourPunCallbacks
{
    Define.Scene _sceneType = Define.Scene.Unknown;

    public Define.Scene SceneType { get; protected set; }

    void Awake()
    {
        Init();
    }

    protected virtual void Init()
    {
        Object obj = GameObject.FindObjectOfType(typeof(EventSystem));
        if (obj == null)
            Managers.Resource.Instantiate("UI/EventSystem").name = "@EventSystem";
    }
    public abstract void Clear();
}
