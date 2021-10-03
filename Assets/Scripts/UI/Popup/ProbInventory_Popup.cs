using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProbInventory_Popup : UIPopup
{
    enum GameObjects
    {
        GridPanel
    }
    // Start is called before the first frame update


    public override void Init()
    {
        base.Init();

        Bind<GameObject>(typeof(GameObjects));
        GameObject gridPanel = Get<GameObject>((int)GameObjects.GridPanel);


        for (int i = 0; i < 8; i++)
        {
            GameObject item = Managers.UI.MakeSubItem<Inven_SubItem>(gridPanel.transform).gameObject;
            Inven_SubItem invenItem = item.GetOrAddComponent<Inven_SubItem>();
            invenItem.SetInfo($"Temp Prob {i + 1}");
        }
    }
}
