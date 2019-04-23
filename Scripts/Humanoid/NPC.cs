using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : Souls
{
    protected enum EnemyStates
    {
        Looking,
        Attacking,
    };

    protected EnemyStates currentState;
    public Ninja ninja;
    public int EnemyDirection;
    public Ray2D enemyRay;
    public bool playerDetected;
    public LayerMask playerLayer;
    [Range(0.0f, 100.0f)][SerializeField]
    protected float detectingRayLength;
    public bool detectingCast;
    public static List<NPC> testList = new List<NPC>();
    protected Rigidbody2D EnemyMovement;


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public virtual Vector2 GetDirectionOfSoul(Souls Player, List<NPC> Enemy)
    {
       return Direction = ninja.transform.position - transform.position;    
    }

    public override void Attack()
    {

    }

    public override void Movement()
    {

    }

    public virtual void Sees()
    {
        if (angle < 100)
        {
            enemyRay = new Ray2D(transform.position, transform.right * detectingRayLength);
            Debug.DrawRay(new Vector2(transform.position.x + 8,transform.position.y), transform.right * detectingRayLength, Color.red);

            if (Physics2D.Raycast(new Vector2(transform.position.x + 8, transform.position.y), transform.right, detectingRayLength, playerLayer))
            {
                playerDetected = true;
            }

            else
            {
                playerDetected = false;
            }
        }

        else if (angle > 100)
        {
            enemyRay = new Ray2D(transform.position, -transform.right * detectingRayLength);
            Debug.DrawRay(new Vector2(transform.position.x  -8, transform.position.y), -transform.right * detectingRayLength, Color.red);

            if (Physics2D.Raycast(new Vector2(transform.position.x  -8, transform.position.y), -transform.right, detectingRayLength, playerLayer))
            {
                playerDetected = true;
            }

            else
            {
                playerDetected = false;
            }
        }
    }

    public override void Death()
    {
        Destroy(gameObject);
    }

    public virtual void TakeDamage()
    {
        Health -= ninja.Damage;
    }

    private void OnDestroy()
    {
        testList.Remove(this);
    }

    public void OnStartUp()
    {
        for (int i = 0; i < testList.Count; ++i)
        {
            EnemyMovement = testList[i].GetComponent<Rigidbody2D>();
        }
    }
}
