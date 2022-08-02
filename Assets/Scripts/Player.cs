using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    [SerializeField]
    private float m_Speed;
    
    [SerializeField]
    private float m_JumpForce;
    
    private Rigidbody2D m_RigidyBody;

    private Vector3 m_Movement;
    // Start is called before the first frame update
    void Awake()
    {
        m_RigidyBody = GetComponent<Rigidbody2D>();
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
       
        transform.position += movement * Time.deltaTime * m_Speed;
    }

    private void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Vector2 jumpDirection = new Vector2(0, m_JumpForce);
            m_RigidyBody.AddForce(jumpDirection, ForceMode2D.Impulse);
        }
    }

}
