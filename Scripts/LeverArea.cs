using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LeverArea : MonoBehaviour
{
    // Start is called before the first frame update
    private bool active = false;
    public float timeToDoor = 5;
    public float countUp = 0;
    public GameObject door;
    Transform doorTransform;
    public Vector2 goal;
    public Slider slider;
    SpriteRenderer areaSprite;

    void Start()
    {
        areaSprite = this.gameObject.GetComponent<SpriteRenderer>();
        if (door != null)
        {
            doorTransform = door.transform;
        }
        else
        {
            Debug.Log("No Door!");
        }
    }

    // Update is called once per frame
    void Update()
    {
        //while in the set area the timer counts down, once the countdown reaches 0 the door opens
        if (active)
        {
            if (countUp < timeToDoor)
            {
                countUp += Time.deltaTime;
                slider.value = countUp;
            }
        }
        if (countUp >= timeToDoor)
        {
            doorTransform.position = Vector2.MoveTowards(doorTransform.position, goal, 5.0f * Time.deltaTime);
            //Destroy(slider.gameObject);

            for (float i = 1; i >= 0; i -= Time.deltaTime)
            {
                areaSprite.color = new Color(1, 1, 1, i);
            }
            StartCoroutine(destroyDelay());
        }




    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        //Checks if the collider that entered the trigger is the player, which allows the countdown to start
        if (other.gameObject.tag == "Player")
        {
            active = true;
        }

    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            active = false;
        }
    }

    IEnumerator destroyDelay()
    {
        yield return new WaitForSeconds(1);
        Destroy(this.gameObject);
    }


}
