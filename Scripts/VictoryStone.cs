using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(AudioSource))]
public class VictoryStone : MonoBehaviour
{
    public float deathDelay = 1.0f;
    public Image flasher;
    private bool flash = false;
    private float counter = 0.1f;
    private GameObject[] enemies;

    public AudioSource MusicLvl1;
    public AudioSource MusicLvl2;
    public AudioSource MusicLvl3;
    public AudioSource MusicLvl4;

    public GameObject test1;
    public GameObject test2;
    public GameObject test3;
    public GameObject test4;

    public CamShake cam;
    public AudioClip FULGUR1;
    Animator guardian;
    AudioSource audio;

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
        audio = GetComponent<AudioSource>();
        cam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CamShake>();
        flasher = GameObject.Find("Flash").GetComponent<Image>();

    }

    void Update()
    {
        if (flash)
        {
            flasher.color = Color.white;
            counter -= Time.deltaTime;
            if (counter <= 0)
            {
                flasher.color = Color.clear;
                flash = false;
            }
        }
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            flash = true;
            counter = 0.1f;
            //StartCoroutine(cam.Shake(0.1f, 0.1f));
            audio.PlayOneShot(FULGUR1, 1);
            StartCoroutine(fulgurDelay());
        }
    }

    IEnumerator fulgurDelay()
    {
        yield return new WaitForSeconds(deathDelay);
        enemies = GameObject.FindGameObjectsWithTag("Enemy");
        for (int i = 0; i < enemies.Length; i++)
        {
            guardian = enemies[i].GetComponent<Animator>();
            guardian.SetBool("Fulgur", true);
        }

        yield return new WaitForSeconds(deathDelay / 2);
        enemies = GameObject.FindGameObjectsWithTag("Enemy");
        for (int i = 0; i < enemies.Length; i++)
        {
            Destroy(enemies[i].gameObject);
        }
        MusicLvl1.volume = 0.6f;
        MusicLvl2.volume = 0;
        MusicLvl3.volume = 0;
        MusicLvl4.volume = 0;

        yield return new WaitForSeconds(deathDelay);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);

    }



}
