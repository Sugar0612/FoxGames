using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControlloer : MonoBehaviour
{

    [SerializeField]private Rigidbody2D rb;
    private Animator anim;
    public Collider2D coll;

    public LayerMask ground;
    public float speed;
    public float jumpForce;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Movement();
        SwitchAnim();
    }

    void Movement()
    {
        float horizontal_dir = 0.0f;
        horizontal_dir = Input.GetAxis("Horizontal"); // value [1, -1]

        float facedir = 0.0f;
        facedir = Input.GetAxisRaw("Horizontal"); // value {1, 0, -1}

        //移动机制
        if(horizontal_dir != 0.0f)
        {
            /* Time.deltaTime 使在不同设备更加的平滑. */
            rb.velocity = new Vector2(horizontal_dir * speed * Time.deltaTime, rb.velocity.y);
            anim.SetFloat("speed", Mathf.Abs(facedir));
        }
        
        if(facedir != 0)
        {
            transform.localScale = new Vector3(facedir, 1, 1);
        }

        // 跳跃机制
        if(Input.GetButtonDown("Jump"))
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce * Time.deltaTime);
            anim.SetBool("is_jumping", true);
        }

    }

    void SwitchAnim() {
        anim.SetBool("is_idle", false);
       
        if(anim.GetBool("is_jumping"))
        {
            if(rb.velocity.y < 0)
            {
                anim.SetBool("is_jumping", false);
                anim.SetBool("is_falling", true);
            }
        }

        else if (coll.IsTouchingLayers(ground))
        {
            anim.SetBool("is_falling", false);
            anim.SetBool("is_idle", true);
        }
    }
}
