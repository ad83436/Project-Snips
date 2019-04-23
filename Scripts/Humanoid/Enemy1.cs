using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy1 : NPC
{

    // Start is called before the first frame update
    void Start()
    {
        NPC.testList.Add(this);
        OnStartUp();
    }

    // Update is called once per frame
    void Update()
    {
        GetDirectionOfSoul(ninja,testList);
        GetDistance(Direction);
        GetAngle(Direction);
        Sees();
    }

    public override void Movement()
    {
       
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Ninja")
        {
            ninja.Health -= Damage;
        }
    }
}
