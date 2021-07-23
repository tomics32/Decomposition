using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paddle : MonoBehaviour
{
    [SerializeField] float minX = -20f;
    [SerializeField] float maxX = 20f;
    [SerializeField] float screenWidthInUnits = 22f;
    [SerializeField] GameObject[] paddleSize;

    GameSession theGameSession;
    Ball theBall;
    Block block;



   
    void Start()
    {
        theGameSession = FindObjectOfType<GameSession>();
        theBall = FindObjectOfType<Ball>();
        block = FindObjectOfType<Block>();
    }

   
    void Update()
    {
        Vector2 paddlePos = new Vector2(transform.position.x , transform.position.y);
        paddlePos.x = Mathf.Clamp(GetXPos(), minX, maxX);
        transform.position = paddlePos;
    }

    private float GetXPos()
    {
        if (theGameSession.IsAutoPlayEnabled())
        {
            return theBall.transform.position.x;
        }
        else
        {
            return Input.mousePosition.x / Screen.width * screenWidthInUnits;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ball")
        {
            theBall.pushSpeed = theBall.pushSpeed + 0.15f;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {


        if (collision.gameObject.tag == "SlowDown")
        {
            theBall.pushSpeed = 15f;
            Destroy(collision.gameObject);
            FindObjectOfType<GameSession>().AddToScore();
        }
        
        if (collision.gameObject.tag == "WidePaddle")
        {
            if (transform.localScale.x < 1.4 && transform.localScale.x > 0.9)
            {
                Destroy(gameObject);
                GameObject widePaddle = Instantiate(paddleSize[2], transform.position, transform.rotation);
            }
            else if (transform.localScale.x < 0.9)
            {
                Destroy(gameObject);
                GameObject wideDefaultPaddle = Instantiate(paddleSize[1], transform.position, transform.rotation);
            }
            else if (transform.localScale.x > 1.4 && transform.localScale.x < 1.7)
            {
                Destroy(gameObject);
                GameObject widerPaddle = Instantiate(paddleSize[3], transform.position, transform.rotation);
            }
            Destroy(GameObject.FindWithTag("WidePaddle"));
            FindObjectOfType<GameSession>().AddToScore();
        }

        if (collision.gameObject.tag == "SmallPaddle")
        {
            if (transform.localScale.x > 1.4 && transform.localScale.x < 1.6)
            {
                Destroy(gameObject);
                GameObject smallDefaultPaddle = Instantiate(paddleSize[1], transform.position, transform.rotation);
            }
            else if (transform.localScale.x > 1.75)
            {
                Destroy(gameObject);
                GameObject smallWidePaddle = Instantiate(paddleSize[2], transform.position, transform.rotation);
            }
            else if (transform.localScale.x < 1.1 && transform.localScale.x > 0.9)
            {
                Destroy(gameObject);
                GameObject smallPaddle = Instantiate(paddleSize[0], transform.position, transform.rotation);
            }
            Destroy(collision.gameObject);
        }

        if (collision.gameObject.tag == "SpeedUp")
        {
            theBall.pushSpeed = 20f;
            Destroy(collision.gameObject);
        }

        if (collision.gameObject.tag == "Small Ball")
        {
            theBall.SmallBall();
            Destroy(collision.gameObject);
        }

        if (collision.gameObject.tag == "Through Square")
        {
            block.TriggerBlocks();
            Destroy(collision.gameObject);
        }
    }
}
