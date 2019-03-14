using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveBall : MonoBehaviour
{

    public Rigidbody rb;
    private float moveSpeed = 1000f;
    private float jumpSpeed = 300f;
    private bool isGrounded = true;

    void Update()
    {
        BallMovement();
    }

    private void BallMovement()
    {
        if (Input.GetKey("w"))
        {
            Handler("w");
        }
        if (Input.GetKey("s"))
        {
            Handler("s");
        }
        if (Input.GetKey("a"))
        {
            Handler("a");
        }
        if (Input.GetKey("d"))
        {
            Handler("d");
        }
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            Handler("space");
        }
    }

    private void Handler(string val)
    {
        switch (val)
        {
            case "w":
                MoveForward("forward");
                break;
            case "s":
                MoveForward("back");
                break;
            case "a":
                MoveSide("left");
                break;
            case "d":
                MoveSide("right");
                break;
            case "space":
                MoveJump();
                break;
        }
    }

    private void MoveForward(string command)
    {
        switch (command)
        {
            case "forward":
                rb.AddForce(0, 0, -moveSpeed * Time.deltaTime);
                break;
            case "back":
                rb.AddForce(0, 0, moveSpeed * Time.deltaTime);
                break;
        }
    }

    private void MoveSide(string command)
    {
        switch (command)
        {
            case "right":
                rb.AddForce(-moveSpeed * Time.deltaTime, 0, 0);
                break;
            case "left":
                rb.AddForce(moveSpeed * Time.deltaTime, 0, 0);
                break;
        }
    }

    private void MoveJump()
    {
        rb.AddForce(0, jumpSpeed, 0);
        isGrounded = false;
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.tag == "ground")
        {
            isGrounded = true;
        }   
        if (collision.gameObject.tag == "obstacle")
        {
            isGrounded = true;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "ground")
        {
            isGrounded = false;
        }
    }

}
