using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;
public class HockeyStrikerMoverment : NetworkBehaviour
{
    private Rigidbody playerRigidbody;
    private Vector3 mOffset;
    private Vector3 movePosition;
    public float speed = 20f;
    private float mZCoord;

    private void Start() 
    {
        playerRigidbody = GetComponent<Rigidbody>();
    }
    private void OnMouseDown()
    {
        if(IsClient && IsOwner)
        {
        mZCoord = Camera.main.WorldToScreenPoint(gameObject.transform.position).z;
        mOffset = gameObject.transform.position - GetMouseWorldPos();
        }
    }

    private Vector3 GetMouseWorldPos()
    {
        Vector3 mousePoint = Input.mousePosition;
        mousePoint.z = mZCoord;

        return Camera.main.ScreenToWorldPoint(mousePoint);
    }

    private void OnMouseDrag() 
    {
        if(IsClient && IsOwner)
        {
        movePosition = GetMouseWorldPos() + mOffset;
        // velocity = movePosition - transform.position;
        movePosition = Vector3.Normalize(movePosition - transform.position) * speed;

        // playerRigidbody.velocity = velocity;
        playerRigidbody.AddForce(movePosition);
        // playerRigidbody.MovePosition(movePosition);
        }
    }
}
