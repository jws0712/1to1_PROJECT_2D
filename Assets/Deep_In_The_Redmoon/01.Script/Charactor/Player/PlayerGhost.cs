namespace OTO.Charactor.Player
{

    //System
    using System.Collections;
    using System.Collections.Generic;

    //UnityEngine
    using UnityEngine;

    /// <summary>
    /// 플레이어의 대시시 잔상을 관리하는 클래스
    /// </summary>
    public class PlayerGhost : MonoBehaviour
    {
        [Header("GhostSetting")]
        [SerializeField] private float ghostDelay;
        [SerializeField] private GameObject ghost;
        [SerializeField] private float destoryTime;

        [HideInInspector]
        public bool makeGhost = false;

        //private variables
        private float ghostDelaySeconds;

        private void Start()
        {
            ghostDelaySeconds = ghostDelay;
        }

        /// <summary>
        /// 플레이어가 대시를 할때 잔상을 소환하는 코드
        /// </summary>
        private void Update()
        {
            if (makeGhost)
            {
                if (ghostDelaySeconds > 0)
                {
                    ghostDelaySeconds -= Time.deltaTime;
                }
                else
                {
                    GameObject currentGhost = Instantiate(ghost, transform.position, transform.rotation);
                    Sprite currentSprite = GetComponent<SpriteRenderer>().sprite;
                    currentGhost.transform.localScale = transform.localScale;
                    currentGhost.GetComponent<SpriteRenderer>().sprite = currentSprite;
                    ghostDelaySeconds = ghostDelay;
                    Destroy(currentGhost, destoryTime);
                }
            }
        }
    }
}


