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
        pCombat = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerCombat>();
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
                Instantiate(rock, new Vector3(gameObject.transform.position.x, gameObject.transform.position.y+5+i, gameObject.transform.position.z), Quaternion.identity);
            }
            Destroy(gameObject);
        }
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
                Instantiate(rock, new Vector3(gameObject.transform.position.x, gameObject.transform.position.y+5, gameObject.transform.position.z), Quaternion.identity);
            }
        }
        else if (Player.attack && pCombat.Pickaxe.activeSelf)
        {
            Debug.Log("CHOP_AXE");
            drop = Random.Range(0, 3);
            health -= 5;
            if (drop == 2)
            {
                Instantiate(rock, new Vector3(gameObject.transform.position.x , gameObject.transform.position.y+5, gameObject.transform.position.z ), Quaternion.identity);
            }
        }
        else if (Player.attack && pCombat.Axe.activeSelf)
        {
            Debug.Log("CHOP_AXE");
            drop = Random.Range(0, 3);
            health -= 4;
            if (drop == 2)
            {
                Instantiate(rock, new Vector3(gameObject.transform.position.x , gameObject.transform.position.y+5, gameObject.transform.position.z ), Quaternion.identity);
            }
        }
    }
}
