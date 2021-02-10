using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PickUp : MonoBehaviour
{
    public GameObject Item;
    public bool canPickUp;
    public int sticks;
    public int stone;
    public int herb;
    public GameObject pickUpText;
    public TextMeshProUGUI pickupBox;

    //new
    public static bool pick;
    public static GameObject y;

    // Start is called before the first frame update
    void Start()
    {
        pick = false;
    }

    // Update is called once per frame
    void Update()
    {
        var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 1f))
        {
            Debug.Log(hit.transform);
            if (hit.collider.tag == "Item")
            {
                pickupBox.text = "Press E to pick up item";
                Debug.Log(hit.transform.name);

            }
        }
        else
        {
            pickupBox.text = " ";
        }
        if (canPickUp)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                Destroy(Item);
                Item = null;
                sticks += 1;
                pick = true;
                canPickUp = false;

            }
        }

        if (canPickUp)
        {
            pickUpText.SetActive(true);
        }
        else
        {
            pickUpText.SetActive(false);
        }

    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Item")
        {
            y = other.gameObject;
            Item = other.gameObject;
            canPickUp = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Item")
        {
            y = null;
            Item = null;
            canPickUp = false;
        }
    }
}
