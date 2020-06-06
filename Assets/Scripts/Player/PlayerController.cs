using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Animator animator;
    public SpriteRenderer spriteRenderer;
    public float walkSpeed = 2.0f;
    public float leftBound = -6.5f;
    public float rightBound = 16.5f;
    public InteractionController interactionController;
    private int _animatorIsWalking;
    
    // Start is called before the first frame update
    private void Start()
    {
        this._animatorIsWalking = Animator.StringToHash("isWalking");
    }

    // Update is called once per frame
    private void Update()
    {
        if (!this.interactionController.interactionActive)
        {
            if (Input.GetKeyDown(KeyCode.W))
            {
                this.interactionController.activate();
            }
            else if (Input.GetKey(KeyCode.D) && transform.position.x < this.rightBound)
            {
                animator.SetBool(this._animatorIsWalking, true);
                this.spriteRenderer.flipX = false;
                transform.Translate(Time.deltaTime * walkSpeed * Vector3.right);
            }
            else if (Input.GetKey(KeyCode.A)  && transform.position.x > this.leftBound)
            {
                animator.SetBool(this._animatorIsWalking, true);
                this.spriteRenderer.flipX = true;
                transform.Translate(Time.deltaTime * walkSpeed * Vector3.left);
            }
            else
            {
                animator.SetBool(this._animatorIsWalking, false);
            }
        }
        else
        {
            animator.SetBool(this._animatorIsWalking, false);
        }
    }
}
