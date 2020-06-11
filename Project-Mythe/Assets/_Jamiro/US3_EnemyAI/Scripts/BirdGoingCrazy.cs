using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Valve.VR.InteractionSystem;

public class BirdGoingCrazy : MonoBehaviour
{
    [SerializeField] private NavMeshAgent agent = null;
    [SerializeField] private Transform[] positionsToGo = null;
    private int index;
    private bool crazyModes = false;

    // Update is called once per frame
    void Update()
    {
        if (!agent.pathPending && crazyModes)
        {
            if (agent.remainingDistance <= agent.stoppingDistance)
            {
                if (agent.hasPath || agent.velocity.sqrMagnitude == 0f)
                {
                    NextPosition();
                }
            }
        }
    }

    public void GoCrazy()
    {
        if (crazyModes == false)
        {
            NextPosition();
            crazyModes = true;
        }
        

    }

    public void NextPosition()
    {
        if (index < positionsToGo.Length)
        {
            agent.destination = positionsToGo[index].position;
            index++;
        }
        else
        {
            crazyModes = false;
            index = 0;
        }

    }
}
