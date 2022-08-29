using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveWall : MonoBehaviour
{
    public Vector2 goal;
    public float speed = 4.0f;

    public void moveDoor(){
        transform.position = Vector2.MoveTowards(transform.position, goal, speed * Time.deltaTime);
    }

}
