using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerDeath : MonoBehaviour
{
    [SerializeField] private NavMeshAgent EnemyAgent;
    [SerializeField] private GameObject deathBox = null;
    [SerializeField] private GameObject enemy;
    private float timeLeft;
    private bool timer;

    void Update()
    {
        if(timer)
        {
            timeLeft -= Time.deltaTime;
            print(timeLeft);
            if(timeLeft < 0)
            {
                DeathByLookingInHisEyes();
                timer = false;
            }
        }
    }
    public void DeathByLookingInHisEyes()
    {
        deathBox.SetActive(true);
    }
    public void SetDestenationToKill()
    {
        float distance = Vector3.Distance(transform.position, enemy.transform.position);
        print(distance);
        timeLeft = distance / 25;
        timer = true;
        EnemyAgent.destination = transform.position;
    }

}
