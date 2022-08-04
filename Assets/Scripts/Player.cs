using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    [SerializeField]
    private float m_Speed;
    
    [SerializeField]
    private float m_JumpForce;

    #region AuxVariables
    private bool m_IsJumping;

    private bool m_DoubleJump;

    private Vector3 m_Movement;

    private Vector2 m_JumpDirection;
    
    private Rigidbody2D m_RigidyBody;

    private Animator m_Animator;

    #endregion


    void Awake()
    {
        m_RigidyBody = GetComponent<Rigidbody2D>();
        m_Animator = GetComponent<Animator>();
        m_JumpDirection = new Vector2(0, m_JumpForce);
    }

    // Update is called once per frame
    void Update()
    {
        m_Movement = new Vector3(Input.GetAxis("Horizontal"), 0, 0);
        Jump();
    }

    private void FixedUpdate()
    {
        Move(m_Movement);   
    }

    private void Move(Vector3 movement)
    {
        if (movement.x > 0)
        {
            m_Animator.SetBool("Walk", true);
            transform.position += movement * Time.deltaTime * m_Speed;
            transform.eulerAngles = new Vector3(0,0,0);
        }

        if (movement.x < 0)
        {
            m_Animator.SetBool("Walk", true);
            transform.position += movement * Time.deltaTime * m_Speed;
            transform.eulerAngles = new Vector3(0, 180 , 0);
        }

        if (movement.x == 0)
        {
            m_Animator.SetBool("Walk", false);

        }

        
    }

    private void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            ExecuteJump();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == 8)
        {
            m_IsJumping = false;
            m_Animator.SetBool("Jump", false);
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.layer == 8)
        {
            m_IsJumping = true;
        }
    }

    private void ExecuteJump()
    {
        if (!m_IsJumping)
        {
            m_RigidyBody.AddForce(m_JumpDirection, ForceMode2D.Impulse);
            m_DoubleJump = true;
            m_Animator.SetBool("Jump", true);
        }
        else
        {
            if (m_DoubleJump)
            {
                m_RigidyBody.AddForce(m_JumpDirection, ForceMode2D.Impulse);
                m_DoubleJump =false;
            }
        }
    
    }




}
