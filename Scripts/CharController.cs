using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharController : MonoBehaviour
{
    Rigidbody2D body;

    public Vector2 movement;
    float horizontal;
    float vertical;
    public float runSpeed = 1.0f;
    public float hf = 0.0f;
    public float vf = 0.0f;
    public Animator anim;
    GameObject attackLocation;

    void Start()
    {
        body = this.GetComponent<Rigidbody2D>();
        anim = this.GetComponent<Animator>();
        attackLocation = GameObject.Find("AttackLocation");
    }

    void Update()
    {
        //Input is between 1 and -1
        movement.x = Input.GetAxisRaw("Horizontal"); // -1 is left
        movement.y = Input.GetAxisRaw("Vertical"); // -1 is down

        //normalizes speed for diagonal movement
        movement = movement.normalized;

        //checks which direction the character is facing
        hf = movement.x > 0.01f ? movement.x : movement.x < -0.01f ? 1 : 0;
        vf = movement.x > 0.01f ? movement.y : movement.y < -0.01f ? 1 : 0;

        //Checks if the player moves left or right and flips the sprite in the appropriate direction, it also notifies the animator if it is moving sideways
        if (movement.x < -0.01f)
        {
            attackLocation.GetComponent<Animator>().SetBool("Is_AttackingLeft", false);
            this.gameObject.transform.localScale = new Vector3(-1, 1, 1);
            attackLocation.transform.localPosition = new Vector3 (0.2f, 0f, 0f);
            anim.SetBool("Is_Moving_Side", true);
            attackLocation.GetComponent<Animator>().SetBool("Is_AttackingRight",true);
            attackLocation.GetComponent<Animator>().SetBool("Is_AttackingUp",false);
            attackLocation.GetComponent<Animator>().SetBool("Is_AttackingDown",false);
            
        }
        else if (movement.x > 0.01f)
        {
            this.gameObject.transform.localScale = new Vector3(1, 1, 1);
            attackLocation.transform.localPosition = new Vector3 (0.2f, 0f, 0f);
            
            attackLocation.GetComponent<Animator>().SetBool("Is_AttackingUp",false);
            attackLocation.GetComponent<Animator>().SetBool("Is_AttackingDown",false);            
            attackLocation.GetComponent<Animator>().SetBool("Is_AttackingRight",true);
            attackLocation.GetComponent<Animator>().SetBool("Is_AttackingLeft",false);
            
            anim.SetBool("Is_Moving_Side", true);
        }
        else if(movement.y > 0.0f){
            anim.SetBool("Is_Moving_Side", true);
            attackLocation.transform.localPosition = new Vector3 (0f, 0.2f, 0f);
            attackLocation.GetComponent<Animator>().SetBool("Is_AttackingRight", false);
            attackLocation.GetComponent<Animator>().SetBool("Is_AttackingLeft",false);
            attackLocation.GetComponent<Animator>().SetBool("Is_AttackingUp", true);
            attackLocation.GetComponent<Animator>().SetBool("Is_AttackingDown",false);

        }
        else if(movement.y < 0.0f){
            anim.SetBool("Is_Moving_Side", true);
            attackLocation.GetComponent<Animator>().SetBool("Is_AttackingDown", true);
            attackLocation.transform.localPosition = new Vector3 (0f, -0.2f, 0f);
            attackLocation.GetComponent<Animator>().SetBool("Is_AttackingRight",false);
            attackLocation.GetComponent<Animator>().SetBool("Is_AttackingLeft",false);
            attackLocation.GetComponent<Animator>().SetBool("Is_AttackingUp",false);

        }
        else anim.SetBool("Is_Moving_Side", false);
        //sets the parameters for the animator to use
        anim.SetFloat("Horizontal", hf);
        anim.SetFloat("Vertical", movement.y);
        anim.SetFloat("Speed", vf);


    }

    private void FixedUpdate()
    {
        //Moves the player based on the current inputs.
        body.MovePosition(body.position + movement * runSpeed * Time.fixedDeltaTime);
    }
}
