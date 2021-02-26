using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestDatabase: MonoBehaviour
{
    public static List<Quest> questList = new List<Quest>();

    private void Awake()
    {
        questList.Add(new Quest(0, "None", "None",false ,false, false, 0, 0, 0, 0, 0, 0,0,0));
        questList.Add(new Quest(1, "Find Shelter", "Return to the cave that you are living", false, false, false, 0, 0, 0, 0, 0, 0, 0, 0));
        questList.Add(new Quest(2, "Prove yourself I", "Prove your worth to join the survivors", false, false, false, 1, 0, 0, 30, 0, 0, 0, 0));
        questList.Add(new Quest(2, "Prove yourself II", "Prove your worth to join the survivors", false, false, false, 2, 0, 0, 30, 0, 0, 0, 0));
        questList.Add(new Quest(3, "Gather Wood", "Gather some woods",false, false, true, 1, 0, 0, 5, 0, 0, 4, 5));
        questList.Add(new Quest(4, "Gather Stone", "Gather some stones", false, false, true, 2, 0, 0, 5, 0, 0, 3, 5));
    }
}
