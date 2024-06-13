namespace OTO.Charactor.Player
{

    //System
    using System.Collections;
    using System.Collections.Generic;

    //UnityEngine
    using UnityEngine;


    public class PlayerGhost : MonoBehaviour
    {
        [Header("GhostSetting")]
        [SerializeField] private float ghostDelay;
        [SerializeField] private GameObject ghost;
        [SerializeField] private float destoryTime;

        //public variables
        public bool makeGhost = false;

        //private variables
        private float ghostDelaySeconds;

        private void Start()
        {
            ghostDelaySeconds = ghostDelay;
        }

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


