using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tree : MonoBehaviour
{
    public int dayOver;
    public int health;
    public GameObject stick;
    public Inventory inventory;
    public PlayerCombat pCombat;
    // Start is called before the first frame update
    void Start()
    {
        health =30;
        pCombat = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerCombat>();
    }

    // Update is called once per frame
    void Update()
    {
        if (dayOver == 3)
        {
            dayOver = 0;
            health = 30;
            gameObject.SetActive(true);
        }
        if (health <= 0)
        {
            for(int i = 0; i <= 2; i++)
            {
                Instantiate(stick, new Vector3(gameObject.transform.position.x+i, gameObject.transform.position.y+i, gameObject.transform.position.z+i),Quaternion.identity);
            }
            gameObject.SetActive(false);
        }
    }

    public void Chop()
    {
        int drop;
        Debug.Log(Player.unarm);
        if(Player.Arm >= 15)
        {
            if (Player.attack && Player.unarm)
            {
                Debug.Log("CHOP_Hand");
                drop = Random.Range(0, 5);
                health -= 3;
                Player.Arm -= 2;
                if (drop == 2)
                {
                    Instantiate(stick, new Vector3(gameObject.transform.position.x + 5, gameObject.transform.position.y+3, gameObject.transform.position.z + 5), Quaternion.identity);
                }
            }
            else if (Player.attack && pCombat.Axe.activeSelf)
            {

                drop = Random.Range(0, 3);
                health -= 5;
                if (drop == 2)
                {
                    Instantiate(stick, new Vector3(gameObject.transform.position.x + 5, gameObject.transform.position.y+3, gameObject.transform.position.z + 5), Quaternion.identity);
                }
            }
            else if (Player.attack && pCombat.Pickaxe.activeSelf)
            {
                Debug.Log("CHOP_AXE");
                drop = Random.Range(0, 3);
                health -= 4;
                if (drop == 2)
                {
                    Instantiate(stick, new Vector3(gameObject.transform.position.x + 5, gameObject.transform.position.y+3, gameObject.transform.position.z + 5), Quaternion.identity);
                }
            }
        }
        else if (Player.Arm < 15 && Player.Arm > 5)
        {
            if (Player.attack && Player.unarm)
            {
                Debug.Log("CHOP_Hand");
                drop = Random.Range(0, 5);
                health -= 2;
                Player.Arm -= 2;
                if (drop == 2)
                {
                    Instantiate(stick, new Vector3(gameObject.transform.position.x + 5, gameObject.transform.position.y+3, gameObject.transform.position.z + 5), Quaternion.identity);
                }
            }
            else if (Player.attack && pCombat.Axe.activeSelf)
            {
                Debug.Log("CHOP_AXE");
                drop = Random.Range(0, 3);
                health -= 4;
                if (drop == 2)
                {
                    Instantiate(stick, new Vector3(gameObject.transform.position.x + 5, gameObject.transform.position.y + 3, gameObject.transform.position.z + 5), Quaternion.identity);
                }
            }
            else if (Player.attack && pCombat.Pickaxe.activeSelf)
            {
                Debug.Log("CHOP_AXE");
                drop = Random.Range(0, 3);
                health -= 3;
                if (drop == 2)
                {
                    Instantiate(stick, new Vector3(gameObject.transform.position.x + 5, gameObject.transform.position.y + 3, gameObject.transform.position.z + 5), Quaternion.identity);
                }
            }
        }
        else if (Player.Arm < 5)
        {
            if (Player.attack && Player.unarm)
            {
                Debug.Log("CHOP_Hand");
                drop = Random.Range(0, 5);
                health -= 1;
                Player.Arm -= 2;
                if (drop == 2)
                {
                    Instantiate(stick, new Vector3(gameObject.transform.position.x + 5, gameObject.transform.position.y + 3, gameObject.transform.position.z + 5), Quaternion.identity);
                }
            }
            else if (Player.attack && pCombat.Axe.activeSelf)
            {
                Debug.Log("CHOP_AXE");
                drop = Random.Range(0, 3);
                health -= 3;
                if (drop == 2)
                {
                    Instantiate(stick, new Vector3(gameObject.transform.position.x + 5, gameObject.transform.position.y + 3, gameObject.transform.position.z + 5), Quaternion.identity);
                }
            }
            else if (Player.attack && pCombat.Pickaxe.activeSelf)
            {
                Debug.Log("CHOP_AXE");
                drop = Random.Range(0, 3);
                health -= 2;
                if (drop == 2)
                {
                    Instantiate(stick, new Vector3(gameObject.transform.position.x + 5, gameObject.transform.position.y + 3, gameObject.transform.position.z + 5), Quaternion.identity);
                }
            }
        }
        
        
    }
}
