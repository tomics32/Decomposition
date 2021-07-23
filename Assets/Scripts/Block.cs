using UnityEngine;

public class Block : MonoBehaviour
{
    
    [SerializeField] AudioClip breakSound;
    [SerializeField] GameObject blockSparklesVFX;
    [SerializeField] Sprite[] hitSprites;
    [SerializeField] GameObject[] objToSpawn;

    float randomBlock;
    
    
    Level level;
    

    
    [SerializeField] int timesHit; 

    
    private void Start()
    {
        CountBreakableBlocks();
        
        
    }

    private void CountBreakableBlocks()
    {
        level = FindObjectOfType<Level>();
        if (tag == "Breakable")
        {          
            level.CountBlocks();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {

        if (tag == "Breakable")
        {
            HandleHit();
        }
   
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (tag == "Breakable")
        {
            HandleHit();
        }
    }


    private void HandleHit()
    {
        timesHit = timesHit + 1;
        int maxHits = hitSprites.Length + 1;
        if (timesHit >= maxHits)
        {
            DestroyBlock();
        }
        else
        {
            ShowNextHitSprite();
        }
    }

    private void ShowNextHitSprite()
    {
        int spriteIndex = timesHit - 1;
        if (hitSprites[spriteIndex] != null)
        {
            GetComponent<SpriteRenderer>().sprite = hitSprites[spriteIndex];
        }
        else
        {
            Debug.LogError("Block sprite is missing from array " + gameObject.name);
        }
    }

    public void FallingBlock()
    {
        randomBlock = Random.Range(1, 10);
        if(randomBlock == 2)
        {
            GameObject fallenBlock = Instantiate(objToSpawn[UnityEngine.Random.Range(
                0, objToSpawn.Length)], transform.position, transform.rotation);                       
        }      
    }

    

    private void DestroyBlock()
    {
        PlayBlockDestroySFX();
        Destroy(gameObject);
        level.BlockDestroyed();
        TriggerSparklesVFX();
        FallingBlock();
    }

    private void PlayBlockDestroySFX()
    {
        FindObjectOfType<GameSession>().AddToScore();
        AudioSource.PlayClipAtPoint(breakSound, Camera.main.transform.position);
    }

    private void TriggerSparklesVFX()
    {
        GameObject sparkles = Instantiate(blockSparklesVFX, transform.position, transform.rotation);
        Destroy(sparkles, 2f);
    }

    public void TriggerBlocks()
    { 
            GetComponent<Collider2D>().isTrigger = true;
    }

    

}
