using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.AI;
using UnityEngine.Events;



public class AIMoveToSound : MonoBehaviour
{
    public static Action<Vector3> playSound;
    [SerializeField] NavMeshAgent agent = null;

    void Start()
    {
        playSound += GoToThisPosition;
    }

    public void GoToThisPosition(Vector3 positionToGo)
    {
        agent.destination = positionToGo;
    }
    
}
