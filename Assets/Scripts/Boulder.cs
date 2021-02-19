using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boulder : MonoBehaviour
{
    public ItemRespawner respawner;
    public PlayerCombat pCombat;
    public Terrain terrain;
    public int health;
    public GameObject rock;
    public int daysOver;

    // Start is called before the first frame update
    void Awake()
    {
        respawner = GameObject.FindGameObjectWithTag("GameController").GetComponent<ItemRespawner>();
        terrain = GameObject.FindGameObjectWithTag("Ground").GetComponent<Terrain>();
    }

    private void Start()
    {
        health = 30;
    }

    // Update is called once per frame
    void Update()
    {
        if (daysOver == 3)
        {
            daysOver = 0;
        }
        if (health <= 0)
        {
            for (int i = 0; i <= 2; i++)
            {
                Instantiate(rock, new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, gameObject.transform.position.z), Quaternion.identity);
            }
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        //if (other.gameObject.tag != "Ground"||other==null||other.tag=="Untagged")
        //{
        //    if(gameObject.transform.position.y!= terrain.terrainData.alphamapHeight)
        //    {
        //        Debug.Log(other);
        //        Debug.Log("Respawn");
        //        respawner.Spawn();
        //        Destroy(gameObject);
        //    }           
        //}
        //else if(other.tag=="Ground")
        //{
        //    for(int i = 0; i < respawner.positionBank; i++)
        //    {
        //        if (respawner.positions[i] != Vector3.zero)
        //        {
        //            respawner.positions[i] = gameObject.transform.position;
        //        }
        //        else
        //        {
        //            continue;
        //        }
        //    }
        //}
    }

    public void Mine()
    {
        int drop;
        Debug.Log(Player.unarm);
        if (Player.attack && Player.unarm)
        {
            Debug.Log("CHOP_Hand");
            drop = Random.Range(0, 5);
            health -= 3;
            Player.Arm -= 2;
            if (drop == 2)
            {
                Instantiate(rock, new Vector3(gameObject.transform.position.x + 2, gameObject.transform.position.y, gameObject.transform.position.z + 2), Quaternion.identity);
            }
        }
        else if (Player.attack && pCombat.Pickaxe.activeSelf)
        {
            Debug.Log("CHOP_AXE");
            drop = Random.Range(0, 3);
            health -= 5;
            if (drop == 2)
            {
                Instantiate(rock, new Vector3(gameObject.transform.position.x + 2, gameObject.transform.position.y, gameObject.transform.position.z + 2), Quaternion.identity);
            }
        }
        else if (Player.attack && pCombat.Axe.activeSelf)
        {
            Debug.Log("CHOP_AXE");
            drop = Random.Range(0, 3);
            health -= 4;
            if (drop == 2)
            {
                Instantiate(rock, new Vector3(gameObject.transform.position.x + 2, gameObject.transform.position.y, gameObject.transform.position.z + 2), Quaternion.identity);
            }
        }
    }
}
