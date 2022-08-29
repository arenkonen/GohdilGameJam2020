using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class EnemyAI : MonoBehaviour
{
    public int health = 2;
    public EnemySplitting splitScript;
    private SpriteRenderer spriteRenderer;
    private Transform enemyLocation;
    private Animator anim;

    Rigidbody2D body;
    public float speed;
    private Transform player;
    private bool alive;
    private bool didDamage = false;
    private bool touching = false;
    public float attackDelay = 0.5f;
    private AudioSource ghostSource;
    Animator childAnim;
    public AudioClip[] audioGhostClips;
    AudioClip RandomClip(){
        return audioGhostClips[Random.Range (0, audioGhostClips.Length)];
        }
    // Start is called before the first frame update
    void Start()
    {
       /*  aGhost = GetComponent<AudioSource>(); */
        ghostSource = GetComponent<AudioSource>();
        spriteRenderer = this.gameObject.GetComponent<SpriteRenderer>();
        enemyLocation = this.gameObject.GetComponent<Transform>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        body = GetComponent<Rigidbody2D>();
        alive = true;
        anim = GetComponent<Animator>();
        anim.SetBool("respawn",true);
        childAnim = gameObject.transform.Find("AttackArea").GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        anim.SetBool("respawn",false);
        //when health reaches 0 the enemy splits into 2 new via the splitting script
        if (health <= 0)
        {
             
             if (alive){
        ghostSource.PlayOneShot (RandomClip());
        }
    
            health = 2;
            alive = false;
            anim.SetBool("alive", false);
            splitScript.splitEnemies(enemyLocation.position, this.gameObject);
        }

        //if the enemy is alive, it will follow the player
        if (alive && !didDamage)
        {

            if (Vector2.Distance(transform.position, player.position) > 5)
            {
                transform.position = Vector2.MoveTowards(transform.position, player.position, speed * Time.deltaTime *1.6f);
            }
            else if (Vector2.Distance(transform.position, player.position) <= 5 && Vector2.Distance(transform.position, player.position) > 0.3)
            {
                transform.position = Vector2.MoveTowards(transform.position, player.position, speed * 0.75f * Time.deltaTime);
            }
            if (transform.position.x < player.position.x){
                spriteRenderer.flipX = true;
            }
            else spriteRenderer.flipX = false;
        }
        else if(alive && didDamage){            
            transform.position =Vector2.MoveTowards(transform.position, -player.position, speed * Time.deltaTime);

            if (transform.position.x > player.position.x){
                spriteRenderer.flipX = true;
            }
            else spriteRenderer.flipX = false;

            StartCoroutine(enemyFlee());
        }
        if(didDamage){
            childAnim.SetBool("Is_Attacking", false);
        }


    }
    //once the enemy touches the player they will start doing damage
    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            //The enemy must be touching the player to do damage
            touching = true;
            StartCoroutine(waitForAttack(other));
        }

    }

    void OnCollisionExit2D(Collision2D other)
    {
        //once the player leaves the collision with the enemy, they are not touching.
        touching = false;
    }

    IEnumerator waitForAttack(Collision2D other)
    {
        //after the player and enemy have been touching for the attackDelay damage is dealt
        yield return new WaitForSeconds(attackDelay);
        if (touching && alive)
        {
            CharacterHealth cHP = other.gameObject.GetComponent<CharacterHealth>();
            cHP.getDamaged();
            didDamage = true;
            childAnim.SetBool("Is_Attacking", true);
            GameObject.Find("PlayerCharacter/MusicControl").GetComponent<MusicControl>().playghostattackswing();
            GameObject.Find("PlayerCharacter/MusicControl").GetComponent<MusicControl>().playgohdilouch();

        }

    }
    IEnumerator enemyFlee(){
        yield return new WaitForSeconds(0.5f);
        didDamage = false;
    }

    public void getDamaged()
    {
        health -= 1; 
    }
}
