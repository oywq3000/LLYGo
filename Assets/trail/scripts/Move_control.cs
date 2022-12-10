using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move_control : MonoBehaviour
{
    public Animator animator;

    public Rigidbody rigid_body;


    public float speed = 8.0F;

    private Vector3 moveDirection = Vector3.zero;
    private CharacterController controller;




    void Start()
    {
        this.controller = GetComponent<CharacterController>();
    }
    void Update()
    {
        float offset_vertical = Input.GetAxis("Vertical");
        float offset_horizontal = Input.GetAxis("Horizontal");


        if (offset_vertical == 0 && offset_horizontal == 0)
        {
            this.animator.SetBool("is_run", false);
            this.animator.SetBool("is_idle", true);

        }
        else
        {
            this.animator.SetBool("is_run", true);
            this.animator.SetBool("is_idle", false);




            this.moveDirection = new Vector3(-Input.GetAxis("Horizontal"), 0, -Input.GetAxis("Vertical"));

            //this.transform.TransformDirection(this.moveDirection.normalized);

            //this.moveDirection = transform.TransformDirection(this.moveDirection);


            this.moveDirection *= this.speed;
            //this.moveDirection.y -= gravity * Time.deltaTime;
            this.transform.position += this.moveDirection;

            transform.rotation = Quaternion.LookRotation(this.moveDirection);

        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            this.animator.SetBool("is_jump", true);
            this.rigid_body.AddForce(Vector3.up*220);
        }
        if (Input.GetKeyUp(KeyCode.Space))
        {
            this.animator.SetBool("is_jump", false);
        }


    }

}
