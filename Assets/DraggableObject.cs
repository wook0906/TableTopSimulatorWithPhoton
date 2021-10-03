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

    private void Start()
    {
        PhotonNetwork.AddCallbackTarget(this);
        outline = GetComponent<Outline>();
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
        base.photonView.RequestOwnership();
        GetComponent<Rigidbody>().isKinematic = true;
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
        GetComponent<Rigidbody>().isKinematic = false;
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
}
