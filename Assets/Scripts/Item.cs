using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
[System.Serializable]

public class Item 
{
    public int id;
    public string name;
    public string description;
    public Sprite itemSprite;

    public bool consumable;
    public bool healing;
    public bool canbeCrafted;
    public bool canbeCooked;

    //item needed
    public int n1;
    public int n2;
    public int n3;

    //item quantity needed
    public int q1;
    public int q2;
    public int q3;

    public Item()
    {

    }

    public Item(int Id, string Name, string Description, Sprite ItemSprite, bool Consumable, bool Healing, bool CanbeCrafted, bool CanbeCooked, int N1,int N2,int N3, int Q1, int Q2, int Q3)
    {
        id = Id;
        name = Name;
        description = Description;
        itemSprite = ItemSprite;
        consumable = Consumable;
        healing = Healing;
        canbeCrafted = CanbeCrafted;
        canbeCooked = CanbeCooked;


        n1 = N1;
        n2 = N2;
        n3 = N3;

        q1 = Q1;
        q2 = Q2;
        q3 = Q3;
    }
}
