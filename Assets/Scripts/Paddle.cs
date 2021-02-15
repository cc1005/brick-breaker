using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paddle : MonoBehaviour
{
    // configuration parameters

    [SerializeField] float screenWidthInUnits;
    [SerializeField] float minMouseRange;
    [SerializeField] float maxMouseRange;

    // Cached references
    GameStatus theGameSession;
    Ball ball;

    // Start is called before the first frame update
    void Start()
    {
        theGameSession = FindObjectOfType<GameStatus>(); 
        ball = FindObjectOfType<Ball>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 paddlePosition = new Vector2(transform.position.x, transform.position.y);
        paddlePosition.x = Mathf.Clamp(GetXPos(), minMouseRange, maxMouseRange);
        transform.position = paddlePosition;
    }

    private float GetXPos()
    {
        if (theGameSession.IsAutoPlayEnabled())
        {
            return ball.transform.position.x;
        }
        else
        {
            return Input.mousePosition.x / Screen.width * screenWidthInUnits;
        }
    }
}
