using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class block : MonoBehaviour
{
    // config params
    [SerializeField] AudioClip breakingSound;
    [SerializeField] GameObject blockSparklesVFX;
    [SerializeField] Sprite[] hitSprites;

    // cached reference
    level Level;
    GameStatus gameStatus;

    // state variables
    [SerializeField] int timesHit; //only for degub process
    

    private void Start()
    {
        CountBreakableBlocks();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (tag == "Unbreakable")
        {
            GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
        }
        else if (tag == "Breakable")
        {
            HandleHit();
        }

    }

    private void HandleHit()
    {
        timesHit++;
        int blockLife = hitSprites.Length + 1;
        if (blockLife <= timesHit)
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
        if(hitSprites[spriteIndex] != null) 
        { 
        GetComponent<SpriteRenderer>().sprite = hitSprites[spriteIndex];
        }
        else
        {
            Debug.LogError("Block sprite is missing from array" + gameObject.name);
        }

    }
    private void DestroyBlock()
    {
        PlayBlockDestroySFX();
        Destroy(gameObject, 0);
        Level.RemoveBreakableBlock();
        TriggerSparklesVFX();
    }

    private void PlayBlockDestroySFX()
    {
        gameStatus = FindObjectOfType<GameStatus>();
        gameStatus.AddToScore();
        AudioSource.PlayClipAtPoint(breakingSound, Camera.main.transform.position);
    }

    private void TriggerSparklesVFX()
    {
        GameObject sparkles = Instantiate(blockSparklesVFX, transform.position, transform.rotation);
        Destroy(sparkles, 2);
    }

    private void CountBreakableBlocks()
    {
        Level = FindObjectOfType<level>();
        if (tag == "Breakable")
        {
            Level.CountBlocks();
        }
    }
}
