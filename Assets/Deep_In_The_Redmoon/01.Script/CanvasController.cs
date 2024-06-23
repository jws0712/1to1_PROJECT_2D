using OTO.Manager;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasController : MonoBehaviour
{
    private Animator animator;
    private void Awake()
    {
        animator = GetComponent<Animator>();
    }
    private void Update()
    {
        if(GameManager.instance.isPlayerSpawn == true)
        {
           
            SetPlayerUI();
        }
        else
        {
            return;
        }
    }

    private void SetPlayerUI()
    {
        animator.SetTrigger("SetPlayerUI");
    }
}
