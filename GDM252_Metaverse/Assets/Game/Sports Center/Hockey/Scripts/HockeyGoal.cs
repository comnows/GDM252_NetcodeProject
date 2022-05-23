using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HockeyGoal : MonoBehaviour
{
    public HockeyPuck hockeyPuck;
    public int id;

    private void OnTriggerEnter(Collider other) 
    {
        if(other.CompareTag("Puck"))
        {
            hockeyPuck.GoalTriggerEnter(id);
        }
    }
}
