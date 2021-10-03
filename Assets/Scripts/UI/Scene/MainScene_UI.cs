using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class MainScene_UI : UIScene
{
    public bool isOnInventory = false;
    enum Buttons
    {
        Probs_Button
    }
    // Start is called before the first frame update

    public override void Init()
    {
        base.Init();

        Bind<Button>(typeof(Buttons));

        Get<Button>((int)Buttons.Probs_Button).onClick.AddListener(new UnityAction(()=>
        {
            if (!isOnInventory)
            {
                isOnInventory = true;
                Managers.UI.ShowPopupUI<ProbInventory_Popup>();
            }
            else
            {
                Managers.UI.ClosePopupUI();
                isOnInventory = false;
            }
        }));
    }
}
