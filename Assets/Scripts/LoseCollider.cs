using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace DapperDino.Scoreboards
{
    public class LoseCollider : MonoBehaviour
    {
        Scoreboard scoreboard;
        ScoreboardEntryData scoreboardEntryData;
        [SerializeField] AudioClip loseSound;

        private void Awake()
        {
            scoreboard = FindObjectOfType<Scoreboard>();
        }
        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.tag == "Ball")
            {
                StartCoroutine(FindObjectOfType<SceneLoader>().LoseLevel());
                AudioSource.PlayClipAtPoint(loseSound, Camera.main.transform.position);      
            }

            if (collision.gameObject.tag == "SlowDown")
            {
                Destroy(collision.gameObject);
            }

            if (collision.gameObject.tag == "WidePaddle")
            {
                Destroy(collision.gameObject);
            }

            if (collision.gameObject.tag == "SmallPaddle")
            {
                Destroy(collision.gameObject);
            }

            if (collision.gameObject.tag == "SpeedUp")
            {
                Destroy(collision.gameObject);
            }

            if (collision.gameObject.tag == "Small Ball")
            {
                Destroy(collision.gameObject);
            }

            if (collision.gameObject.tag == "Through Square")
            {
                Destroy(collision.gameObject);
            }
        }

    }
}

