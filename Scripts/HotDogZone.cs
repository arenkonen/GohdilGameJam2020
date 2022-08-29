using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HotDogZone : MonoBehaviour
{
    // Start is called before the first frame update
    GameObject player;
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        this.gameObject.GetComponent<AudioSource>().mute = true;
    }

    // Update is called once per frame
    void Update()
    {
        
      
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        //Checks if the collider that entered the trigger is the player, which allows the countdown to start
        if (other.gameObject.tag == "Player")
        {
            other.gameObject.transform.Find("MusicControl").GetComponent<MusicControl>().muteAllMusic();
            this.gameObject.GetComponent<AudioSource>().mute = false;
            
        }

    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            other.gameObject.transform.Find("MusicControl").GetComponent<MusicControl>().unmuteAllMusic();
            this.gameObject.GetComponent<AudioSource>().mute = true;
        }
    }
}
