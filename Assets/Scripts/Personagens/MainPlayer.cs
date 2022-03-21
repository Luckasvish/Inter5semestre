using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainPlayer : MonoBehaviour
{
    CharacterController controller;
    public float speed;
    public bool canMove;

    [SerializeField]
    internal CollisionsManager collisions;
    
    [SerializeField]
    internal Chef chef;

    void Start()
    {
        controller = GetComponent<CharacterController>();
    }
    void Update()
    {
        if(canMove)
        {
            Move();
        }
    }

    void Move()
    {
        Vector3 direction = new Vector3(Input.GetAxis("Horizontal"),0,Input.GetAxis("Vertical"));

        controller.Move(direction * speed * Time.deltaTime );


    }
}
