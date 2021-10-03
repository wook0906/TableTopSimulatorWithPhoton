using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Managers : MonoBehaviour
{
    static Managers instance;
    static Managers S
    {
        get
        {
            Init();
            return instance;
        }
    }
    //#region contents
    //GameManagerEX game = new GameManagerEX();
    //public static GameManagerEX Game { get { return S.game; } }
    //#endregion

    #region core
    //DataManager data = new DataManager();
    InputManager input = new InputManager();
    PoolManager pool = new PoolManager();
    ResourceManager resource = new ResourceManager();
    SceneManagerEx scene = new SceneManagerEx();
    SoundManager sound = new SoundManager();
    UIManager ui = new UIManager();

    //public static DataManager Data { get { return S.data; } }
    public static InputManager Input { get { return S.input; } }
    public static PoolManager Pool { get { return S.pool; } }
    public static ResourceManager Resource { get { return S.resource; } }
    public static SceneManagerEx Scene { get { return S.scene; } }
    public static SoundManager Sound { get { return S.sound; } }
    public static UIManager UI { get { return S.ui; } }
    #endregion

    static void Init()
    {
        if (instance == null)
        {
            GameObject go = GameObject.Find("Managers");
            if (go == null)
            {
                go = new GameObject { name = "Managers" };
                
            }
            DontDestroyOnLoad(go);
            instance = go.AddComponent<Managers>();

            //instance.data.Init();
            instance.pool.Init();
            instance.sound.Init();
        }
    }

    void Start()
    {
        Init();
    }

    // Update is called once per frame
    void Update()
    {
        input.OnUpdate();
    }

    public static void Clear()
    {
        Sound.Clear();
        Input.Clear();
        UI.Clear();
        Scene.Clear();

        Pool.Clear();
    }
}
