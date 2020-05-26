using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckAnimationIsDone : MonoBehaviour
{
    [SerializeField] private Animator anim;
    [SerializeField] private GameObject enemy;
    [SerializeField] private GameObject enemyOnHead;
    private bool done = false;
    private float timeleft;
    // Start is called before the first frame update
    void Awake()
    {
        timeleft = this.anim.GetCurrentAnimatorClipInfo(0)[0].clip.length;
        enemy.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
    }

    // Update is called once per frame
    void Update()
    {

        if (!done && timeleft > 0)
        {
            timeleft -= Time.deltaTime;
            enemy.transform.Lerp(enemyOnHead.transform, Time.deltaTime);
        }
        else if (!done)
        { 
            done = true;
            Destroy(enemy);
            enemyOnHead.SetActive(true);
        }


    }
}
