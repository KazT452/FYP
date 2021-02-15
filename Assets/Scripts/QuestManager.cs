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

    public List<Button> availableQuests = new List<Button>();
    public List<Button> activeQuests = new List<Button>();

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

    int totalA = 0;
    int totalB = 0;
    int totalC = 0;

    public bool complete;
    public GameObject completeBtn;
    // Start is called before the first frame update
    void Start()
    {
        activeQid = 0;
    }

    // Update is called once per frame
    void Update()
    {
        QuestChecker();
        for (int i = 0; i < availableQuests.Count; i++)
        {
            availableQuests[i].onClick.AddListener(()=>ButtonOnClick(i));
        }
        for (int i = 0; i < activeQuests.Count; i++)
        {
            activeQuests[i].onClick.AddListener(()=>ButtonOnClick(i));
        }
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
        int a =0;
        int b =0;
        int c=0;
        if (QuestDatabase.questList[activeQid].active)
        {
            for (int i = 0; i < inventory.slotsNumber; i++)
            {
                if (inventory.yourInventory[i].id == QuestDatabase.questList[activeQid].n1)
                {
                    a += inventory.slotStack[i];
                    break;
                }
            }

            for (int i = 0; i < inventory.slotsNumber; i++)
            {
                if (inventory.yourInventory[i].id == QuestDatabase.questList[activeQid].n2)
                {
                    b += inventory.slotStack[i];
                    break;
                }
            }

            for (int i = 0; i < inventory.slotsNumber; i++)
            {
                if (inventory.yourInventory[i].id == QuestDatabase.questList[activeQid].n3)
                {
                    c += inventory.slotStack[i];
                    break;
                }
            }







            if (a >= QuestDatabase.questList[activeQid].q1 && b >= QuestDatabase.questList[activeQid].q2 && c >= QuestDatabase.questList[activeQid].q3)
            {
                Debug.Log(a);
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
    }
    public void ButtonOnClick(int btnnumber)
    {
        activeQid = btnnumber;
    }
    public void AcceptQuest()
    {
        QuestDatabase.questList[activeQid].active = true;
        QuestDatabase.questList[activeQid].complete = false;
        activeQid = 0;
    }

    public void CompleteQuest()
    {
        if (complete == true)
        {
            totalA = QuestDatabase.questList[activeQid].q1;
            totalB = QuestDatabase.questList[activeQid].q2;
            totalC = QuestDatabase.questList[activeQid].q3;
            int recountA = 0;
            for (int i = 0; i < inventory.slotsNumber; i++)
            {
                if (inventory.yourInventory[i].id == QuestDatabase.questList[activeQid].r1)
                {
                    if (inventory.slotStack[i] == inventory.maxStack)
                    {
                        continue;
                    }
                    else
                    {
                        inventory.slotStack[i] += QuestDatabase.questList[activeQid].qr1;
                        if (inventory.slotStack[i] > inventory.maxStack)
                        {
                            recountA= inventory.slotStack[i] - inventory.maxStack;
                            inventory.slotStack[i] -= recountA;
                            i++;
                            inventory.yourInventory[i] = Database.itemList[QuestDatabase.questList[activeQid].r1];
                            inventory.slotStack[i] += recountA;
                        }
                        i = inventory.slotsNumber;
                    }

                    for (int j = 0; j < inventory.slotsNumber; j++)
                    {
                        if (inventory.yourInventory[j].id == QuestDatabase.questList[activeQid].n1 && totalA > 0)
                        {
                            if (inventory.slotStack[j] > totalA)
                            {
                                inventory.slotStack[j] -= totalA;
                                totalA = 0;
                            }
                            else
                            {
                                totalA -= inventory.slotStack[j];
                                inventory.slotStack[j] = 0;
                                inventory.yourInventory[j] = Database.itemList[0];

                            }
                        }

                    }

                    for (int k = 0; k < inventory.slotsNumber; k++)
                    {
                        if (inventory.yourInventory[k].id == QuestDatabase.questList[activeQid].n2 && totalB > 0)
                        {
                            if (inventory.slotStack[k] > totalB)
                            {
                                inventory.slotStack[k] -= totalB;
                                totalB = 0;
                            }
                            else
                            {
                                totalB -= inventory.slotStack[k];
                                inventory.slotStack[k] = 0;
                                inventory.yourInventory[k] = Database.itemList[0];

                            }
                        }

                    }

                    for (int l = 0; l < inventory.slotsNumber; l++)
                    {
                        if (inventory.yourInventory[l].id == QuestDatabase.questList[activeQid].n3 && totalC > 0)
                        {
                            if (inventory.slotStack[l] > totalC)
                            {
                                inventory.slotStack[l] -= totalC;
                                totalC = 0;
                            }
                            else
                            {
                                totalC -= inventory.slotStack[l];
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
                    inventory.yourInventory[i] = Database.itemList[QuestDatabase.questList[activeQid].r1];

                    inventory.slotStack[i] += QuestDatabase.questList[activeQid].qr1;

                    for (int j = 0; j < inventory.slotsNumber; j++)
                    {
                        if (inventory.yourInventory[j].id == QuestDatabase.questList[activeQid].n1 && totalA > 0)
                        {
                            if (inventory.slotStack[j] > totalA)
                            {
                                inventory.slotStack[j] -= totalA;
                                totalA = 0;
                            }
                            else
                            {
                                totalA -= inventory.slotStack[j];
                                inventory.slotStack[j] = 0;
                                inventory.yourInventory[j] = Database.itemList[0];

                            }
                        }

                    }

                    for (int k = 0; k < inventory.slotsNumber; k++)
                    {
                        if (inventory.yourInventory[k].id == QuestDatabase.questList[activeQid].n2 && totalB > 0)
                        {
                            if (inventory.slotStack[k] > totalB)
                            {
                                inventory.slotStack[k] -= totalB;
                                totalB = 0;
                            }
                            else
                            {
                                totalB -= inventory.slotStack[k];
                                inventory.slotStack[k] = 0;
                                inventory.yourInventory[k] = Database.itemList[0];

                            }
                        }


                    }

                    for (int l = 0; l < inventory.slotsNumber; l++)
                    {
                        if (inventory.yourInventory[l].id == QuestDatabase.questList[activeQid].n3 && totalC > 0)
                        {
                            if (inventory.slotStack[l] > totalC)
                            {
                                inventory.slotStack[l] -= totalC;
                                totalC = 0;
                            }
                            else
                            {
                                totalC -= inventory.slotStack[l];
                                inventory.slotStack[l] = 0;
                                inventory.yourInventory[l] = Database.itemList[0];

                            }
                        }


                    }
                    


                    complete = false;
                }
                
            }
            QuestDatabase.questList[activeQid].complete = true;
            activeQuests[activeQid - 1].gameObject.SetActive(false);
            if (QuestDatabase.questList[activeQid].repeatable)
            {
                availableQuests[activeQid-1].gameObject.SetActive(true);
            }

        }
    }
}
