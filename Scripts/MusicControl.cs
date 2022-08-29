using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicControl : MonoBehaviour
{
    public AudioSource MusicLvl1;
    public AudioSource MusicLvl2;
    public AudioSource MusicLvl3;
    public AudioSource MusicLvl4;
    public GameObject test1;
    public GameObject test2;
    public GameObject test3;
    public GameObject test4;
    public AudioClip ahotdog;
    AudioSource hotdogSource;
    private AudioSource swingSource;
    public AudioClip[] audioSwingClips;
    public AudioSource ambientSource;
    public AudioClip[] audioAmbientClips;
    public AudioSource gAttackSource;
    public AudioClip[] audiogAttackClips;
    public AudioSource gohdilOuchSource;
    public AudioClip[] gohdilOuchClips;


    public GameObject[] enemies;

    // Start is called before the first frame update


    void Start()
    {

        test1 = GameObject.Find("PlayerCharacter/MusicLvl1");
        test2 = GameObject.Find("PlayerCharacter/MusicLvl2");
        test3 = GameObject.Find("PlayerCharacter/MusicLvl3");
        test4 = GameObject.Find("PlayerCharacter/MusicLvl4");

        MusicLvl1 = test1.GetComponent<AudioSource>();
        MusicLvl2 = test2.GetComponent<AudioSource>();
        MusicLvl3 = test3.GetComponent<AudioSource>();
        MusicLvl4 = test4.GetComponent<AudioSource>();

        hotdogSource = GetComponent<AudioSource>();
        swingSource = GetComponent<AudioSource>();
        ambientSource = GetComponent<AudioSource>();
        gAttackSource = GetComponent<AudioSource>();
        gohdilOuchSource = GetComponent<AudioSource>();
    }


    public void playghostattackswing()
    {
        AudioClip RandomClip()
        {
            return audiogAttackClips[Random.Range(0, audiogAttackClips.Length)];
        }
        gAttackSource.PlayOneShot(RandomClip());
    }

    public void playgohdilouch()
    {
        AudioClip RandomClip()
        {
            return gohdilOuchClips[Random.Range(0, gohdilOuchClips.Length)];
        }
        gohdilOuchSource.PlayOneShot(RandomClip());
    }

    public void playattackswing()
    {
        AudioClip RandomClip()
        {
            return audioSwingClips[Random.Range(0, audioSwingClips.Length)];
        }
        swingSource.PlayOneShot(RandomClip());
    }
    public void playhotdog()
    {
        Debug.Log("hotdogplay");
        GetComponent<AudioSource>().PlayOneShot(ahotdog, 1f);
    }

    // Update is called once per frame

    void Update()
    {
        enemies = GameObject.FindGameObjectsWithTag("Enemy");

        for (int i = 0; i < enemies.Length; i++)


            if (enemies.Length <= 6)
            {
                MusicLvl1.volume = 0.6f;
                MusicLvl2.volume = 0;
                MusicLvl3.volume = 0;
                MusicLvl4.volume = 0;
            }

            else if (enemies.Length > 6 && enemies.Length <= 12)
            {
                MusicLvl1.volume = 0;
                MusicLvl2.volume = 0.6f;
                MusicLvl3.volume = 0;
                MusicLvl4.volume = 0;
            }
            else if (enemies.Length > 12 && enemies.Length <= 20)
            {
                MusicLvl1.volume = 0;
                MusicLvl2.volume = 0;
                MusicLvl3.volume = 0.6f;
                MusicLvl4.volume = 0;
            }
            else if (enemies.Length > 20)
            {
                MusicLvl1.volume = 0;
                MusicLvl2.volume = 0;
                MusicLvl3.volume = 0;
                MusicLvl4.volume = 0.4f;
            }
    }

    public void muteAllMusic(){
        MusicLvl2.mute = true;
        MusicLvl1.mute = true;
        MusicLvl3.mute = true;
        MusicLvl4.mute = true;

    }
    public void unmuteAllMusic(){
        MusicLvl2.mute = false;
        MusicLvl1.mute = false;
        MusicLvl3.mute = false;
        MusicLvl4.mute = false;

    }

}