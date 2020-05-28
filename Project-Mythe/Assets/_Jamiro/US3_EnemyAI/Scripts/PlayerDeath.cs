using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerDeath : MonoBehaviour
{
    [SerializeField] private GameObject deathBox = null;
    [SerializeField] private float minimalDistance;
    [SerializeField] private BirdHandler birdHandler;
    private bool playerIsTargeted;
    void Update()
    {
        if (playerIsTargeted && Vector3.Distance(transform.position, birdHandler.GetActiveBird().transform.position) < minimalDistance )
        {
            DeathByLookingInHisEyes();
            playerIsTargeted = false;
        }
    }


    public void SetPlayerIsTargeted(bool _bool)
    {
        playerIsTargeted = _bool;
    }
    public void DeathByLookingInHisEyes()
    {
        deathBox.SetActive(true);
    }
    public void SetDestenationToKill()
    {
        playerIsTargeted = true;
        birdHandler.GetAgent().destination = transform.position;
    }

}
