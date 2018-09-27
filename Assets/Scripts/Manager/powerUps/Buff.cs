using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Buff : PowerUp{

    private int duration;

    public Buff(string name, int duration)
    {
        this.duration = duration;
        this.color = "yellow";
        this.name = name;
        this.grouping = "powerUp";
    }
}
