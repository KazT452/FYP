using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;
using TMPro;

public class DaysManager : MonoBehaviour
{
    public Player player;
    public ItemRespawner itemRespawner;
    public GameObject savePoint;
    public DayNightSystem dayNightSystem;
    public GameObject eyelidScreen;
    public GameObject diedText;
    public GameObject reviveBtn;
    public bool sleep;
    // Start is called before the first frame update
    void Start()
    {
        sleep = false;
        eyelidScreen.SetActive(false);
        itemRespawner = transform.GetComponent<ItemRespawner>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Sleep(bool alive)
    {
        Debug.Log("SLEEP");
        sleep = true;
        if (sleep)
        {
            player.revivePoint = player.transform.position;
            itemRespawner.TreeRockRespawn();
            itemRespawner.HerbSpawn();
            sleep = false;
            if (alive)
            {
                eyelidScreen.SetActive(true);
                Player.Head += 3;
                Player.Body += 3;
                Player.Arm += 3;
                Player.Legs += 3;
                Player.Hunger -= 50;
                StartCoroutine(WakeUp(false));
            }
            else
            {
                eyelidScreen.SetActive(true);
                int deadDamage = Random.Range(1, 5);
                Player.Head -= deadDamage;
                Player.Body -= deadDamage;
                Player.Arm -= deadDamage;
                Player.Legs -= deadDamage;
                Player.Hunger -= 100;
                StartCoroutine(WakeUp(true));

            }
            dayNightSystem.currentTimeofDay = 0.23f;
        }        
    }

    public IEnumerator WakeUp(bool tired)
    {
        yield return new WaitForSeconds(2f);
        player.tired = tired;
        player.Dead = false;
        eyelidScreen.SetActive(false);
        diedText.SetActive(false);
        FirstPersonController FPS = GameObject.FindGameObjectWithTag("Player").GetComponent<FirstPersonController>();
        FPS.GetComponent<UnityStandardAssets.Characters.FirstPerson.FirstPersonController>().enabled = true;
    }
}
