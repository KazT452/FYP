using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityStandardAssets.Characters.FirstPerson;
using TMPro;

public class Player : MonoBehaviour
{
    public UIController uiControl;
    public DaysManager daysManager;
    public Inventory inventory;
    public DayNightSystem dnSystem;
    public Vector3 revivePoint;
    public static float Head, Body,Arm,Legs;
    private enum Weapon { Hand, Knife, Axe };
    public List<GameObject> QuickItem = new List<GameObject>();
    public static float Health, Hunger, Thrist;
    public float Stamina, StaminaDecrease;
    public Slider staminaSlider;
    public float maxHealth, maxHunger, maxStamina;
    public float HealthDecreaseRate,HungerDecreaseRate ;
    public Slider healthSlider, hungerSlider, headSlider, bodySlider, armSlider, legSlider; 
    public bool Dead;
    public TextMeshProUGUI hpPenaltyBox;
    public GameObject msgBox;
    public TextMeshProUGUI msgBoxText;
    public FirstPersonController FPS;
    public static bool walk, run, unarm, axe, p_Axe,attack;
    public static Animator anim;
    public bool enterCraft, enterCook, enterQuest, enterSave;
    public bool enterSafePoint;
    public GameObject Tutorial;
    public bool tired = false;
    public Animator statAnim;
    public GameObject statTxt;

