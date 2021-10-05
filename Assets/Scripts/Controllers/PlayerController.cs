using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
public class PlayerController : MonoBehaviourPunCallbacks
{
    [HideInInspector]
    public Camera cam;
    public float moveSpeed = 3f;

    private void Start()
    {
        if (!photonView.IsMine)
        {
            GetComponent<Camera>().enabled = false;
            return;
        }
        Init();
    }
    // Update is called once per frame
    void Update()
    {
        if (!photonView.IsMine) return;
        UpdateMoving(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));

        
    }

    public void Init()
    {
        cam = GetComponent<Camera>();
        Managers.Input.keyAction -= OnKeyEvent;
        Managers.Input.keyAction += OnKeyEvent;
        Managers.Input.mouseEventAction -= OnMouseEvent;
        Managers.Input.mouseEventAction += OnMouseEvent;
    }

    protected void UpdateMoving(float xAxis, float zAxis)
    {
        Vector3 dir = new Vector3(xAxis, 0f, zAxis).normalized;
        transform.Translate(dir * Time.deltaTime * moveSpeed);
    }

    void OnMouseEvent(Define.MouseEvent evt)
    {
        //RaycastHit hit;
        //Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        //bool raycastHit = Physics.Raycast(ray, out hit, Mathf.Infinity, _mask);

        //Debug.DrawRay(Camera.main.transform.position, ray.direction * Mathf.Infinity, Color.red, 1.0f);

        switch (evt)

        {
            case Define.MouseEvent.LeftPress:
                //if (_lockTarget == null && raycastHit)
                //    _destPos = hit.point;
                break;
            case Define.MouseEvent.LeftPointerDown:
                //if (raycastHit)
                //{
                //    _destPos = hit.point;
                //    State = Define.State.Moving;
                //    _stopSkill = false;
                //    if (hit.collider.gameObject.layer == (int)Define.Layer.Monster)
                //    {
                //        _lockTarget = hit.collider.gameObject;
                //    }
                //    else
                //    {
                //        _lockTarget = null;
                //    }
                //}
                break;
            case Define.MouseEvent.LeftPointerUp:
            //_stopSkill = true;
            //break;
            case Define.MouseEvent.LeftClick:
                break;
            default:
                break;
        }
    }
    void OnKeyEvent()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            Managers.RPC.CreateObject();
        }
    }
}
