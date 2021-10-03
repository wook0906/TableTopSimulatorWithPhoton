using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using UnityEngine.EventSystems;

public class UIButton : UIPopup
{
    enum Buttons
    {
        Button
    }
    enum Texts
    {
        Text
    }
    enum Images
    { 
        Icon

    }


    public override void Init()
    {
        base.Init();

        Bind<Button>(typeof(Buttons));
        Bind<Text>(typeof(Texts));
        Bind<Image>(typeof(Images));

        GameObject imageGO = GetImage((int)Images.Icon).gameObject;
        AddUIEvent(imageGO, (PointerEventData data) => { imageGO.transform.position = data.position; }, Define.UIEvent.Drag);

        GetButton((int)Buttons.Button).gameObject.BindEvent(OnClickButton);
    }
    
    public void OnClickButton(PointerEventData data)
    {
        Debug.Log("Button Clicked!!");
    }

}
