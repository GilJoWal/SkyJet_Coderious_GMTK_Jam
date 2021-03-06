﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    GameManager manager;
    public Animator animator;

    public Transform attackPoint;
    public float attackRange = 1f;
    public LayerMask enemyLayer;

    int swordDamage = 20;

    // Start is called before the first frame update
    void Start()
    {
        manager = FindObjectOfType<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        animator.SetFloat("xSpeed", manager.xSpeed);
        animator.SetFloat("ySpeed", manager.ySpeed);
        animator.SetFloat("xLastSpeed", manager.xLastSpeed);
        animator.SetFloat("yLastSpeed", manager.yLastSpeed);
        animator.SetBool("isMoving", manager.isMoving);

        if(manager.hasAttacked)
        {
            animator.SetTrigger("Attack");
            manager.hasAttacked = false;

            AudioManager.instance.Play("Sword");

            Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayer);

            foreach(Collider2D enemy in hitEnemies)
            {
                if(!manager.c || !manager.t || !manager.r || !manager.l)
                {
                    AudioManager.instance.Play("BadHit");
                }

                enemy.GetComponent<EnemyGraphics>().TakeDamage(swordDamage);

                Vector3 newPos = enemy.GetComponent<Transform>().parent.GetComponent<Transform>().position - attackPoint.position;
                newPos.Normalize();

                enemy.GetComponent<Transform>().parent.GetComponent<Transform>().Translate(newPos * 2f);
            }
        }
    }
}
