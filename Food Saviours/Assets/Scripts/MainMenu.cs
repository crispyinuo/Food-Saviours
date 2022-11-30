using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{   
    public static string Result = "You Win!";
    private int level = 1;
    public AudioSource levelSound;
    public AudioSource playSound;

        public void QuitGame()
    {
        Application.Quit();
    }

    public void RestartGame()
    {
        SceneManager.LoadScene("Start");
      //  Debug.Log("restarting");
    }

    public void PlayGame()
    {
        playSound.Play();
        SceneManager.LoadScene("Game");
       // Debug.Log("starting game");
    }

    public void SetLevel(int level){
        if(level >= 1 && level <= 6){
            this.level = level;
        }
        levelSound.Play();
        switch(level){
            case 1:
                MazeRenderer.foodcount = 5;
                MazeRenderer.soldiercount = 1;
                break;
            case 2:
                MazeRenderer.foodcount = 8;
                MazeRenderer.soldiercount = 1;
                break;
            case 3:
                MazeRenderer.foodcount = 10;
                MazeRenderer.soldiercount = 2;
                break;
            case 4:
                MazeRenderer.foodcount = 12;
                MazeRenderer.soldiercount = 2;
                break;
            case 5:
                MazeRenderer.foodcount = 12;
                MazeRenderer.soldiercount = 3;
                break;
            case 6:
                MazeRenderer.foodcount = 15;
                MazeRenderer.soldiercount = 3;
                break;
        }
    }
}
