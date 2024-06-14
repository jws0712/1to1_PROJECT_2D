using OTO.Manager;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop : MonoBehaviour
{
    [SerializeField]
    private float maxHp = default;

    public float currentHp = default;

    private Animator animator = null;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void Start()
    {
        currentHp = maxHp;
    }

    private void Update()
    {
        animator.SetBool("isOpen", GameManager.instance.isStoreOpen);
    }

    public void TakeDamage(float damage)
    {
        currentHp -= damage;
    }


}
