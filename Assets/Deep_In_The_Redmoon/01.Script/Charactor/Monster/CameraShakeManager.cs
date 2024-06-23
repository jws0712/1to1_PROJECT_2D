using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShakeManager : MonoBehaviour
{
    public static CameraShakeManager instance;

    private Animator animator = null;

    private void Awake()
    {
        instance = this;

        animator = GetComponent<Animator>();
    }

    public void PlayShake(string name)
    {
        animator.SetTrigger(name);
    }
}
