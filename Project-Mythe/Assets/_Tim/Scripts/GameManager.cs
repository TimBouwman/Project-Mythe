//author: Tim Bouwman
//Github: https://github.com/TimBouwman
using UnityEngine;

namespace Management
{
    /// <summary>
    /// 
    /// </summary>
    public class GameManager : MonoBehaviour
    {
        #region Variables
        private GameObject player;
        private static Vector3 playerScale;
        public Vector3 PlayerScale { set { playerScale = value; } }
        #endregion

        #region Unity Methods
        private void Awake()
        {
            Calibrator.playerHight += playerScale => PlayerScale = playerScale;
            player = GameObject.FindWithTag("Player");
            if (player != null && playerScale != Vector3.zero)
                player.transform.localScale = playerScale;
        }
        #endregion
    }
}