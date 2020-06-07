using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BirdHandler : MonoBehaviour
{
    [SerializeField] private GameObject[] enemyNachtKrapps = null;
    [SerializeField] private Transform[] enemyHeads = null;
    private NavMeshAgent[] enemyAgents = null;
    private int index = 0;
    
    public void changeBird()
    {
        if (index < enemyNachtKrapps.Length)
        {
            enemyNachtKrapps[index].SetActive(false);
            index++;
            enemyNachtKrapps[index].SetActive(true);
        } 
    }
    void Start()
    {
        enemyAgents = new NavMeshAgent[enemyNachtKrapps.Length + 1];
        for(int i = 0; i < enemyNachtKrapps.Length; i++)
        {
            if (enemyNachtKrapps[i].GetComponent<NavMeshAgent>() != null)
            {
                enemyAgents[i] = enemyNachtKrapps[i].GetComponent<NavMeshAgent>();
            }
        }
    }

    public GameObject GetActiveBird()
    {
        return enemyNachtKrapps[index];
    }
    public NavMeshAgent GetAgent()
    {
        return enemyAgents[index];
    }
    public Transform ActiveHead()
    {
        return enemyHeads[index];
    }
}
