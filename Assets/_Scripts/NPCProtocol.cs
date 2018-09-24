using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCProtocol : MonoBehaviour {
    private Animator animator;
    private PostmanStateHandler postmanState;
    private PostmanMoveScript postmanMoves;
    private FollowPostman camera;
    public int state = 0;
    public float speed;
    public float start;

    void Start()
    {
        GameObject postman = GameObject.FindGameObjectWithTag("Postman");
        animator = GetComponent<Animator>();
        postmanState = postman.GetComponent<PostmanStateHandler>();
        postmanMoves = postman.GetComponent<PostmanMoveScript>();
        camera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<FollowPostman>();
        start = transform.position.x;
    }
    void Update()
    {
        switch (state)
        {
            case 1:
                postmanMoves.TurnAround();
                state++;
                break;
            case 2:
                animator.SetBool("walking", true);
                transform.Translate(speed * Vector2.right);
                break;
            case 3:
                animator.SetBool("walking", false);
                animator.SetTrigger("handOver");
                Debug.Log("handing over");
                state = 4;
                break;
            case 4:
                Debug.Log("wait for it");
                break;
            case 5:
                animator.SetBool("walking", true);
                Turn();
                Debug.Log("Thanks and bye");
                state = 6;
                break;
            case 6:
                transform.Translate(-speed * Vector2.right);
                if(transform.position.x < start)
                {
                    postmanMoves.TurnAround();
                    postmanState.letsGo();
                    Object.Destroy(gameObject);
                }
                break;
            default:
                break;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Postman" && state == 0)
        {
            state = 1;
            postmanState.letsWait();    
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "Postman" && state < 3)
        {
            state = 3;
        }
    }
    private void Turn()
    {
        transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
    }

    public void HandedOver()
    {
        state = 5;
    }
}
