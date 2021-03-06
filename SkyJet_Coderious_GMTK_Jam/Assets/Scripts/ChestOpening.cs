﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestOpening : MonoBehaviour
{
    public GameObject enemy;
    GameManager manager;

    private void Start()
    {
        manager = FindObjectOfType<GameManager>();
    }
    // Start is called before the first frame update
    private void OnTriggerEnter2D(Collider2D collision)
    {
        manager.isUnderControl = false;
        manager.upKey.text = "";
        manager.downKey.text = "";
        manager.leftKey.text = "";
        manager.rightKey.text = "";
        manager.attackKey.text = "";

        AudioManager.instance.Play("OpenChest");

        enemy.SetActive(true);
        gameObject.SetActive(false);

        if(GameData.instance.difficulty == 1)
            enemy.GetComponent<Pathfinding.AIPath>().maxSpeed = 0.25f;

        if (GameData.instance.difficulty == 2)
            enemy.GetComponent<Pathfinding.AIPath>().maxSpeed = 0.5f;

        if (GameData.instance.difficulty == 3)
            enemy.GetComponent<Pathfinding.AIPath>().maxSpeed = 0.75f;
    }

}
