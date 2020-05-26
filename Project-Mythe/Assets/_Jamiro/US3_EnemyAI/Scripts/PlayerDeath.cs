using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerDeath : MonoBehaviour
{
    [SerializeField] private NavMeshAgent EnemyAgent;
    [SerializeField] private GameObject deathBox = null;
    [SerializeField] private GameObject enemy;
    [SerializeField] private float minimalDistance;
    private bool playerIsTargeted;

    void Update()
    {
        if (playerIsTargeted &&Vector3.Distance(transform.position, enemy.transform.position) < minimalDistance)
        {
            DeathByLookingInHisEyes();
            playerIsTargeted = false;
        }
    }
    public void DeathByLookingInHisEyes()
    {
        deathBox.SetActive(true);
    }
    public void SetDestenationToKill()
    {
        playerIsTargeted = true;
        EnemyAgent.destination = transform.position;
    }

}
