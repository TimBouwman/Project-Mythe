//author: Tim Bouwman
//Github: https://github.com/TimBouwman
using UnityEngine;

namespace Management
{
    public class GameManager : MonoBehaviour
    {
        #region Variables
        private GameObject player;
        /// <summary> The size the player object should be relative to the size of the player irl </summary>
        private static Vector3 PLAYER_SCALE;
        public Vector3 PlayerScale { set { PLAYER_SCALE = value; } }
        #endregion

        #region Unity Methods
        private void Awake()
        {
            Calibrator.playerheight += playerScale => PlayerScale = playerScale;
            player = GameObject.FindWithTag("Player");
            //scales the player only when it has a custom scale and when it has a reference to the player object
            if (player != null && PLAYER_SCALE != Vector3.zero)
                player.transform.localScale = PLAYER_SCALE;
        }
        #endregion
    }
}