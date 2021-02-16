using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DaysManager : MonoBehaviour
{
    public Player player;
    public GameObject savePoint;
    public DayNightSystem dayNightSystem;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Sleep(bool alive)
    {
        if (alive)
        {
            Player.Head += 3;
            Player.Body += 3;
            Player.Arm += 3;
            Player.Legs += 3;
        }

        else
        {
            int deadDamage = Random.Range(0, 5);
            Player.Head -= deadDamage;
            Player.Body -= deadDamage;
            Player.Arm -= deadDamage;
            Player.Legs -= deadDamage;
        }
        
        
    }


}
