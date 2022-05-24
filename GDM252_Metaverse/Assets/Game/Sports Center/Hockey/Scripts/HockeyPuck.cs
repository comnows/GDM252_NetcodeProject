using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HockeyPuck : MonoBehaviour
{
    public event Action<int> onGoal;

    public Rigidbody puckRigidbody;
    
    public Transform spawnPositionLeft;
    public Transform spawnPositionRight;
    private Vector3 originPosition;

    private void Start() 
    {
        originPosition = this.transform.position;
    }

    public void ResetPosition(int id)
    {
        puckRigidbody.velocity = Vector3.zero;
        
        if(id == 1)
        {
            transform.position = spawnPositionLeft.position;
        }
        else
        {
            transform.position = spawnPositionRight.position;
        }
    }

    public void GoalTriggerEnter(int id)
    {
        onGoal?.Invoke(id);
    }

    public void ResetOriginPosition()
    {
        transform.position = originPosition;
    }
}
