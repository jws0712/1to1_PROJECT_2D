using OTO.Manager;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �ΰ��� UI �ִϸ��̼��� ���� ��Ű�� �Լ�
/// </summary>
public class CanvasController : MonoBehaviour
{
    private Animator animator;

    /// <summary>
    /// ������Ʈ �ʱ�ȭ
    /// </summary>
    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    /// <summary>
    /// �ִϸ��̼��� �����ϴ� �ڵ�
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
    /// �ΰ��� UI�ִϸ��̼Ǹ� �����Ű�� �Լ�
    /// </summary>
    private void SetPlayerUI()
    {
        animator.SetTrigger("SetPlayerUI");
    }
}
