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
    private bool walk = false;
    void Update()
    {
        if(walk)
        {
            if (agent.remainingDistance <= agent.stoppingDistance)
            {
                if (!agent.hasPath || agent.velocity.sqrMagnitude == 0f)
                {
                    animHandler.SetWalkAnimation(false);
                    walk = false;
                }
            }
        }    
    }
    void Awake()
    {
        playSound = null;
        playSound += GoToThisPosition;
        agent = this.GetComponent<NavMeshAgent>();
    }

    public void GoToThisPosition(Vector3 positionToGo)
    {
        agent.destination = positionToGo;
        animHandler.SetWalkAnimation(true);
        walk = true;
    }
    
}
