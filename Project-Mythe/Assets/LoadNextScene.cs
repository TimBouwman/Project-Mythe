using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class LoadNextScene : MonoBehaviour
{
    [SerializeField] private Animator anim;
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
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }
}