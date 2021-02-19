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
        }
        if (health <= 0)
        {
            for(int i = 0; i <= 2; i++)
            {
                Instantiate(stick, new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, gameObject.transform.position.z),Quaternion.identity);
            }
            Destroy(gameObject);
        }
    }

    public void Chop()
    {
        int drop;
        Debug.Log(Player.unarm);
        if (Player.attack && Player.unarm)
        {
            Debug.Log("CHOP_Hand");
            drop = Random.Range(0, 5);
            health -= 3;
            Player.Arm -= 2;
            if(drop == 2)
            {
                Instantiate(stick, new Vector3(gameObject.transform.position.x + 2, gameObject.transform.position.y, gameObject.transform.position.z + 2), Quaternion.identity);
            }
        }
        else if (Player.attack && pCombat.Axe.activeSelf)
        {
            Debug.Log("CHOP_AXE");
            drop = Random.Range(0,3);
            health -= 5;
            if (drop == 2)
            {
                Instantiate(stick, new Vector3(gameObject.transform.position.x + 2, gameObject.transform.position.y, gameObject.transform.position.z + 2), Quaternion.identity);
            }
        }
        else if (Player.attack && pCombat.Pickaxe.activeSelf)
        {
            Debug.Log("CHOP_AXE");
            drop = Random.Range(0,3);
            health -= 4;
            if (drop == 2)
            {
                Instantiate(stick, new Vector3(gameObject.transform.position.x + 2, gameObject.transform.position.y, gameObject.transform.position.z + 2), Quaternion.identity);
            }
        }
        
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Debug.Log("PLAYER");
        }
    }
}
