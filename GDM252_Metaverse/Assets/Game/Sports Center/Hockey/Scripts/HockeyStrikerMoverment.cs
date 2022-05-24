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
    private bool isOwn;
    private PlayerInteract [] players;
    private PlayerInteract player;
    private void Start() 
    {
        isOwn = false;
        playerRigidbody = GetComponent<Rigidbody>();
        FindPlayerScript();
    }
    private void OnMouseDown()
    {
        if(OwnerClientId == player.OwnerClientId)
        {
        mZCoord = Camera.main.WorldToScreenPoint(gameObject.transform.position).z;
        mOffset = gameObject.transform.position - GetMouseWorldPos();
        Debug.Log("OnMouseDown");
        }
    }

    // public void SetObjectID()
    // {
    //     var drag = this.transform.GetComponent<NetworkObject>();
    //         if (drag != null)
    //         {
    //             if (!drag.IsOwner)
    //             {
    //                 ChangeBombStateServerRpc(drag.OwnerClientId, drag.NetworkObjectId);
    //             }
    //         }
    // }

    // [ServerRpc]
    // public void ChangeBombStateServerRpc(ulong clientId, ulong networkId)
    // {
    //     List<NetworkObject> networkObjects = NetworkManager.Singleton.ConnectedClients[clientId].OwnedObjects;
    //     foreach (NetworkObject networkObject in networkObjects)
    //     {   
    //         if (networkObject.NetworkObjectId == networkId)
    //         {
    //             isOwn = true;
    //         }
    //     }    
    // }
    private void FindPlayerScript()
    {
        players = GameObject.FindObjectsOfType<PlayerInteract>();
        foreach (PlayerInteract n in players)
        {
            if (n.IsOwner)
            {
                player = n;
            }
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
        if(OwnerClientId == player.OwnerClientId)
        {
        movePosition = GetMouseWorldPos() + mOffset;
        // velocity = movePosition - transform.position;
        movePosition = Vector3.Normalize(movePosition - transform.position) * speed;

        // playerRigidbody.velocity = velocity;
        playerRigidbody.AddForce(movePosition);
        // playerRigidbody.MovePosition(movePosition);
        }
        Debug.Log("OnMouseDrag");
    }
}
