using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;

public class UIController : MonoBehaviour
{
    public Player player;
    //Inventory
    public GameObject Inventory;
    public bool inventoryClosed;
    public GameObject healSelect;

    //Crafting
    public GameObject Crafting;
    public bool craftingisClosed;

    //Cooking
    public GameObject Cooking;
    public bool cookingisClosed;

    //Quest
    public GameObject Quest;
    public bool questisClosed;

    //Pause
    public bool pauseGame;
    public GameObject PauseMenu;

    public FirstPersonController FPS;


    // Start is called before the first frame update
    void Start()
    {
        inventoryClosed = true;
        craftingisClosed = true;
        cookingisClosed = true;
        questisClosed = true;
        pauseGame = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (!pauseGame)
        {
            if (Input.GetKeyDown(KeyCode.Tab))
            {
                if (inventoryClosed)
                {
                    Inventory.SetActive(true);
                    inventoryClosed = false;
                }
                else
                {
                    Inventory.SetActive(false);
                    inventoryClosed = true;
                }
            }           
        }        

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!pauseGame && inventoryClosed && craftingisClosed&&cookingisClosed&&questisClosed)
            {
                pauseGame = true;
                PauseMenu.SetActive(true);
                Time.timeScale = 0;
            }
            else if (pauseGame && inventoryClosed && craftingisClosed&&cookingisClosed&&questisClosed)
            {
                pauseGame = false;
                PauseMenu.SetActive(false);
                Time.timeScale = 1;
            }
            else if (!inventoryClosed)
            {
                Inventory.SetActive(false);
                inventoryClosed = true;
            }
            else if (!craftingisClosed)
            {
                Crafting.SetActive(false);
                craftingisClosed = true;
            }
            else if (!cookingisClosed)
            {
                Cooking.SetActive(false);
                cookingisClosed= true;
            }
            else if (!questisClosed)
            {
                Quest.SetActive(false);
                questisClosed = true;
            }
        }

        if (!craftingisClosed || !inventoryClosed||!cookingisClosed||!questisClosed|| pauseGame)
        {
            FPS.GetComponent<UnityStandardAssets.Characters.FirstPerson.FirstPersonController>().enabled = false;
            Cursor.lockState = CursorLockMode.None;
            Cursor.lockState = CursorLockMode.Confined;
            Cursor.visible = true;
        }
        else
        {
            FPS.GetComponent<UnityStandardAssets.Characters.FirstPerson.FirstPersonController>().enabled = true;
        }
    }

    public void ContinueBtn()
    {
        pauseGame = false;
        PauseMenu.SetActive(false);
        Time.timeScale = 1;
    }

    public void uiQuit()
    {
        Application.Quit();
    }
}
