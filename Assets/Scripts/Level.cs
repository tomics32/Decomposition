using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level : MonoBehaviour
{
    // parametry
    [SerializeField] int breakableBlocks;

    // cached reference
    SceneLoader sceneloader;
    public Animator transition;
    Ball theBall;
    public GameSession gameSession;

    private void Start()
    {
        sceneloader = FindObjectOfType<SceneLoader>();
        theBall = FindObjectOfType<Ball>();
        
    }

    private void DeleteData()
    {
        PlayerPrefs.DeleteKey("levelReached");
    }
    public void CountBlocks()
    {
        breakableBlocks = breakableBlocks + 1;
    }

    public void BlockDestroyed()
    {
        breakableBlocks = breakableBlocks - 1;
        if (breakableBlocks <= 0)
        {
            theBall.StopBall();
            transition.SetTrigger("Start");
            sceneloader.Invoke("LoadNextScene", 1f);
            gameSession.levelProgress();
            
        }
    }
    
}
