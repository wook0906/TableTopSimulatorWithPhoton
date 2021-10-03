using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class Inven_SubItem: UIBase
{
    enum Texts
    {
        ItemName
    }
    enum Buttons
    {
        ItemButton
    }

    string name;
    public GameObject probPrefab;

    public override void Init()
    {
        Bind<Text>(typeof(Texts));
        Bind<Button>(typeof(Buttons));
        Get<Text>((int)Texts.ItemName).text = this.name;
        

        //Get<Button>((int)Buttons.ItemButton).BindEvent((PointerEventData) => Debug.Log($"{this.name} Å¬¸¯!"));
        Get<Button>((int)Buttons.ItemButton).onClick.AddListener(new UnityAction(() =>
        {

        }));
    }

    public void SetInfo(string name)
    {
        this.name = name;
        probPrefab = Managers.Resource.Load<GameObject>(name);
        if (!probPrefab)
        {
            Debug.Log("SetInfo Error!");
        }
    }
}
