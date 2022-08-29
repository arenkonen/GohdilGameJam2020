using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySplitting : MonoBehaviour
{
    public GameObject enemyPrefab;
    public float respawnTime = 1.0f;
    // Start is called before the first frame update
    private Vector2 rightEnemy;
    private Vector2 leftEnemy;
    private float[] randomValue = new float[4];

    public void splitEnemies(Vector2 position, GameObject oldEnemy)
    {   
        //sets new spawn locations for the two new enemies
        rightEnemy = new Vector2(0.25f, 0.1f) + position;
        leftEnemy = new Vector2(-0.25f, 0.1f) + position;
        StartCoroutine(ExecuteAfterTime(rightEnemy, leftEnemy, oldEnemy));
    }
    IEnumerator ExecuteAfterTime(Vector2 right, Vector2 left, GameObject old)
    {
        //respawns two new enemies after a set time, and destroys the old enemies corpse. 
        yield return new WaitForSeconds(respawnTime);
        Instantiate(enemyPrefab, left, Quaternion.identity);
        
        Instantiate(enemyPrefab, right, Quaternion.identity);
        Destroy(old);

    }

}
