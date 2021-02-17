using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemRespawner : MonoBehaviour
{
    public Boulder _boulder;
    public Vector3[] positions;
    public int positionBank;
    public GameObject boulder;
    // Start is called before the first frame update
    void Start()
    {
        positionBank = 15;
        for (int i = 0; i < positionBank; i++)
        {
            Spawn();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        GameObject[] boulders = GameObject.FindGameObjectsWithTag("Stone");
        foreach(GameObject boulder in boulders)
        {
            if (collision.gameObject == gameObject)
            {

            }
        }        
    }

    public void Spawn()
    {
        float randXpos = Random.Range(150, 1860);
        float randYpos = Random.Range(6, 55);
        float randZpos = Random.Range(0,1720);
        int randYrot = Random.Range(0, 90);
        Instantiate(boulder, new Vector3(randXpos,randYpos,randZpos),Quaternion.Euler(0f,randYrot,0f));
    }
}
