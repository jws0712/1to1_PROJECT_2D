using OTO.Manager;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 인게임 UI 애니매이션을 실행 시키는 함수
/// </summary>
public class CanvasController : MonoBehaviour
{
    private Animator animator;

    /// <summary>
    /// 컴포넌트 초기화
    /// </summary>
    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    /// <summary>
    /// 애니매이션을 실행하는 코드
    /// </summary>
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

    /// <summary>
    /// 인게임 UI애니매이션를 실행시키는 함수
    /// </summary>
    private void SetPlayerUI()
    {
        animator.SetTrigger("SetPlayerUI");
    }
}
