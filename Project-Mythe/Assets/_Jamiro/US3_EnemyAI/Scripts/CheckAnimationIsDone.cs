using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckAnimationIsDone : MonoBehaviour
{
    [SerializeField] private Animator anim;
    [SerializeField] private GameObject enemy;
    [SerializeField] private GameObject enemyOnHead;
    [SerializeField] private Transform player;
    [SerializeField] private Transform deathPosition;
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
            Destroy(enemy);
            enemyOnHead.SetActive(true);
            player.transform.position = deathPosition.position;
        }
    }
}
