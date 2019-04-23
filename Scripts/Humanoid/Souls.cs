using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public abstract class Souls : MonoBehaviour
{
   [SerializeField] private float health;
    public float Health
    {
        get
        {
            return health;
        }

        set
        {
            health = value;

            if(value < 0)
            {
                health = 0;
            }

            if(value <= 0)
            {
               Death();
            }
        }
    }

    [SerializeField] private float damage;
    public float Damage
    {
        get
        {
            return damage;
        }
        set
        {
            damage = value;

            if (isHit)
            {
                //takeDamage();
            }
        }
    }

   
    public float xSpeed;
    public float ySpeed;
    public float jumpTime;
    public float xDistance;
    public float yDistance;
    public Vector2 Direction;
    public float angle;
    public float timeFromGround;
    public bool canHit;
    public bool isHit;
    public bool isGrounded;
    public bool canDoubleJump;
    public bool isJumping;
    public bool canAttack; 

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public abstract void Attack();

    public abstract void Movement();
    

    public void GetDistance(Vector2 Direction_)
    {
        xDistance = Mathf.Sqrt(Mathf.Pow(Direction_.x, 2));
        yDistance = Mathf.Sqrt(Mathf.Pow(Direction_.y, 2));
    }

    public void GetAngle(Vector2 Direction_)
    {
        angle = Vector2.Angle(transform.right,Direction_);
    }

    public abstract void Death();


}


