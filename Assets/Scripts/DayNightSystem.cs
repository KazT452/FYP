using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DayNightSystem : MonoBehaviour
{
    [SerializeField] private Light sun;
    [SerializeField] private float secondsInFullDay = 120f;

    [Range(0, 1)] [SerializeField] private float currentTimeofDay = 0;
    private float timeMultiplier = 1f;
    private float sunInitialIntensity;
    //Near night reminder
    public TextMeshProUGUI NightTeller;

    // Start is called before the first frame update
    void Start()
    {
        sunInitialIntensity = sun.intensity;
        currentTimeofDay = 0.25f;
    }

    // Update is called once per frame
    void Update()
    {
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
        else if (currentTimeofDay >= 0.74f && currentTimeofDay <= 0.75f)
        {
            NightTeller.text = "THE NIGHT IS HERE!!\r\nReturn NOW!!!";
        }
        else
        {
            NightTeller.text = " ";
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
