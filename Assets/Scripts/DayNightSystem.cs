using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DayNightSystem : MonoBehaviour
{
    public DaysManager daysManager;
    public Player player;
    [SerializeField] private Light sun;
    [SerializeField] private float secondsInFullDay;

    [Range(0, 1)] public float currentTimeofDay = 0;
    private float timeMultiplier = 1f;
    private float sunInitialIntensity;
    //Near night reminder
    public TextMeshProUGUI NightTeller;

    // Start is called before the first frame update
    void Start()
    {
        daysManager = GameObject.FindGameObjectWithTag("GameController").GetComponent<DaysManager>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        sunInitialIntensity = sun.intensity;
        currentTimeofDay = 0.25f;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.L)&&Input.GetKeyDown(KeyCode.RightShift))
        {
            currentTimeofDay = 0.74f;
        }
        if (Input.GetKeyDown(KeyCode.M)&&Input.GetKeyDown(KeyCode.RightShift))
        {
            QuestDatabase.questList[1].complete = true;
            Debug.Log("COMPETE");
        }

        UpdateSun();

        currentTimeofDay += (Time.deltaTime / secondsInFullDay) * timeMultiplier;

        if (currentTimeofDay >= 1)
        {
            currentTimeofDay = 0;
        }

        if (currentTimeofDay >= 0.72f && currentTimeofDay <= 0.73f)
        {
            NightTeller.text = "The night is near!\r\nReturn to Cave!!!";
        }
        else if (currentTimeofDay >= 0.74f && currentTimeofDay <= 0.75f&&!player.enterSafePoint)
        {
            NightTeller.text = "THE NIGHT IS HERE!!\r\nReturn NOW!!!";
        }
        else if (currentTimeofDay >= 0.74f && currentTimeofDay <= 0.75f&&player.enterSafePoint)
        {
            NightTeller.text = "THE NIGHT IS HERE!!\r\nStay in Shelter!!!";
        }
        else if (currentTimeofDay >= 0.9f)
        {
            NightTeller.text = "THE NIGHT IS ENDING!!\r\nReturn NOW!!!";
        }
        else
        {
            NightTeller.text = " ";
        }

        if (currentTimeofDay >= 0.8f)
        {
            if (!player.enterSafePoint)
            {
                daysManager.Sleep(false);
            }
        }

        if (currentTimeofDay >= 0.95f)
        {
            if (player.enterSafePoint)
            {
                daysManager.Sleep(true);
            }
            else
            {
                daysManager.Sleep(false);
            }
        }
    }

    void UpdateSun()
    {
        sun.transform.localRotation = Quaternion.Euler((currentTimeofDay * 360f) - 90, 170, 0);

        float intensityMultiplier = 1;

        if(currentTimeofDay <=0.23f || currentTimeofDay >= 0.75f)
        {
            intensityMultiplier = 0;
        }
        else if (currentTimeofDay <= 0.25f)
        {
            intensityMultiplier = Mathf.Clamp01((currentTimeofDay - 0.23f) * (1 / 0.02f));
        }
        else if (currentTimeofDay >= 0.73f)
        {
            intensityMultiplier = Mathf.Clamp01(1-(currentTimeofDay - 0.73f) * (1 / 0.02f));
        }

        sun.intensity = sunInitialIntensity * intensityMultiplier;
    }
}
