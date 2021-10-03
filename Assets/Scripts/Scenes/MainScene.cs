using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class MainScene : BaseScene
{
    public GameObject playerPrefab;
    public GameObject tmpProbs;

    protected override void Init()
    {
        base.Init();
        SceneType = Define.Scene.Main;

        MainScene_UI mainScene_UI = Managers.UI.ShowSceneUI<MainScene_UI>();
        mainScene_UI.transform.position = Vector3.zero;

        SpawnPlayer();
        if (PhotonNetwork.IsMasterClient)
        {
            //방장이 할 것?
        }
    }
    private void Update()
    {
        
    }
    public override void Clear()
    {

    }
    void SpawnPlayer()
    {
        int localPlayerIdx = PhotonNetwork.LocalPlayer.ActorNumber - 1;
        Vector3 spawnPosition = new Vector3(Random.Range(0f, 10f), 5f, Random.Range(0f, 10f));
        PhotonNetwork.Instantiate(playerPrefab.name, spawnPosition, Quaternion.identity);
    }

    public override void OnLeftRoom()
    {
        Managers.Scene.LoadScene(Define.Scene.Login);
    }
}