    // Start is called before the first frame update
    void Start()
    {
        revivePoint = transform.position;
        inventory = transform.GetComponent<Inventory>();

        //Collision = false
        enterCraft = false;
        enterCook = false;
        enterQuest = false;
        enterSave = false;
        enterSafePoint = false;

        attack = false;
        unarm = true;
        anim = transform.GetComponentInChildren<Animator>();
        Hunger = 200f;
        Thrist = 150f;
        Head = 20;
        Body = 20;
        Arm = 30;
        Legs = 30;
        headSlider.maxValue = 20;
        headSlider.value = Head;
        bodySlider.maxValue = 20;
        bodySlider.value = Body;
        legSlider.maxValue = 30;
        legSlider.value = Legs;
        armSlider.maxValue = 30;
        armSlider.value = Arm;
        healthSlider.maxValue = maxHealth;
        healthSlider.value = maxHealth;
        hungerSlider.maxValue = maxHunger;
        hungerSlider.value = maxHunger;
        staminaSlider.maxValue = maxStamina;
        staminaSlider.value = maxStamina;
        HealthDecreaseRate = 1f;
        HungerDecreaseRate = 5f;
        UpdateSlider();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateSlider();
        HealthPenalty();
        if (Health <= 0||Head<=0)
        {
            Die();
        }
        //Animator Settings
        statAnim.SetBool("Tired", tired);
        anim.SetBool("Attack", attack);
        anim.SetBool("Unarm", unarm);
        if (FPS.m_Input!= Vector2.zero)
        {
            anim.SetBool("Walk", true);
        }
        else
        {
            anim.SetBool("Walk", false);
        }
        if (FPS.m_Input != Vector2.zero&&!FPS.m_IsWalking)
        {
            anim.SetBool("Run",true);

        }
        else
        {
            anim.SetBool("Run", false);
        }
        if (FPS.m_Jumping)
        {
            anim.SetBool("isGrounded", false);
        }
        else
        {
            anim.SetBool("isGrounded", true);
        }

        if (FPS.isActiveAndEnabled)
        {
            if (anim.GetCurrentAnimatorStateInfo(0).IsName("wp_Attk") || anim.GetCurrentAnimatorStateInfo(0).IsName("hd_Attk"))
            {
                if (anim.GetCurrentAnimatorStateInfo(0).IsName("wp_Attk"))
                {
                    if (anim.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.5)
                    {
                        attack = false;
                    }
                }
                else if (anim.GetCurrentAnimatorStateInfo(0).IsName("hd_Attk"))
                {
                    if (anim.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.5)
                    {
                        attack = false;
                    }
                }
            }
            if (Stamina > 1 && Arm > 0)
            {
                if (!attack)
                {
                    if (Input.GetButtonDown("Fire1") && !anim.GetBool("Unarm"))
                    {
                        anim.Play("wp_Attk");
                        Stamina -= 2f * StaminaDecrease;
                        attack = true;

                    }
                    else if (Input.GetButtonDown("Fire1") && anim.GetBool("Unarm"))
                    {
                        anim.Play("hd_Attk");
                        Stamina -= 1.5f * StaminaDecrease;
                        attack = true;
                    }

                }
            }
        }               
        
        //Health Controller
        if (hungerSlider.value <= 0)
        {
            Body -= (Time.deltaTime / HealthDecreaseRate * 2);
            bodySlider.value -= (int)(Time.deltaTime / HealthDecreaseRate * 2);
        }
        if (Head <= 0)
        {
            Head = 0;
        }
        if (Body <= 0)
        {
            Body = 0;
        }
        if (Arm <= 0)
        {
            Arm = 0;
        }
        if (Legs <= 0)
        {
            Legs = 0;
        }
        
        //Hunger Controller
        if (Hunger>= 0)
        {
            hungerSlider.value -= Time.deltaTime / HungerDecreaseRate;
            Hunger -= Time.deltaTime / HungerDecreaseRate;
        }
        else if (Hunger<= 0)
        {
            hungerSlider.value = 0;
            Hunger = 0;
        }
        else if (Hunger >= maxHunger)
        {
            hungerSlider.value = maxHunger;
            Hunger= maxHunger;
        }

        //Stamina Controller
        if (uiControl.inventoryClosed&&uiControl.craftingisClosed &&uiControl.cookingisClosed)
        {
            if (!FPS.m_IsWalking && FPS.m_Input != Vector2.zero)
            {
                if (Stamina >= 0)
                {
                    staminaSlider.value -= (Time.deltaTime / StaminaDecrease);
                    Stamina -= (Time.deltaTime / StaminaDecrease);
                }
                else if (Stamina <= 0)
                {
                    staminaSlider.value = 0;
                    Stamina = 0;
                    if (Legs >= 0)
                    {
                        legSlider.value -= Time.deltaTime / HealthDecreaseRate;
                        Legs -= 1;
                    }
                    else
                    {
                        Legs = 0;
                    }

                }
                else if (Stamina >= maxStamina)
                {
                    staminaSlider.value = maxStamina;
                    Stamina = maxStamina;
                }
            }
        }        
        else if (FPS.m_IsWalking && Stamina<=maxStamina||!FPS.isActiveAndEnabled&& Stamina <= maxStamina)
        {
            Debug.Log(FPS.m_IsWalking);
            staminaSlider.value += (Time.deltaTime / StaminaDecrease);
            Stamina += (Time.deltaTime / StaminaDecrease);
        }

        //UI
        if (enterCook)
        {
            msgBox.SetActive(true);
            msgBoxText.text = "Press E to Cook";
            if (Input.GetKeyDown(KeyCode.E))
            {
                if (uiControl.cookingisClosed)
                {
                    uiControl.Cooking.SetActive(true);
                    uiControl.cookingisClosed = false;
                }
            }
        }        
        else if (enterCraft)
        {
            msgBox.SetActive(true);
            msgBoxText.text = "Press E to Craft";
            if (Input.GetKeyDown(KeyCode.E))
            {
                if (uiControl.craftingisClosed)
                {
                    uiControl.Crafting.SetActive(true);
                    uiControl.craftingisClosed = false;
                }
            }

        }
        else if (enterQuest)
        {
            msgBox.SetActive(true);
            msgBoxText.text = "Press E to access Quest";
            if (Input.GetKeyDown(KeyCode.E))
            {
                if (uiControl.questisClosed)
                {
                    uiControl.Quest.SetActive(true);
                    uiControl.questisClosed = false;
                }
            }
        }
        else if (enterSave)
        {
            revivePoint = transform.position;
            if (dnSystem.currentTimeofDay <= 0.95f && dnSystem.currentTimeofDay >= 0.75f)
            {
                msgBox.SetActive(true);
                msgBoxText.text = "Press E to Sleep";
                if (Input.GetKeyDown(KeyCode.E))
                {
                    daysManager.Sleep(true);
                }
            }
            else
            {
                msgBox.SetActive(true);
                msgBoxText.text = "Can't Sleep, Not Night Yet";
            }
        }
        else
        {
            msgBox.SetActive(false);
            msgBoxText.text = " ";
        }
    }

    public void UpdateSlider()
    {
        Health = Head + Body + Arm + Legs;
        headSlider.value = Head;
        bodySlider.value = Body;
        armSlider.value = Arm;
        legSlider.value = Legs;
        healthSlider.value = Health;
        hungerSlider.value = Hunger;
        staminaSlider.value = Stamina;
    }

