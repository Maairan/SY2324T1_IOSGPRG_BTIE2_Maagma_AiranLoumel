using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Unit
{
    protected override void Start()
    {
        this.GetComponent<Health>().InitHealth(50);
    }
}
