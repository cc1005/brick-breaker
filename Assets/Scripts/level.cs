using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class level : MonoBehaviour
{
    // parameters
    [SerializeField] int noOfBreakableBlocks;  // Serialized for debugging purposes

    // cached reference
    SceneLoader nextScene;

    private void Start()
    {
        nextScene = FindObjectOfType<SceneLoader>();
    }

    public void CountBlocks()
    {
        noOfBreakableBlocks++;
    }

    public void RemoveBreakableBlock()
    {
        noOfBreakableBlocks--;
        if (noOfBreakableBlocks <= 0)
        {
            nextScene.LoadNextScene();
        }
    }

}