    private void HealthPenalty()
    {
        if (Head <= 10 && Head >= 1)
        {

        }
        if (Body <= 5||tired)
        {
            HealthDecreaseRate = 2f;
            HungerDecreaseRate = 10f;
        }
        else if (tired && Body <= 5)
        {
            HealthDecreaseRate = 2.5f;
            HungerDecreaseRate = 15f;
        }
        else
        {
            HealthDecreaseRate = 1f;
            HungerDecreaseRate = 4f;
        }
        if (Body <= 0)
        {
            Head -= (Time.deltaTime / HealthDecreaseRate);
            Arm -= (Time.deltaTime / HealthDecreaseRate);
            Legs -= (Time.deltaTime / HealthDecreaseRate);
        }
        else if (Body <= 0 && (hungerSlider.value <= 0 ))
        {
            Head -= (Time.deltaTime / HealthDecreaseRate * 2);
            Arm -= (Time.deltaTime / HealthDecreaseRate * 2);
            Legs -= (Time.deltaTime / HealthDecreaseRate * 2);
        }
        if ((Legs <= 10&&Legs>=1)||Hunger>=maxHunger||tired)
        {
            FPS.m_RunSpeed = 8;
            FPS.m_WalkSpeed =4;
            hpPenaltyBox.text = " ";
        }
        else if (Legs <= 0)
        {
            FPS.m_RunSpeed = 0;
            FPS.m_WalkSpeed = 0;
            hpPenaltyBox.text = "Broken Legs, can not move!!";
        }
        else
        {
            FPS.m_RunSpeed = 20;
            FPS.m_WalkSpeed = 10;
            hpPenaltyBox.text = " ";
        }
        if (Hunger > maxHunger)
        {
            hpPenaltyBox.text = "Overeat,reduced movement speed!!";
        }
        if (tired)
        {
            statAnim.enabled = true;
            StaminaDecrease = 2;
            if (Hunger > maxHunger)
            {
                tired = false;
                statTxt.GetComponent<TextMeshProUGUI>().faceColor=new Color32(255,0,0,0);
            }
        }
        else
        {
            statAnim.enabled = false;
            StaminaDecrease = 1;
        }
    }
   
    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag == "CookingStation")
        {            
            enterCook= true;
        }
        else if (collision.gameObject.tag == "CraftingStation")
        {         
            enterCraft = true;
        }
        else if (collision.gameObject.tag == "QuestStation")
        {          
            enterQuest = true;
        }
        if (collision.gameObject.tag == "SavePoint")
        {
            enterSave = true;
        }
        if (collision.gameObject.tag == "SafeArea")
        {
            enterSafePoint = true;
            if (!QuestDatabase.questList[1].complete)
            {
                QuestDatabase.questList[1].complete = true;
            }
        }
        if (collision.tag == "Tutorial")
        {
            Tutorial.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "CookingStation")
        {
            Debug.Log("exitcook");
            enterCook = false;
        }
        else if (other.gameObject.tag == "CraftingStation")
        {
      
            enterCraft = false;
        }
        else if (other.gameObject.tag == "QuestStation")
        {
            enterQuest = false;
        }
        if (other.gameObject.tag == "SavePoint")
        {
            enterSave = false;
        }
        if (other.gameObject.tag == "SafeArea")
        {
            enterSafePoint = false;
        }
        if (other.tag == "Tutorial")
        {
            Tutorial.SetActive(false);
        }
    }

    public void Revive()
    {
        daysManager.diedText.SetActive(false);
        daysManager.reviveBtn.SetActive(false);
        StartCoroutine(daysManager.WakeUp(false));
        Head = 20;
        Body = 20;
        Arm = 30;
        Legs = 30;
        Hunger = maxHunger;
        Stamina = maxStamina;
    }


    private void Die()
    {
        Dead = true;
        if (Dead)
        {
            FPS.GetComponent<UnityStandardAssets.Characters.FirstPerson.FirstPersonController>().enabled = false;
            transform.position = revivePoint;
            for (int i = 0; i < inventory.slotsNumber; i++)
            {
                inventory.yourInventory[i] = ItemDatabase.itemList[0];
                inventory.slotStack[i] = 0;
            }
            daysManager.eyelidScreen.SetActive(true);
            daysManager.diedText.SetActive(true);
            daysManager.reviveBtn.SetActive(true);
        }       
    }
}
