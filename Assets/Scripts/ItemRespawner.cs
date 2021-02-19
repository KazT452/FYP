using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemRespawner : MonoBehaviour
{
    public Boulder _boulder;
    public List<Vector3>boudler_positions = new List<Vector3>();
    public List<Vector3> tree_Positions = new List<Vector3>();
    public int positionBank;
    public GameObject boulder;
    public GameObject tree;
    public TerrainData terrain;
    // Start is called before the first frame update
    void Start()
    {
        GameObject[] boulders = GameObject.FindGameObjectsWithTag("Stone");
        foreach (GameObject boulder in boulders)
        {
            boudler_positions.Add(boulder.transform.position);
        }
        //GameObject[] trees = GameObject.FindGameObjectsWithTag("Tree");
        //foreach (GameObject tree in trees)
        //{
        //    tree_Positions.Add(tree.transform.position);
        //}
        foreach(TreeInstance newtree in terrain.treeInstances)
        {
            Vector3 worldTreePos = Vector3.Scale(newtree.position, terrain.size) + Terrain.activeTerrain.transform.position;
            tree_Positions.Add(worldTreePos);
        }
        foreach(Vector3 treepos in tree_Positions)
        {
            Instantiate(tree, treepos, Quaternion.identity);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {

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
