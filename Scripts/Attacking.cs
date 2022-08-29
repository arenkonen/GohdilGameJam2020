using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attacking : MonoBehaviour
{
    public float attackTime;
    public float startTimeAttack;
    
    public Transform attackLocation;
    public Vector2 attackRange;
    public LayerMask enemies;
    private Animator anim;
    public Animator swipe;


    // Start is called before the first frame update
    void Start()
    {
        
        anim = GetComponent<Animator>();
        swipe = GameObject.Find("AttackLocation").GetComponent<Animator>();
        
    }

    // Update is called once per frame
    void Update()
    {
        if (attackTime <= 0)
        {
            if (Input.GetButton("Fire1"))
            {
                GameObject.Find("PlayerCharacter/MusicControl").GetComponent<MusicControl>().playattackswing();
                anim.SetBool("Is_Attacking", true);
                swipe.SetBool("Is_Attacking", true);

                //Checks if the attack location overlaps with an enemy, and for each overlapping does 1 damage
                
                Collider2D[] damage = Physics2D.OverlapCapsuleAll(attackLocation.position, attackRange, CapsuleDirection2D.Vertical, enemies);
                for (int i = 0; i < damage.Length; i++)
                {
                    if (damage[i].gameObject.tag == "Enemy" && anim.GetBool("Alive"))
                    {   
                        damage[i].gameObject.GetComponent<EnemyAI>().getDamaged();
                        
                    }

                }
                //the timer starts to not allow spamming of attacks
                attackTime = startTimeAttack;
            }


        }
        else
        {
            attackTime -= Time.deltaTime;
            anim.SetBool("Is_Attacking", false);
            swipe.SetBool("Is_Attacking",false);
        }
    }

    
}

