using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDatabase : MonoBehaviour
{
    public static List<Item> itemList = new List<Item>();

    private void Awake()
    {
        itemList.Add(new Item(0, "None", "None", Resources.Load<Sprite>("Square"), false,false,false,false,0,0,0,0,0,0));
        itemList.Add(new Item(1, "Stick", "It is item", Resources.Load<Sprite>("Stick"), false,false, false,false, 0, 0, 0, 0, 0, 0));
        itemList.Add(new Item(2, "Stone", "It is item", Resources.Load<Sprite>("Stone"), false,false, false,false, 0, 0, 0, 0, 0, 0));
        itemList.Add(new Item(3, "Herb", "It is healing item", Resources.Load<Sprite>("Herb"), true,true, false,false, 0, 0, 0, 0, 0, 0));
        itemList.Add(new Item(4, "Meat", "It is Raw Food", Resources.Load<Sprite>("Raw Meat"), true,false, false,true, 0, 0, 0, 0, 0, 0));
        //Cookables
        itemList.Add(new Item(5, "Cooked Meat", "It is Food", Resources.Load<Sprite>("Cooked Meat"),true,false,false,false,1,4,0,2,1,0));
        //Craftables
        itemList.Add(new Item(6, "Axe", "Can Cut Tree", Resources.Load<Sprite>("Axe"), false,false, true,false, 1, 2, 0, 2, 2, 0));
        itemList.Add(new Item(7, "Pick_Axe", "Can Cut Tree", Resources.Load<Sprite>("Pickaxe"), false,false, true,false, 1, 2, 0, 2, 2, 0));
    }
}
