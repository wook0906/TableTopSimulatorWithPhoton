using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ObjectControl_Popup : UIPopup
{
    DraggableObject obj;
    GameObject panel;
    Camera targetCam;
    Text lockText;

    enum GameObjects
    {
        GridPanel,
    }
    enum Buttons
    {
        Lock_Button,
        Close_Button,
    }
    enum Texts
    {
        Lock_Text,
    }
    // Start is called before the first frame update

    public override void Init()
    {
        base.Init();

        Bind<GameObject>(typeof(GameObjects));
        Bind<Button>(typeof(Buttons));
        Bind<Text>(typeof(Texts));

        Get<Button>((int)Buttons.Lock_Button).onClick.AddListener(new UnityEngine.Events.UnityAction(() =>
        {
            OnClickLockButton();
        }));
        Get<Button>((int)Buttons.Close_Button).onClick.AddListener(new UnityEngine.Events.UnityAction(() =>
        {
            Managers.UI.ClosePopupUI(this);
        }));

        panel = Get<GameObject>((int)GameObjects.GridPanel);

        Vector3 pos = targetCam.WorldToScreenPoint(obj.transform.position);
        pos.x += 125f;
        pos.y -= 200f;
        panel.transform.position = pos;

        lockText = Get<Text>((int)Texts.Lock_Text);
        if(obj.State == Define.ObjectState.Lock)
        {
            lockText.text = "Unlock";
        }
        else
        {
            lockText.text = "Lock";
        }
    }
    public void SetObject(DraggableObject obj, Camera targetCam)
    {
        this.obj = obj;
        this.targetCam = targetCam;
    }
    void OnClickLockButton()
    {
        if (obj.State != Define.ObjectState.Lock)
        {
            obj.RequestRPCChangeState(Define.ObjectState.Lock);
            lockText.text = "UnLock";
        }
        else
        {
            obj.RequestRPCChangeState(Define.ObjectState.Idle);
            lockText.text = "Lock";
        }
    }
}
