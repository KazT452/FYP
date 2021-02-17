using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boulder : MonoBehaviour
{
    public ItemRespawner respawner;
    // Start is called before the first frame update
    void Start()
    {
        respawner = GameObject.FindGameObjectWithTag("GameController").GetComponent<ItemRespawner>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag != "Ground"||other==null||other.tag=="Untagged")
        {
            Debug.Log(other);
            Debug.Log("Respawn");
            respawner.Spawn();
            Destroy(gameObject);
        }
        else if(other.tag=="Ground")
        {
            for(int i = 0; i < respawner.positionBank; i++)
            {
                if (respawner.positions[i] != Vector3.zero)
                {
                    respawner.positions[i] = gameObject.transform.position;
                }
                else
                {
                    continue;
                }
            }
        }
    }

    public void Spawn()
    {

    }
}
