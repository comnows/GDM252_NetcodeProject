using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HockeySrikerMovement : MonoBehaviour
{
    private Vector3 mOffset;
    private float mZCoord;

    // Start is called before the first frame update
    private void OnMouseDown()
    {
        mZCoord = Camera.main.WorldToScreenPoint(gameObject.transform.position).z;

        mOffset = gameObject.transform.position - GetMouseWorldPos();
    }

    private Vector3 GetMouseWorldPos()
    {
        Vector3 mousePoint = Input.mousePosition;

        mousePoint.z = mZCoord;

        return Camera.main.ScreenToWorldPoint(mousePoint);
    }

    private void OnMouseDrag() 
    {
        transform.position = GetMouseWorldPos() + mOffset;
    }
}
