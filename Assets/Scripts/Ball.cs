using UnityEngine;

public class Ball : MonoBehaviour
{

    
    [SerializeField] Paddle paddle1;  
    [SerializeField] AudioClip ballSounds;
    [Range(8f, 20.0f)] [SerializeField] public float pushSpeed = 15.0f;
    [Range(0.2f, 0.9f)] [SerializeField] float bounceFudge = 0.4f;
    [SerializeField] Vector2 ballVector;
    Vector2 paddleToBallVector;
    bool hasStarted = false;
    

    
    AudioSource myAudioSource;
    Rigidbody2D myRigidBody2D;

    
    void Start()
    {

        if (gameObject.tag == "Ball")
        {
            paddleToBallVector = transform.position - paddle1.transform.position;
        }
        myAudioSource = GetComponent<AudioSource>();
        myRigidBody2D = GetComponent<Rigidbody2D>();
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (hasStarted == false)
        {
            LockBallToPaddle();
            LaunchOnMouseClick();
        }
    }

    private void LaunchOnMouseClick()
    {
        if (Input.GetMouseButtonDown(0))
        {
            hasStarted = true;
            myRigidBody2D.velocity = new Vector2(bounceFudge, pushSpeed);
            
        }
    }

    private void LockBallToPaddle()
    {
        if (gameObject.tag == "Ball")
        {
            Vector2 paddlePos = new Vector2(paddle1.transform.position.x, paddle1.transform.position.y);
            transform.position = paddlePos + paddleToBallVector;
        }
    }

    public void OnCollisionExit2D(Collision2D collision)
    {
        
        ballVector = myRigidBody2D.velocity;

         float ballAngle = Mathf.Atan2(ballVector.y, ballVector.x);

        //Przyspieszenie pilki
         ballVector.x = Mathf.Cos(ballAngle) * pushSpeed;
         ballVector.y = Mathf.Sin(ballAngle) * pushSpeed;
         myRigidBody2D.velocity = ballVector;
      
        //Naprawianie fizyki by piłka sie nie zablokowala

        if (ballVector.y <= 0.2f && ballVector.y > 0)
        {
            ballVector.y = ballVector.y + 0.6f;
        }
        else if (ballVector.y >= -0.2f && ballVector.y <= 0)
        {
            ballVector.y = ballVector.y - 0.6f;
        }

        if (ballVector.x <= 0.2f && ballVector.x > 0)
        {
            ballVector.x = ballVector.x + 0.6f;
        }
        else if (ballVector.x >= -0.2f && ballVector.x <= 0)
        {
            ballVector.x = ballVector.x - 0.6f;
        }

    } 

        
    private void OnCollisionEnter2D(Collision2D collision)
    {        
        if (hasStarted == true && collision.gameObject.tag.Equals("Breakable") == false)
        {
            AudioClip clip = ballSounds;
            myAudioSource.PlayOneShot(clip);
        }    
    }
    public void SmallBall()
    {
        Vector2 ballSize = new Vector2(0.8f, 0.8f);
        transform.localScale = ballSize;
    }
    public void StopBall()
    {
        GetComponent<Rigidbody2D>().velocity = new Vector2(2f, 2f);
    }
}