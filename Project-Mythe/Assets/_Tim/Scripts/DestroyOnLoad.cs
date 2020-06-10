//author: Tim Bouwman
//Github: https://github.com/TimBouwman
using UnityEngine;

public class DestroyOnLoad : MonoBehaviour
{
    private void Awake()
    {
        Destroy(this.gameObject);
    }
}