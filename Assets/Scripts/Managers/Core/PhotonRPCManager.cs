using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class PhotonRPCManager
{
    [PunRPC]
    public void CreateObject()
    {
        PhotonNetwork.InstantiateRoomObject("Prob3", new Vector3(Random.Range(0, 10), Random.Range(0, 10), Random.Range(0, 10)), Quaternion.identity);
    }

    public void RequestRPCChangeState(DraggableObject obj, Define.ObjectState newState)
    {
        obj.photonView.RPC("ChangeState", RpcTarget.All, newState);
    }
}
