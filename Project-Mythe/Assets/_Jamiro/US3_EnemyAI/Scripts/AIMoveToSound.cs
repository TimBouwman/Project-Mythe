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
    [SerializeField] private BirdAnimationHandler animHandler;
    private bool walk = true;
    void Update()
    {
        if(walk)
        {
            if (agent.remainingDistance <= agent.stoppingDistance)
            {
                if (!agent.hasPath || agent.velocity.sqrMagnitude == 0f)
                {
                    animHandler.SetWalkAnimation(false);
                }
            }
        }    
    }
    void Start()
    {
        playSound += GoToThisPosition;
    }

    public void GoToThisPosition(Vector3 positionToGo)
    {
        agent.destination = positionToGo;
        animHandler.SetWalkAnimation(true);
    }
    
}
