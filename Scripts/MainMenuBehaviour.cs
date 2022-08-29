using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenuBehaviour : MonoBehaviour
{
    Text startGame;
    Color baseColor = new Color (1f, 0.6519095f, 0.1745283f, 1.0f);
    // Start is called before the first frame update
    void Start()
    {
        startGame = GameObject.Find("StartGameText").GetComponent<Text>();
        startGame.color = baseColor;
    }
    public void StartGame(){
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    // Update is called once per frame
    public void mouseHover(){
        startGame.color = new Color(1, 0.9420261f, 0.8632076f);
    }

    public void mouseHoverExit(){
        startGame.color = baseColor;
    }
}
