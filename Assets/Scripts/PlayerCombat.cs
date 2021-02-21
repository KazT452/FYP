using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    public GameObject Axe, Pickaxe;
    public Inventory inventory;
    public Tree tree;
    public Boulder boulder;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Tree")
        {
            Debug.Log("touchtree");
            tree = other.GetComponent<Tree>();
            tree.Chop();
        }        
        else
        {
            tree = null;
        }
        if (other.gameObject.tag == "Stone")
        {
            Debug.Log("touchstone");
            boulder = other.GetComponent<Boulder>();
            boulder.Mine();
        }
        else
        {
            boulder = null;
        }
    }
}
