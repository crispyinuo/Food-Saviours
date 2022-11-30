using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameResult : MonoBehaviour
{
    // Start is called before the first frame update
    static public bool didWin = true;
    public AudioSource WinSound;
    public AudioSource LoseSound;
    public TextMeshProUGUI resultDisplay;

    // Update is called once per frame
    void Start(){
        if(didWin){
            resultDisplay.text = "You Win!";
            WinSound.Play();
        }else{
            resultDisplay.text = "You Lose!";
            LoseSound.Play();
        }   
    }
    void Update()
    { 
    }
}
