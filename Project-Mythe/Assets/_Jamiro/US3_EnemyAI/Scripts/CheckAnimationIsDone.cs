using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckAnimationIsDone : MonoBehaviour
{
    [SerializeField] private Animator anim;
    [SerializeField] private BirdHandler birdHandler;
    [SerializeField] private Transform player;
    [SerializeField] private GameObject playerToTurnOff;
    [SerializeField] private GameObject CameraToTurnOn;
    private bool done = false;
    private float timeleft;
    // Start is called before the first frame update
    void Awake()
    {
        timeleft = this.anim.GetCurrentAnimatorClipInfo(0)[0].clip.length;
    }

    // Update is called once per frame
    void Update()
    {
        if (timeleft > 0)
        {
            timeleft -= Time.deltaTime;
        }
        else if (!done)
        { 
            done = true;
            Destroy(birdHandler.GetActiveBird());
            CameraToTurnOn.SetActive(true);
            playerToTurnOff.SetActive(false);
        }
    }
}
