using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityStandardAssets.Characters.FirstPerson;
using TMPro;

public class Player : MonoBehaviour
{    
    public static float Head, Body,Arm,Legs;
    private enum Weapon { Hand, Knife, Axe };
    public List<GameObject> QuickItem = new List<GameObject>();
    public static float Health, Hunger, Thrist;
    public float Stamina, StaminaDecrease;
    public Slider staminaSlider;
    public float maxHealth, maxThrist, maxHunger, maxStamina;
    public float HealthDecreaseRate,HungerDecreaseRate, ThristDecreaseRate ;
    public Slider healthSlider, thristSlider, hungerSlider, headSlider, bodySlider, armSlider, legSlider; 
    public bool Dead;
    public TextMeshProUGUI hpPenaltyBox;
    public FirstPersonController FPS;
    public bool walk, run, unarm, axe, p_Axe,attack;
    public static Animator anim;

    // Start is called before the first frame update
    void Start()
    {
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
        thristSlider.maxValue = maxThrist;
        thristSlider.value = maxThrist;
        hungerSlider.maxValue = maxHunger;
        hungerSlider.value = maxHunger;
        staminaSlider.maxValue = maxStamina;
        staminaSlider.value = maxStamina;
        HealthDecreaseRate = 1f;
        HungerDecreaseRate = 5f;
        UpdateHealth();
    }

    // Update is called once per frame
    void Update()
    {
        //Animator Settings
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
        attack = Input.GetButtonDown("Fire1");
        anim.SetBool("Attack", attack);
        UpdateHealth();
        HealthPenalty();
        //Health Controller
        if (hungerSlider.value <= 0 && thristSlider.value <= 0)
        {
            Body -= (Time.deltaTime / HealthDecreaseRate * 2);
            bodySlider.value -= (int)(Time.deltaTime / HealthDecreaseRate * 2);
        }
        else if (hungerSlider.value <= 0 || thristSlider.value <= 0)
        {
            Body -= (Time.deltaTime / HealthDecreaseRate);
            bodySlider.value -=(Time.deltaTime / HealthDecreaseRate);
        }
        
        if (healthSlider.value <= 0||Head<=0)
        {
            Die();
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

        //Thrist Controller
        if (Thrist >= 0)
        {
            thristSlider.value -= Time.deltaTime / ThristDecreaseRate;
            Thrist-= Time.deltaTime / ThristDecreaseRate;
        }
        else if (Thrist <= 0)
        {
            thristSlider.value = 0;
            Thrist= 0;
        }
        else if (Thrist>= maxThrist)
        {
            thristSlider.value = maxThrist;
            Thrist= maxThrist;
        }

        //Stamina Controller
        if (!FPS.m_IsWalking&& FPS.m_Input!=Vector2.zero)
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
        else if (FPS.m_IsWalking && Stamina<=maxStamina)
        {
            staminaSlider.value += (Time.deltaTime / StaminaDecrease);
            Stamina += (Time.deltaTime / StaminaDecrease);
        }
    }

    public void UpdateHealth()
    {
        Health = Head + Body + Arm + Legs;
        headSlider.value = Head;
        bodySlider.value = Body;
        armSlider.value = Arm;
        legSlider.value = Legs;
        healthSlider.value = Health;
        hungerSlider.value = Hunger;
        thristSlider.value = Thrist;
    }

    public void UpdateHunger()
    {

    }

    private void Attack()
    {

    }

    private void HealthPenalty()
    {
        if (Head <= 10 && Head >= 1)
        {

        }
        if (Body <= 5)
        {
            HealthDecreaseRate = 2f;
            HungerDecreaseRate = 10f;
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
        else if (Body <= 0 && (hungerSlider.value <= 0 || thristSlider.value <= 0))
        {
            Head -= (Time.deltaTime / HealthDecreaseRate * 2);
            Arm -= (Time.deltaTime / HealthDecreaseRate * 2);
            Legs -= (Time.deltaTime / HealthDecreaseRate * 2);
        }
        else if (Body <= 0 && hungerSlider.value <= 0 && thristSlider.value <= 0)
        {
            Head -= (Time.deltaTime / HealthDecreaseRate * 3);
            Arm -= (Time.deltaTime / HealthDecreaseRate * 3);
            Legs -= (Time.deltaTime / HealthDecreaseRate * 3);
        }
        if ((Legs <= 10&&Legs>=1)||Hunger>=maxHunger)
        {
            FPS.m_RunSpeed = 8;
            FPS.m_WalkSpeed =4;
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
    }




    private void Die()
    {
        Dead = true;
    }
}
