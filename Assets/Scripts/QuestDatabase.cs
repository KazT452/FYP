using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestDatabase: MonoBehaviour
{
    public static List<Quest> questList = new List<Quest>();

    private void Awake()
    {
        questList.Add(new Quest(0, "None", "None",false ,false, false, 0, 0, 0, 0, 0, 0,0,0));
        questList.Add(new Quest(1, "Gather Wood", "Gather some wood",false, false, true, 1, 0, 0, 5, 0, 0, 4, 5));
    }
}
