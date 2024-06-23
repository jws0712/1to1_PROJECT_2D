using OTO.Manager;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Unity.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Shop : MonoBehaviour
{
    [SerializeField]
    private float maxHp = default;
    [SerializeField] private Slider houseHpSlider = null;

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
        houseHpSlider.value = currentHp/maxHp;
        animator.SetBool("isOpen", GameManager.instance.isStoreOpen);

        if(GameManager.instance.isFieldClear == true)
        {
            currentHp = maxHp;
        }

        if(currentHp <= 0 )
        {
            GameManager.instance.GameOver();
        }
    }

    public void TakeDamage(float damage)
    {
        currentHp -= damage;
    }
}
