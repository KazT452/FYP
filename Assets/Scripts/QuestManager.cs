using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class QuestManager : MonoBehaviour
{
    public List<Quest> Qid = new List<Quest>();
    public Inventory inventory;

    public GameObject availableQuest;
    public GameObject activeQuest;

    public List<GameObject> availableQuests = new List<GameObject>();
    public GameObject[] activeQuests;

    public int totalQuest;

    public Image[] ItemNeededInQuest;
    public Sprite[] ItemNeededInQuestSprite;

    public Image[] rewardedItem;
    public Sprite[] rewardedItemSprite;

    public int activeQid;

    public TextMeshProUGUI questName;
    public TextMeshProUGUI questDesc;
    public TextMeshProUGUI[] itemText;
    public TextMeshProUGUI[] rewarditemText;

    int a = 0;
    int b = 0;
    int c = 0;

    public bool complete;
    public GameObject completeBtn;
    // Start is called before the first frame update
    void Start()
    {
        activeQid = 1;
    }

    // Update is called once per frame
    void Update()
    {
        QuestChecker();
        questName.text = QuestDatabase.questList[activeQid].name;
        questDesc.text = QuestDatabase.questList[activeQid].description;

        ItemNeededInQuestSprite[0] = Database.itemList[QuestDatabase.questList[activeQid].n1].itemSprite;
        ItemNeededInQuestSprite[1] = Database.itemList[QuestDatabase.questList[activeQid].n2].itemSprite;
        ItemNeededInQuestSprite[2] = Database.itemList[QuestDatabase.questList[activeQid].n3].itemSprite;

        ItemNeededInQuest[0].sprite = ItemNeededInQuestSprite[0];
        ItemNeededInQuest[1].sprite = ItemNeededInQuestSprite[1];
        ItemNeededInQuest[2].sprite = ItemNeededInQuestSprite[2];

        itemText[0].text = "" + QuestDatabase.questList[activeQid].q1;
        itemText[1].text = "" + QuestDatabase.questList[activeQid].q2;
        itemText[2].text = "" + QuestDatabase.questList[activeQid].q3;

        rewardedItemSprite[0] = Database.itemList[QuestDatabase.questList[activeQid].r1].itemSprite;

        rewardedItem[0].sprite = rewardedItemSprite[0];

        rewarditemText[0].text = "" + QuestDatabase.questList[activeQid].qr1;
    }
    public void QuestChecker()
    {
        Debug.Log(a +","+ b + c);
        for (int i = 0; i < inventory.slotsNumber; i++)
        {
            if (inventory.yourInventory[i].id == Database.itemList[activeQid].n1)
            {
                a += inventory.slotStack[i];
            }
        }

        for (int i = 0; i < inventory.slotsNumber; i++)
        {
            if (inventory.yourInventory[i].id == Database.itemList[activeQid].n2)
            {
                b += inventory.slotStack[i];
            }
        }

        for (int i = 0; i < inventory.slotsNumber; i++)
        {
            if (inventory.yourInventory[i].id == Database.itemList[activeQid].n3)
            {
                c += inventory.slotStack[i];
            }
        }







        if (a >= Database.itemList[activeQid].q1 && b >= Database.itemList[activeQid].q2 && c >= Database.itemList[activeQid].q3)
        {
            Debug.Log("questcom");
            complete = true;
            completeBtn.SetActive(true);
        }
        else
        {
            Debug.Log("questXcom");
            complete = false;
            completeBtn.SetActive(false);
        }       

    }
    public void AcceptQuest()
    {

    }

    public void CompleteQuest()
    {
        if (complete == true)
        {
            a = Database.itemList[activeQid].q1;
            b = Database.itemList[activeQid].q2;
            c = Database.itemList[activeQid].q3;

            for (int i = 0; i < inventory.slotsNumber; i++)
            {
                if (inventory.yourInventory[i].id == activeQid)
                {
                    if (inventory.slotStack[i] == inventory.maxStack)
                    {
                        continue;
                    }
                    else
                    {
                        inventory.slotStack[i] += 1;
                        i = inventory.slotsNumber;
                    }

                    for (int j = 0; j < inventory.slotsNumber; j++)
                    {
                        if (inventory.yourInventory[j].id == Database.itemList[activeQid].n1 && a > 0)
                        {
                            if (inventory.slotStack[j] > a)
                            {
                                inventory.slotStack[j] -= a;
                                a = 0;
                            }
                            else
                            {
                                a -= inventory.slotStack[j];
                                inventory.slotStack[j] = 0;
                                inventory.yourInventory[j] = Database.itemList[0];

                            }
                        }

                    }

                    for (int k = 0; k < inventory.slotsNumber; k++)
                    {
                        if (inventory.yourInventory[k].id == Database.itemList[activeQid].n2 && b > 0)
                        {
                            if (inventory.slotStack[k] > b)
                            {
                                inventory.slotStack[k] -= b;
                                b = 0;
                            }
                            else
                            {
                                b -= inventory.slotStack[k];
                                inventory.slotStack[k] = 0;
                                inventory.yourInventory[k] = Database.itemList[0];

                            }
                        }

                    }

                    for (int l = 0; l < inventory.slotsNumber; l++)
                    {
                        if (inventory.yourInventory[l].id == Database.itemList[activeQid].n3 && c > 0)
                        {
                            if (inventory.slotStack[l] > c)
                            {
                                inventory.slotStack[l] -= c;
                                c = 0;
                            }
                            else
                            {
                                c -= inventory.slotStack[l];
                                inventory.slotStack[l] = 0;
                                inventory.yourInventory[l] = Database.itemList[0];

                            }
                        }


                    }

                    complete = false;

                }
            }

            for (int i = 0; i < inventory.slotsNumber; i++)
            {
                if (inventory.yourInventory[i].id == 0 && complete == true)
                {
                    inventory.yourInventory[i] = Database.itemList[activeQid];

                    inventory.slotStack[i] += 1;

                    for (int j = 0; j < inventory.slotsNumber; j++)
                    {
                        if (inventory.yourInventory[j].id == Database.itemList[activeQid].n1 && a > 0)
                        {
                            if (inventory.slotStack[j] > a)
                            {
                                inventory.slotStack[j] -= a;
                                a = 0;
                            }
                            else
                            {
                                a -= inventory.slotStack[j];
                                inventory.slotStack[j] = 0;
                                inventory.yourInventory[j] = Database.itemList[0];

                            }
                        }

                    }

                    for (int k = 0; k < inventory.slotsNumber; k++)
                    {
                        if (inventory.yourInventory[k].id == Database.itemList[activeQid].n2 && b > 0)
                        {
                            if (inventory.slotStack[k] > b)
                            {
                                inventory.slotStack[k] -= b;
                                b = 0;
                            }
                            else
                            {
                                b -= inventory.slotStack[k];
                                inventory.slotStack[k] = 0;
                                inventory.yourInventory[k] = Database.itemList[0];

                            }
                        }


                    }

                    for (int l = 0; l < inventory.slotsNumber; l++)
                    {
                        if (inventory.yourInventory[l].id == Database.itemList[activeQid].n3 && c > 0)
                        {
                            if (inventory.slotStack[l] > c)
                            {
                                inventory.slotStack[l] -= c;
                                c = 0;
                            }
                            else
                            {
                                c -= inventory.slotStack[l];
                                inventory.slotStack[l] = 0;
                                inventory.yourInventory[l] = Database.itemList[0];

                            }
                        }


                    }

                    complete = false;
                }
            }

        }
    }
}
