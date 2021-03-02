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
        foreach (Vector3 treepos in tree_Positions)
        {
            Instantiate(tree, treepos, Quaternion.identity);
        }
    }

    // Update is called once per frame
    void Update()
    {

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

    public void TreeRockRespawn()
    {
        GameObject[] boulders = GameObject.FindGameObjectsWithTag("Stone");
        foreach (Vector3 boudlerPos in boudler_positions)
        {
            for(int i =0; i < boulders.Length; i++)
            {
                if (boudlerPos == boulders[i].transform.position)
                {
                    break;
                }
                else if (i == boulders.Length - 1)
                {
                    Debug.Log("SpawnRock");
                    Instantiate(boulder, boudlerPos, Quaternion.identity);
                }
            }
        }
        GameObject[] trees = GameObject.FindGameObjectsWithTag("Tree");
        foreach (Vector3 treePos in tree_Positions)
        {
            for (int i = 0; i < trees.Length; i++)
            {
                if (treePos == trees[i].transform.position)
                {
                    break;
                }
                else if (i == trees.Length - 1)
                {
                    Debug.Log("SpawnTree");
                    Instantiate(tree, treePos, Quaternion.identity);
                }
            }
        }
    }
}
