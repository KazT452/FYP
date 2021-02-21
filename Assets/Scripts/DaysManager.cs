﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DaysManager : MonoBehaviour
{
    public Player player;
    public ItemRespawner itemRespawner;
    public GameObject savePoint;
    public DayNightSystem dayNightSystem;
    public GameObject eyelidScreen;
    public GameObject diedText;
    public bool sleep;
    // Start is called before the first frame update
    void Start()
    {
        sleep = false;
        eyelidScreen.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Sleep(bool alive)
    {
        GameObject[] trees = GameObject.FindGameObjectsWithTag("Tree");
        foreach(GameObject tree in trees)
        {
            tree.GetComponent<Tree>().dayOver += 1;
        }
        if (sleep)
        {
            sleep = false;
            if (alive)
            {
                eyelidScreen.SetActive(true);
                Player.Head += 3;
                Player.Body += 3;
                Player.Arm += 3;
                Player.Legs += 3;
            }

            else
            {
                eyelidScreen.SetActive(true);
                diedText.SetActive(true);
                int deadDamage = Random.Range(0, 5);
                Player.Head -= deadDamage;
                Player.Body -= deadDamage;
                Player.Arm -= deadDamage;
                Player.Legs -= deadDamage;
            }
            StartCoroutine(WakeUp());
        }        
    }

    IEnumerator WakeUp()
    {
        yield return new WaitForSeconds(2f);
        eyelidScreen.SetActive(false);
    }
}