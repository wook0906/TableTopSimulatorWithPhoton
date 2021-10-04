using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class DraggableObject : MonoBehaviourPunCallbacks,IPunOwnershipCallbacks
{
    //private Vector3 mOffset;
    //private float mZCoord;
    Camera targetCamera;
    [HideInInspector]
    public Outline outline;
    Vector3 dist;
    Vector3 startPos;
    float posX;
    float posZ;
    float posY;
    float deltaY;

    [SerializeField]
    Define.ObjectState state = Define.ObjectState.Idle;
    public Define.ObjectState State
    {
        get { return state; }
    }

    private void Start()
    {
        PhotonNetwork.AddCallbackTarget(this);
        outline = GetComponent<Outline>();
        Managers.Input.keyAction -= OnKeyEvent;
        Managers.Input.keyAction += OnKeyEvent;
    }

    private void OnDestroy()
    {
        PhotonNetwork.RemoveCallbackTarget(this);
    }
    public void SetCamera(Camera camera)
    {
        targetCamera = camera;    
    }
    void OnMouseDown()
    {
        if (state == Define.ObjectState.Pickked || 
            state == Define.ObjectState.Lock) return;
        base.photonView.RequestOwnership();
        RequestRPCChangeState(Define.ObjectState.Pickked);

        startPos = transform.position;
        dist = targetCamera.WorldToScreenPoint(transform.position);
        posX = Input.mousePosition.x - dist.x;
        posY = Input.mousePosition.y - dist.y;
        posZ = Input.mousePosition.z - dist.z;
        outline.enabled = true;
    }
    //Vector3 GetMouseWorldPos()
    //{
    //    Vector3 mousePos = Input.mousePosition;
    //    mousePos.z = mZCoord;
    //    return targetCamera.ScreenToWorldPoint(mousePos);
    //}
    private void OnMouseDrag()
    {
        if (state == Define.ObjectState.Lock) return;
        if (state != Define.ObjectState.Pickked || !photonView.Owner.IsLocal) return;

        if (targetCamera.GetComponent<CameraController>().curTargetingObj == this.gameObject)
        {
            deltaY += 0.1f;
        }
        
        float disX = Input.mousePosition.x - posX;
        float disY = Input.mousePosition.y - posY;
        float disZ = Input.mousePosition.z - posZ;
        Vector3 lastPos = targetCamera.ScreenToWorldPoint(new Vector3(disX, disY, disZ));

        Vector3 pos = transform.position;
        pos.y -= transform.localScale.y;
        Vector3 dir = pos - transform.position;

        transform.position = new Vector3(lastPos.x, targetCamera.GetComponent<CameraController>().hitPoint.y + (transform.localScale.y * 2f)+deltaY, lastPos.z);
    }
    private void OnMouseOver()
    {
        outline.enabled = true;
    }
    private void OnMouseExit()
    {
        outline.enabled = false;
    }
    private void OnMouseUp()
    {
        if (state == Define.ObjectState.Lock) return;

        RequestRPCChangeState(Define.ObjectState.Idle);
        outline.enabled = false;
        deltaY = 0f;
        SetCamera(null);
    }

    public void OnOwnershipRequest(PhotonView targetView, Player requestingPlayer)
    {
        if (targetView != base.photonView)
            return;

        base.photonView.TransferOwnership(requestingPlayer);
    }

    public void OnOwnershipTransfered(PhotonView targetView, Player previousOwner)
    {
        
    }
    
    public void OnOwnershipTransferFailed(PhotonView targetView, Player senderOfFailedRequest)
    {
       
    }
    void OnKeyEvent()
    {
        if (state != Define.ObjectState.Pickked) return;

        if (Input.GetKey(KeyCode.Q))
        {
            transform.Rotate(0f, -1f, 0f,Space.World);
        }
        else if (Input.GetKey(KeyCode.E))
        {
            transform.Rotate(0f, 1f, 0f, Space.World);
        }
    }

    [PunRPC]
    private void RPCChangeState(Define.ObjectState newState)
    {
        switch (newState)
        {
            case Define.ObjectState.Idle:
                GetComponent<Rigidbody>().isKinematic = false;
                break;
            case Define.ObjectState.Pickked:
                GetComponent<Rigidbody>().isKinematic = true;
                break;
            case Define.ObjectState.Lock:
                GetComponent<Rigidbody>().isKinematic = true;
                break;
            default:
                break;
        }
        state = newState;

        //Debug.Log($"{photonView.InstantiationId} is ChageState To {newState}");
    }
    public void OnControlPanel()
    {
        if (Managers.UI.IsExistPopup<ObjectControl_Popup>())
        {
            Managers.UI.ClosePopupUI<ObjectControl_Popup>();
        }
        else
        {
            Managers.UI.ShowPopupUI<ObjectControl_Popup>().SetObject(this, targetCamera);   
        }
        
    }

    public void RequestRPCChangeState(Define.ObjectState newState)
    {
        photonView.RPC("RPCChangeState", RpcTarget.All, newState);
    }
}
