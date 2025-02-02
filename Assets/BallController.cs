using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallController : MonoBehaviour
{
    public bool moving = false;

    private float velocity;
    private float terminalVelocity;
    private Vector2 direction;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (moving) move();
    }

    void move()
    {

    }

}
