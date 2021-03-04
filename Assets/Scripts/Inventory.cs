using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Inventory : MonoBehaviour
{
    public UIController uiControl;
    public PlayerCombat pCombat;
	public Player player;

    public List<Item> yourInventory = new List<Item>();
    public List<Item> draggedItem = new List<Item>();

    public int slotsNumber;

    public GameObject x;
    public int n;

    public Image[] slot;
    public Sprite[] slotSprite;
    public Image[] qSlot;
    public Sprite[] qSlotSprite;

    public TextMeshProUGUI[] stackText;

    public int a;
    public int b;

    public int[] slotStack;
    public int maxStack;

    public int slotTemporary;

    public int rest;
    public bool shift;

    public bool canConsume;
    public bool canHeal;

    //crafting
    public Image[] SlotInCrafting;
    public Sprite[] SlotInCraftingSprite;

    public Image craftedItem;
    public Sprite craftedItemSprite;

    public int craftableItemId;
    public int firstCraftableItemId;
    public int lastCraftableItemId;

    public TextMeshProUGUI craftedItemName;

    public TextMeshProUGUI[] craftingText;

    public GameObject craftBtn;

    public bool craft;

    //cooking
    public Image[] SlotinCooking;
    public Sprite[] SlotinCookingSprite;

    public Image cookedItem;
    public Sprite cookedItemSprite;

    public int cookableItemId;
    public int firstCookableItemId;
    public int lastCookableItemId;

    public TextMeshProUGUI cookedItemName;
    public TextMeshProUGUI[] cookingText;

    public GameObject cookBtn;

    public bool cook;

    //Equip

    // Start is called before the first frame update
    void Start()
    {
        player = transform.GetComponent<Player>();
        //Cooking items in database
        firstCookableItemId = 5;
        lastCookableItemId = 5;
        cookableItemId = 5;
        //Crafting items in database
        firstCraftableItemId = 6;
        lastCraftableItemId = 7;
        craftableItemId = 6;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            if (qSlot[0].sprite != ItemDatabase.itemList[0].itemSprite)
            {
                if (pCombat.Pickaxe.activeSelf)
                {
                    pCombat.Pickaxe.SetActive(false);
                    pCombat.Axe.SetActive(true);
                }
                else if (!pCombat.Axe.activeSelf)
                {
                    Player.unarm = false;
                    pCombat.Axe.SetActive(true);
                }
                else if(pCombat.Axe.activeSelf)
                {
                    Player.unarm = true;
                    pCombat.Axe.SetActive(false);
                }
            }
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            if (qSlot[1].sprite != ItemDatabase.itemList[0].itemSprite)
            {
                if (pCombat.Axe.activeSelf)
                {
                    pCombat.Axe.SetActive(false);
                    pCombat.Pickaxe.SetActive(true);
                }
                else if (!pCombat.Pickaxe.activeSelf)
                {
                    Player.unarm = false;
                    pCombat.Pickaxe.SetActive(true);
                }
                else
                {
                    Player.unarm = true;
                    pCombat.Pickaxe.SetActive(false);
                }
            }
        }
        for (int i = 0; i < slotsNumber; i++)
        {
            if (slotStack[i] == 0)
            {
                yourInventory[i] = ItemDatabase.itemList[0];
            }
        }

        if (Input.GetKeyDown("left shift"))
        {
            shift = true;
        }

        if(Input.GetKeyUp("left shift"))
        {
            shift = false;
        }

        for (int i = 0; i < slotsNumber; i++)
        {            
            if (yourInventory[i].id == 0)
            {
                stackText[i].text = " ";
            }
            else
            {
                stackText[i].text = " " + slotStack[i];
            }
        }

        for (int i = 0; i < slotsNumber; i++)
        {
            slot[i].sprite = slotSprite[i];
            slotSprite[i] = yourInventory[i].itemSprite;

        }

        for (int i = 0; i < slotsNumber; i++)
        {
            if (yourInventory[i].id == 6)
            {
                qSlot[0].sprite = yourInventory[i].itemSprite;
                break;
            }
            else
            {
                qSlot[0].sprite = ItemDatabase.itemList[0].itemSprite;
            }
        }
        for (int i = 0; i < slotsNumber; i++)
        {
            if (yourInventory[i].id == 7)
            {
                qSlot[1].sprite = yourInventory[i].itemSprite;
                break;
            }
            else
            {
                qSlot[1].sprite = ItemDatabase.itemList[0].itemSprite;
            }
        }

        

        if (PickUp.y != null)
        {
            x = PickUp.y;
            n = x.GetComponent<ThisItem>().thisId;
        }
        else
        {
            x = null;
        }

        if(PickUp.pick == true)
        {
            for (int i = 0; i < slotsNumber; i++)
            {
                //ItemPickup:

                if (yourInventory[i].id == n)
                {
                    if (slotStack[i] >= maxStack)
                    {
                        continue;
                    }
                    else
                    {

                        slotStack[i] += 1;
                        i = slotsNumber;
                        PickUp.pick = false;
                    }                                                       
                }
            }

            for (int i = 0; i < slotsNumber; i++)
            {
                if (yourInventory[i].id == 0 && PickUp.pick == true)
                {
                    yourInventory[i] = ItemDatabase.itemList[n];
                    slotStack[i] += 1;
                    PickUp.pick = false;
                }
            }
            PickUp.pick = false;
        }
        //Comsumable
        if (!uiControl.inventoryClosed)
        {
            if (yourInventory[b].consumable == true)
            {
                canConsume = true;
            }
            else
            {
                canConsume = false;
            }

            if (yourInventory[b].healing)
            {
                canHeal = true;
            }
            else
            {
                canHeal = false;
            }

            if (!canHeal && canConsume && Input.GetButtonDown("Fire1"))
            {
                Debug.Log("EAT");
                if (Player.Hunger < player.maxHunger)
                {
                    if (yourInventory[b] == ItemDatabase.itemList[4])
                    {
                        if (slotStack[b] == 1)
                        {
                            Player.Body -= 3;
                            Player.Hunger += 25;
                            yourInventory[b] = ItemDatabase.itemList[0];
                            slotStack[b] = 0;
                        }
                        else
                        {
                            slotStack[b]--;
                            Player.Body -= 3;
                            Player.Hunger += 25;
                        }
                    }
                    else if (yourInventory[b] == ItemDatabase.itemList[5])
                    {
                        if (slotStack[b] == 1)
                        {
                            Player.Hunger += 25;
                            yourInventory[b] = ItemDatabase.itemList[0];
                            slotStack[b] = 0;
                        }
                        else
                        {
                            slotStack[b]--;
                            Player.Hunger += 25;
                        }
                    }
                }
            }
            else if (canConsume && canHeal && Input.GetButtonDown("Fire1"))
            {
                Debug.Log("HEAL");
                uiControl.Inventory.SetActive(true);
                uiControl.healSelect.SetActive(true);
            }
        }      

        for (int i = 0; i < slotsNumber; i++)
        {
            stackText[i].text = "" + slotStack[i];
        }

        //Crafting
        craftedItemName.text = "" + ItemDatabase.itemList[craftableItemId].name;

        craftedItemSprite = ItemDatabase.itemList[craftableItemId].itemSprite;
        craftedItem.sprite = craftedItemSprite;

        SlotInCraftingSprite[0] = ItemDatabase.itemList[ItemDatabase.itemList[craftableItemId].n1].itemSprite;
        SlotInCraftingSprite[1] = ItemDatabase.itemList[ItemDatabase.itemList[craftableItemId].n2].itemSprite;
        SlotInCraftingSprite[2] = ItemDatabase.itemList[ItemDatabase.itemList[craftableItemId].n3].itemSprite;

        SlotInCrafting[0].sprite = SlotInCraftingSprite[0];
        SlotInCrafting[1].sprite = SlotInCraftingSprite[1];
        SlotInCrafting[2].sprite = SlotInCraftingSprite[2];

        craftingText[0].text = "" + ItemDatabase.itemList[craftableItemId].q1;
        craftingText[1].text = "" + ItemDatabase.itemList[craftableItemId].q2;
        craftingText[2].text = "" + ItemDatabase.itemList[craftableItemId].q3;

        int craftA = 0;
        int craftB = 0;
        int craftC = 0;

        for (int i = 0; i < slotsNumber; i++)
        {
            if (yourInventory[i].id == ItemDatabase.itemList[craftableItemId].n1)
            {
                craftA += slotStack[i];
            }
        }

        for (int i = 0; i < slotsNumber; i++)
        {
            if (yourInventory[i].id == ItemDatabase.itemList[craftableItemId].n2)
            {
                craftB += slotStack[i];
            }
        }

        for (int i = 0; i < slotsNumber; i++)
        {
            if (yourInventory[i].id == ItemDatabase.itemList[craftableItemId].n3)
            {
                craftC += slotStack[i];
            }
        }

        if (craftA >= ItemDatabase.itemList[craftableItemId].q1 && craftB >= ItemDatabase.itemList[craftableItemId].q2 && craftC >= ItemDatabase.itemList[craftableItemId].q3)
        {
            craft = true;
            craftBtn.SetActive(true);
        }
        else
        {
            craft = false;
            craftBtn.SetActive(false);
        }

        //Cooking
        cookedItemName.text = "" + ItemDatabase.itemList[cookableItemId].name;

        cookedItemSprite = ItemDatabase.itemList[cookableItemId].itemSprite;
        cookedItem.sprite = cookedItemSprite;

        SlotinCookingSprite[0] = ItemDatabase.itemList[ItemDatabase.itemList[cookableItemId].n1].itemSprite;
        SlotinCookingSprite[1] = ItemDatabase.itemList[ItemDatabase.itemList[cookableItemId].n2].itemSprite;

        SlotinCooking[0].sprite = SlotinCookingSprite[0];
        SlotinCooking[1].sprite = SlotinCookingSprite[1];

        cookingText[0].text = "" + ItemDatabase.itemList[cookableItemId].q1;
        cookingText[1].text = "" + ItemDatabase.itemList[cookableItemId].q2;

        int cookA = 0;
        int cookB = 0;

        for (int i = 0; i < slotsNumber; i++)
        {
            if (yourInventory[i].id == ItemDatabase.itemList[cookableItemId].n1)
            {
                cookA += slotStack[i];
            }
        }

        for (int i = 0; i < slotsNumber; i++)
        {
            if (yourInventory[i].id == ItemDatabase.itemList[cookableItemId].n2)
            {
                cookB += slotStack[i];
            }
        }

        if (cookA >= ItemDatabase.itemList[cookableItemId].q1 && cookB >= ItemDatabase.itemList[cookableItemId].q2)
        {
            cook = true;
            cookBtn.SetActive(true);
        }
        else
        {
            cook = false;
            cookBtn.SetActive(false);
        }
    }

    public void StartDrag(Image slotX)
    {
        print("start drag: " + slotX.name);
        for (int i = 0; i < slotsNumber; i++)
        {
            if (slot[i] == slotX)
            {
                a = i;
            }
        }
    }

    public void Drop(Image slotX)
    {
        //half item
        if (shift)
        {
            if (yourInventory[b].id == 0)
            {
                slotStack[b] = slotStack[a] / 2;
                rest = slotStack[a] % 2;
                slotStack[a] = slotStack[a] / 2 + rest;
            }
        }
        //switch item slot
        else
        {


            print("stop drag: " + slotX.name);
            if (a != b)
            {
                if (yourInventory[a].id != yourInventory[b].id)
                {
                    draggedItem[0] = yourInventory[a];
                    slotTemporary = slotStack[a];
                    yourInventory[a] = yourInventory[b];
                    slotStack[a] = slotStack[b];
                    yourInventory[b] = draggedItem[0];
                    slotStack[b] = slotTemporary;
                    a = 0;
                    b = 0;
                }
                else
                {
                    if (slotStack[a] + slotStack[b] <= maxStack)
                    {
                        slotStack[b] = slotStack[a] + slotStack[b];
                        yourInventory[a] = ItemDatabase.itemList[0];
                    }
                    else
                    {
                        slotStack[a] = slotStack[a] + slotStack[b] - maxStack;
                        slotStack[b] = maxStack;
                    }
                }
            }
        }
    }

    public void Enter(Image slotX)
    {
        print("Enter");
        for (int i = 0; i < slotsNumber; i++)
        {
            if (slot[i] == slotX)
            {
                b = i;
            }
        }
    }

    public void PreviousItem()
    {
        if (craftableItemId > firstCraftableItemId)
        {
            craftableItemId--;
        }
    }
    
    public void NextItem()
    {
        if (craftableItemId < lastCraftableItemId)
        {
            craftableItemId++;
        }
    }

    public void CraftItem()
    {
        int a = 0;
        int b = 0;
        int c = 0;

        for (int i = 0; i < slotsNumber; i++)
        {
            if (yourInventory[i].id == ItemDatabase.itemList[craftableItemId].n1)
            {
                a += slotStack[i];
            }
        }

        for (int i = 0; i < slotsNumber; i++)
        {
            if (yourInventory[i].id == ItemDatabase.itemList[craftableItemId].n2)
            {
                b += slotStack[i];
            }
        }

        for (int i = 0; i < slotsNumber; i++)
        {
            if (yourInventory[i].id == ItemDatabase.itemList[craftableItemId].n3)
            {
                c += slotStack[i];
            }
        }
        if (a >= ItemDatabase.itemList[craftableItemId].q1 && b >= ItemDatabase.itemList[craftableItemId].q2 && c >= ItemDatabase.itemList[craftableItemId].q3)
        {
            craft = true;
        }
        else
        {
            craft = false;
        }
        if (craft == true)
        {
            a = ItemDatabase.itemList[craftableItemId].q1;
            b = ItemDatabase.itemList[craftableItemId].q2;
            c = ItemDatabase.itemList[craftableItemId].q3;

            for (int i = 0; i < slotsNumber; i++)
            {
                if (yourInventory[i].id == craftableItemId)
                {
                    if (slotStack[i] == maxStack)
                    {
                        continue;
                    }
                    else
                    {
                        slotStack[i] += 1;
                        i = slotsNumber;
                    }

                    for (int j = 0; j < slotsNumber; j++)
                    {
                        if (yourInventory[j].id == ItemDatabase.itemList[craftableItemId].n1 && a > 0)
                        {
                            if (slotStack[j] > a)
                            {
                                slotStack[j] -= a;
                                a = 0;
                            }
                            else
                            {
                                a -= slotStack[j];
                                slotStack[j] = 0;
                                yourInventory[j] = ItemDatabase.itemList[0];

                            }
                        }

                    }

                    for (int k = 0; k < slotsNumber; k++)
                    {
                        if (yourInventory[k].id == ItemDatabase.itemList[craftableItemId].n2 && b > 0)
                        {
                            if (slotStack[k] > b)
                            {
                                slotStack[k] -= b;
                                b = 0;
                            }
                            else
                            {
                                b -= slotStack[k];
                                slotStack[k] = 0;
                                yourInventory[k] = ItemDatabase.itemList[0];

                            }
                        }

                    }

                    for (int l = 0; l < slotsNumber; l++)
                    {
                        if (yourInventory[l].id == ItemDatabase.itemList[craftableItemId].n3 && c > 0)
                        {
                            if (slotStack[l] > c)
                            {
                                slotStack[l] -= c;
                                c = 0;
                            }
                            else
                            {
                                c -= slotStack[l];
                                slotStack[l] = 0;
                                yourInventory[l] = ItemDatabase.itemList[0];

                            }
                        }
                    }
                    craft = false;

                }
            }
            for (int i = 0; i < slotsNumber; i++)
            {
                if (yourInventory[i].id == 0 && craft == true)
                {
                    yourInventory[i] = ItemDatabase.itemList[craftableItemId];

                    slotStack[i] += 1;

                    for (int j = 0; j < slotsNumber; j++)
                    {
                        if (yourInventory[j].id == ItemDatabase.itemList[craftableItemId].n1 && a > 0)
                        {
                            if (slotStack[j] > a)
                            {
                                slotStack[j] -= a;
                                a = 0;
                            }
                            else
                            {
                                a -= slotStack[j];
                                slotStack[j] = 0;
                                yourInventory[j] = ItemDatabase.itemList[0];

                            }
                        }

                    }

                    for (int k = 0; k < slotsNumber; k++)
                    {
                        if (yourInventory[k].id == ItemDatabase.itemList[craftableItemId].n2 && b > 0)
                        {
                            if (slotStack[k] > b)
                            {
                                slotStack[k] -= b;
                                b = 0;
                            }
                            else
                            {
                                b -= slotStack[k];
                                slotStack[k] = 0;
                                yourInventory[k] = ItemDatabase.itemList[0];

                            }
                        }


                    }

                    for (int l = 0; l < slotsNumber; l++)
                    {
                        if (yourInventory[l].id == ItemDatabase.itemList[craftableItemId].n3 && c > 0)
                        {
                            if (slotStack[l] > c)
                            {
                                slotStack[l] -= c;
                                c = 0;
                            }
                            else
                            {
                                c -= slotStack[l];
                                slotStack[l] = 0;
                                yourInventory[l] = ItemDatabase.itemList[0];

                            }
                        }
                    }
                    craft = false;
                }
            }

        }

    }
    public void cookItem()
    {
        int a = 0;
        int b = 0;

        for (int i = 0; i < slotsNumber; i++)
        {
            if (yourInventory[i].id == ItemDatabase.itemList[cookableItemId].n1)
            {
                a += slotStack[i];
            }
        }

        for (int i = 0; i < slotsNumber; i++)
        {
            if (yourInventory[i].id == ItemDatabase.itemList[cookableItemId].n2)
            {
                b += slotStack[i];
            }
        }

        if (a >= ItemDatabase.itemList[cookableItemId].q1 && b >= ItemDatabase.itemList[cookableItemId].q2)
        {
            Debug.Log("cancook");
            cook = true;
            cookBtn.SetActive(true);
        }
        else
        {
            cook = false;
            cookBtn.SetActive(false);
        }

        if (cook == true)
        {
            a = ItemDatabase.itemList[cookableItemId].q1;
            b = ItemDatabase.itemList[cookableItemId].q2;

            for (int i = 0; i < slotsNumber; i++)
            {
                if (yourInventory[i].id == cookableItemId)
                {
                    if (slotStack[i] == maxStack)
                    {
                        continue;
                    }
                    else
                    {
                        slotStack[i] += 1;
                        i = slotsNumber;
                    }

                    for (int j = 0; j < slotsNumber; j++)
                    {
                        if (yourInventory[j].id == ItemDatabase.itemList[cookableItemId].n1 && a > 0)
                        {
                            if (slotStack[j] > a)
                            {
                                slotStack[j] -= a;
                                a = 0;
                            }
                            else
                            {
                                a -= slotStack[j];
                                slotStack[j] = 0;
                                yourInventory[j] = ItemDatabase.itemList[0];

                            }
                        }

                    }

                    for (int k = 0; k < slotsNumber; k++)
                    {
                        if (yourInventory[k].id == ItemDatabase.itemList[cookableItemId].n2 && b > 0)
                        {
                            if (slotStack[k] > b)
                            {
                                slotStack[k] -= b;
                                b = 0;
                            }
                            else
                            {
                                b -= slotStack[k];
                                slotStack[k] = 0;
                                yourInventory[k] = ItemDatabase.itemList[0];

                            }
                        }

                    }

                    cook = false;

                }
            }

            for (int i = 0; i < slotsNumber; i++)
            {
                if (yourInventory[i].id == 0 && cook == true)
                {
                    yourInventory[i] = ItemDatabase.itemList[cookableItemId];

                    slotStack[i] += 1;

                    for (int j = 0; j < slotsNumber; j++)
                    {
                        if (yourInventory[j].id == ItemDatabase.itemList[cookableItemId].n1 && a > 0)
                        {
                            if (slotStack[j] > a)
                            {
                                slotStack[j] -= a;
                                a = 0;
                            }
                            else
                            {
                                a -= slotStack[j];
                                slotStack[j] = 0;
                                yourInventory[j] = ItemDatabase.itemList[0];

                            }
                        }

                    }

                    for (int k = 0; k < slotsNumber; k++)
                    {
                        if (yourInventory[k].id == ItemDatabase.itemList[cookableItemId].n2 && b > 0)
                        {
                            if (slotStack[k] > b)
                            {
                                slotStack[k] -= b;
                                b = 0;
                            }
                            else
                            {
                                b -= slotStack[k];
                                slotStack[k] = 0;
                                yourInventory[k] = ItemDatabase.itemList[0];

                            }
                        }


                    }

                    cook = false;
                }
            }

        }

    }
    public void HealHead()
    {
        if (slotStack[b] ==1)
        {
            Player.Head += 5;
            yourInventory[b] = ItemDatabase.itemList[0];
            slotStack[b] = 0;
            uiControl.healSelect.SetActive(false);
            uiControl.Inventory.SetActive(true);
            
        }
        else
        {
            slotStack[b]--;
            Player.Head += 5;
        }
        player.UpdateSlider();
    }
    public void HealBody()
    {
        if (slotStack[b] ==1)
        {
            Debug.Log("consumefinish");
            Player.Body += 5;
            yourInventory[b] = ItemDatabase.itemList[0];
            slotStack[b] = 0;
            uiControl.healSelect.SetActive(false);
            uiControl.Inventory.SetActive(true);
           
        }
        else
        {
            Debug.Log("consume");
            slotStack[b]--;
            Player.Body += 5;
        }
        player.UpdateSlider();
    }
    public void HealArm()
    {
        if (slotStack[b] > 0)
        {
            slotStack[b]--;
            Player.Arm += 5;
        }
        else
        {
            Player.Arm += 5;
            yourInventory[b] = ItemDatabase.itemList[0];
            slotStack[b] = 0;
            uiControl.healSelect.SetActive(false);
            uiControl.Inventory.SetActive(true);
        }
        player.UpdateSlider();
    }
    public void HealLeg()
    {
        if (slotStack[b] >0)
        {
            slotStack[b]--;
            Player.Legs += 5;
        }
        else
        {
            Player.Legs += 5;
            yourInventory[b] = ItemDatabase.itemList[0];
            slotStack[b] = 0;
            uiControl.healSelect.SetActive(false);
            uiControl.Inventory.SetActive(true);
        }
        player.UpdateSlider();
    }

    public void CancelHeal()
    {
        uiControl.healSelect.SetActive(false);
        uiControl.Inventory.SetActive(true);
    }
}