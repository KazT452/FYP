using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Herb : MonoBehaviour
{
    public ItemRespawner itemRespawner;
    public float distToGround = 1f;
    [SerializeField] private bool _isGrounded = false;
    // Start is called before the first frame update
    void Start()
    {
        itemRespawner = GameObject.FindGameObjectWithTag("GameController").GetComponent<ItemRespawner>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Physics.Raycast(transform.position, Vector3.down, distToGround+ 0.1f))
        {
            _isGrounded = true;
            Debug.Log("ground");
        }
        if (!_isGrounded)
        {
            Destroy(gameObject);
        }
    }

    public void Spawn()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.gameObject.name);

        if (other.tag == "Ground" && _isGrounded)
        {
            itemRespawner.herbPositions.Add(transform.position);
            Debug.Log("ADDED");
        }
        else
        {
            Debug.Log(other.gameObject.name + "DESTROY");
            Destroy(gameObject);
        }
    }
}
