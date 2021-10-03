using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.UI;

public class MatchMaker : MonoBehaviourPunCallbacks {

    private readonly string gameVersion = "1";
    public Button joinButton;

	void Start () {

		Debug.Log ("start");
        PhotonNetwork.GameVersion = gameVersion;
        PhotonNetwork.ConnectUsingSettings ();

        joinButton.interactable = false;
	}


    public override void OnConnectedToMaster()
    {
        Debug.Log("OnConnectedToMaster() was called by PUN");
        joinButton.interactable = true;
    }
    public override void OnDisconnected(DisconnectCause cause)
    {
        joinButton.interactable = false;
        Debug.Log($"offline : connection disabled {cause.ToString()}");

        PhotonNetwork.ConnectUsingSettings();
    }
    public void Connect()
    {
        joinButton.interactable = false;
        if (PhotonNetwork.IsConnected)
        {
            Debug.Log("Connecting to random room...");
            PhotonNetwork.JoinRandomRoom();
        }
        else
        {
            Debug.Log($"offline : connection disabled try reconnection...");
            PhotonNetwork.ConnectUsingSettings();
        }
    }
    public override void OnJoinRoomFailed(short returnCode, string message)
    {
        Debug.Log("There is no empty! Creating new Room....");
        PhotonNetwork.CreateRoom(null, new RoomOptions { MaxPlayers = 4 });
    }
    public override void OnJoinedRoom()
    {
        //      Debug.Log("OnJoinedRoom() called by PUN. Now this client is in a room.");

        //float randomX = Random.Range(-6f, 6f); 

        //      PhotonNetwork.Instantiate(
        //	photonObject.name,
        //	new Vector3(randomX, 1f, 0f),
        //	Quaternion.identity, 
        //	0
        //);

        //GameObject mainCamera = GameObject.FindWithTag("MainCamera");

        Debug.Log("Connected with Room");
        PhotonNetwork.LoadLevel("Main");
    }

	public override void OnJoinRandomFailed(short returnCode, string message)
    {
        Debug.Log("OnJoinRandomFailed() was called by PUN. No random room available, so we create one.\nCalling: PhotonNetwork.CreateRoom");

        // #Critical: we failed to join a random room, maybe none exists or they are all full. No worries, we create a new room.
        PhotonNetwork.CreateRoom(null);

		//PhotonNetwork.CreateRoom(null, new RoomOptions{MaxPlayers = 4});
    }
   

}
