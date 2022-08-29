using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIEffects : MonoBehaviour
{
    Image deathScreen;
    GameObject buttonRestart;
    GameObject PC;
    bool start;
    bool paused = false;
    Image flash;
    Image scroll;
    Text pauseScreen;
    Text unpauseScreen;
    GameObject[] enemies;
    // Start is called before the first frame update
    void Start()
    {
        deathScreen = GameObject.Find("DeathScreen").GetComponent<Image>();
        buttonRestart = GameObject.Find("RestartButton");
        buttonRestart.SetActive(false);
        PC = GameObject.FindGameObjectWithTag("Player");
        flash = GameObject.Find("Flash").GetComponent<Image>();
        scroll = GameObject.Find("ScrollPause").GetComponent<Image>();
        pauseScreen = GameObject.Find("PauseText").GetComponent<Text>();
        unpauseScreen = GameObject.Find("unpauseText").GetComponent<Text>();

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.anyKey && start && !(SceneManager.GetActiveScene().name == "Level1"))
        {
            Time.timeScale = 1;
            flash.color = new Color(1, 1, 1, 0);
            PC.GetComponent<Animator>().SetBool("Fishing", false);

            start = false;
        }
        else if (Input.anyKey && start && (SceneManager.GetActiveScene().name == "Level1"))
        {
            Time.timeScale = 1;
            PC.GetComponent<Animator>().SetBool("Fishing", false);
            for (int i = 0; i < enemies.Length; i++)
            {
                enemies[i].SetActive(true);
            }
            start = false;
        }
        if (Input.GetKeyDown(KeyCode.Return) && !paused)
        {
            Time.timeScale = 0;
            flash.color = new Color(0.25f, 0.25f, 0.25f, 0.5f);
            scroll.color = new Color(1,1,1,1);
            pauseScreen.color = new Color(0, 0.7693628f, 0.7830189f, 1);
            unpauseScreen.color = new Color(0, 0.7693628f, 0.7830189f, 0);
            buttonRestart.SetActive(true);
            paused = true;

        }
        else if (Input.GetKeyDown(KeyCode.Return) && paused)
        {
            Time.timeScale = 1;
            flash.color = new Color(1, 1, 1, 0);
            scroll.color = new Color(1, 1, 1, 0);
            pauseScreen.color = new Color(0, 0, 0, 0);
            unpauseScreen.color = new Color(0, 0.7693628f, 0.7830189f, 1);
            buttonRestart.SetActive(false);
            paused = false;
        }
    }

    public void onDeath()
    {
        StartCoroutine(FadeImage(true));
        buttonRestart.SetActive(true);
        PC.GetComponent<CharacterHealth>().enabled = false;
        PC.GetComponent<Attacking>().enabled = false;
        PC.GetComponent<CharController>().enabled = false;
    }

    IEnumerator FadeImage(bool fadeAway)
    {
        if (fadeAway)
        {
            for (float i = 0; i <= 1; i += Time.deltaTime)
            {
                deathScreen.color = new Color(1, 1, 1, i);
                yield return null;
            }
        }

    }

    public void restartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    void Awake()
    {

        if (SceneManager.GetActiveScene().name == "Level1")
        {
            PC = GameObject.FindGameObjectWithTag("Player");
            PC.GetComponent<Animator>().SetBool("Fishing", true);
            enemies = GameObject.FindGameObjectsWithTag("Enemy");
            for (int i = 0; i < enemies.Length; i++)
            {
                enemies[i].SetActive(false);
            }
            start = true;
        }
        else
        {
            Time.timeScale = 0;
            GameObject.Find("Flash").GetComponent<Image>().color = new Color(0.25f, 0.25f, 0.25f, 0.5f);
            start = true;
        }


    }
}
