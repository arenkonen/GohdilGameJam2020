using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
[RequireComponent(typeof(AudioSource))]
public class CharacterHealth : MonoBehaviour
{
    public int healthPoints = 10;
    Rigidbody2D body;
    Animator anim;
    public Slider slider;
    bool damaged = false;
    public SpriteRenderer pcSprite;

    float counter = 0.1f;

    public AudioSource MusicLvl1;
    public AudioSource MusicLvl2;
    public AudioSource MusicLvl3;
    public AudioSource MusicLvl4;

    public GameObject test1;
    public GameObject test2;
    public GameObject test3;
    public GameObject test4;

    public AudioClip SadTheme;
    UIEffects UIScript;
    AudioSource audio;


    // Start is called before the first frame update
    void Start()
    {
        UIScript = GameObject.Find("UI").GetComponent<UIEffects>(); 
        body = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        slider = GameObject.Find("HealthBar").GetComponent<Slider>();

        test1 = GameObject.Find("PlayerCharacter/MusicLvl1");
        test2 = GameObject.Find("PlayerCharacter/MusicLvl2");
        test3 = GameObject.Find("PlayerCharacter/MusicLvl3");
        test4 = GameObject.Find("PlayerCharacter/MusicLvl4");

        MusicLvl1 = test1.GetComponent<AudioSource>();
        MusicLvl2 = test2.GetComponent<AudioSource>();
        MusicLvl3 = test3.GetComponent<AudioSource>();
        MusicLvl4 = test4.GetComponent<AudioSource>();
        audio = GetComponent<AudioSource>();
        pcSprite = this.gameObject.GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if(healthPoints <= 0){
            if (anim.GetBool("Alive")){
            audio.PlayOneShot(SadTheme, 0.8f);
            }   
            MusicLvl1.mute = true;      
            MusicLvl2.mute = true;      
            MusicLvl3.mute = true;      
            MusicLvl4.mute = true;
            
            body.constraints = RigidbodyConstraints2D.FreezeAll;
            pcSprite.material.color = Color.red;
            UIScript.onDeath();
            
           
            anim.SetBool("Alive", false);

        }
        if(damaged){
             pcSprite.material.color = new Color(1, 0, 0, 1);
             counter -= Time.deltaTime;
            if(counter <= 0){
                pcSprite.material.color = new Color(1, 1, 1, 1);
                damaged = false;
                counter = 0.2f;
            }
             
        }
    }

    public void getDamaged()
    {
        healthPoints -= 1;
        slider.value = healthPoints;
        damaged = true;

    }
}
