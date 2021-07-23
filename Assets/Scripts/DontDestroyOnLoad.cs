using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DontDestroyOnLoad : MonoBehaviour
{
    public static DontDestroyOnLoad instance;

     private void Awake()
        {
          if (instance != null)
            {
                Destroy(transform.gameObject);
            }
            else
            {
                instance = this;
                DontDestroyOnLoad(transform.gameObject);
            }
        }
    

    private void OnLevelWasLoaded(int level)
    {
        Scene scene = SceneManager.GetActiveScene();
        

        if(scene.name != "LevelSelect" && scene.name != "Start Menu" && scene.name != "Game Over" && scene.name != "Scoreboard" && scene.name != "Settings")
        {
            Destroy(gameObject);
        }
    }
   
    
}
