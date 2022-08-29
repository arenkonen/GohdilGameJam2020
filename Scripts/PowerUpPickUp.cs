using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpPickUp : MonoBehaviour
{

    // Start is called before the first frame update
    void Start(){

    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            //randomly changes your color after pickup 
            SpriteRenderer sprite = other.gameObject.GetComponent<SpriteRenderer>();
            sprite.material.color = Random.ColorHSV(0f, 1f, 1f, 1f, 0.5f, 1f);
        
            GameObject.Find("PlayerCharacter/MusicControl").GetComponent<MusicControl>().playhotdog();
            Destroy(this.gameObject);
        }

    }
}
