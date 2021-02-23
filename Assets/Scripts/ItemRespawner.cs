using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemRespawner : MonoBehaviour
{
    public Boulder _boulder;
    public DaysManager daysManager;
    public List<Vector3>boudler_positions = new List<Vector3>();
    public List<Vector3> tree_Positions = new List<Vector3>();
    public List<Vector3> herbPositions = new List<Vector3>();
    public List<Vector3> herbSpawnedPos = new List<Vector3>();

    public TerrainData terrain;

    #region Item
    public GameObject treesCollection;
    public GameObject herbsCollection;
    public GameObject boulder;
    public GameObject tree;
    public GameObject herb;
    #endregion
    // Start is called before the first frame update
    void Start()
    {
        HerbSpawn();
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
        //foreach(TreeInstance newtree in terrain.treeInstances)
        //{
        //    List<TreeInstance> newTrees = new List<TreeInstance>(0);
        //    terrain.treeInstances = newTrees.ToArray();
        //}
        foreach (Vector3 treepos in tree_Positions)
        {
            Instantiate(tree, treepos, Quaternion.identity,treesCollection.transform);
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
        float posRandX = Random.Range(350f, 1750f);
        float posRandZ = Random.Range(120f, 1720f);
        float posRandY = Random.Range(1.4f, 3f);
        Instantiate(herb, new Vector3(posRandX, posRandY, posRandZ), Quaternion.identity);
    }

    public void HerbSpawn()
    {
        for (int i = 0; i < 5; i++)
        {

            for (int k = 1; k < herbSpawnedPos.Count+2; k+=2)
            {
                RandomAgain:
                int RandPos = Random.Range(0, herbPositions.Count);
                if (herbSpawnedPos.Count > 0)
                {
                    if (k < herbSpawnedPos.Count)
                    {

                        if (herbPositions[RandPos] != herbSpawnedPos[k-1])
                        {
                            continue;
                        }
                        else
                        {
                            Debug.Log("GOBACK");
                            goto RandomAgain;
                        }
                    }
                    else
                    {
                        if (herbPositions[RandPos] != herbSpawnedPos[herbSpawnedPos.Count-1])
                        {
                            Instantiate(herb, herbPositions[RandPos], Quaternion.identity);
                            herbSpawnedPos.Add(herbPositions[RandPos]);
                            break;
                        }
                        else
                        {
                            goto RandomAgain;
                        }
                    }
                }
                else
                {
                    Instantiate(herb, herbPositions[RandPos], Quaternion.identity);
                    herbSpawnedPos.Add(herbPositions[RandPos]);
                }
            }
        }
    }
}
