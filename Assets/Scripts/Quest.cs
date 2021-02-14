using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
[System.Serializable]

public class Quest
{
    public int id;
    public string name;
    public string description;

    public bool active;
    public bool complete;
    public bool repeatable;

    //item needed
    public int n1;
    public int n2;
    public int n3;

    //item quantity needed
    public int q1;
    public int q2;
    public int q3;

    //item reward and quantity
    public int r1;
    public int qr1;

    public Quest()
    {

    }

    public Quest(int Id, string Name, string Description, bool Active, bool Complete, bool Repeatable, int N1, int N2, int N3, int Q1, int Q2, int Q3, int R1,int QR1)
    {
        id = Id;
        name = Name;
        description = Description;
        active = Active;
        complete = Complete;
        repeatable = Repeatable;

        n1 = N1;
        n2 = N2;
        n3 = N3;

        q1 = Q1;
        q2 = Q2;
        q3 = Q3;

        r1 = R1;
        qr1 = QR1;
    }
}
