using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Luna_controller : MonoBehaviour
{
    // Start is called before the first frame update
    float vertical;
    float horizontal;
    public float speed = 1;
    public static Luna_controller Instance;
    Rigidbody2D rigidbody2d;
    public Animator animator;
    Vector2 lookDirection = new Vector2(0, -1);

    public AudioClip petDog;
    private void Awake()
    {
        Instance = this;
    }
    void Start()
    {
        
        rigidbody2d = GetComponent<Rigidbody2D>();
        
    }

    // Update is called once per frame
    void Update()
    {
        
        vertical = Input.GetAxisRaw("Vertical");
        horizontal = Input.GetAxisRaw("Horizontal");
        if (GameManager.Instance.battlescene.active == true)
        {
            return;
        }
        
        
        Vector2 move = new Vector2(horizontal, vertical);
        animator.SetFloat("Moving", 0);
        if (!Mathf.Approximately(move.x,0) || !Mathf.Approximately(move.y, 0))
        {
            lookDirection.Set(move.x, move.y);
            lookDirection.Normalize();
            animator.SetFloat("Moving", move.magnitude);
        }
        animator.SetFloat("LookX", lookDirection.x);
        animator.SetFloat("LookY", lookDirection.y);

        if(move.magnitude > 0)
        {
          if(Input.GetKey(KeyCode.LeftShift))
            {
                animator.SetFloat("MoveScale", 1);
            }
            else
            {
                animator.SetFloat("MoveScale", 0);
            }
        }

        if(Input.GetKeyDown(KeyCode.Space))
        {
            Talk();
        }

    }

    private void FixedUpdate()
    {
        Vector2 position = transform.position;
        position.x += speed * horizontal * Time.fixedDeltaTime;
        position.y += speed * vertical * Time.fixedDeltaTime;
        rigidbody2d.MovePosition(position);
    }
    
    public void Jump(bool start)
    {
        animator.SetBool("Jump", start);
        rigidbody2d.simulated = !start;
    }

    public void Climb(bool start)
    {
        animator.SetBool("Climb", start);
    }

    public void Talk()
    {
        Collider2D collider = Physics2D.OverlapCircle(rigidbody2d.position,
            0.5f, LayerMask.GetMask("NPC"));
        if(collider != null)
        {
            if(collider.name == "Nala")
            {
                GameManager.Instance.canControlLuna = false;
                collider.GetComponent<NPCDialog>().DisplayDialog();
            }
            else if(collider.name == "Dog")
            {
                GameManager.Instance.PlayShot(petDog);
                GameManager.Instance.hasPetTheDog = true;
                transform.position = new Vector3(-7.69f, -5.78f,0);
                animator.CrossFade("PetDogA",0);
                GameObject temp =  GameObject.Find("Nala");
                if(temp.GetComponent<NPCDialog>().dialogInfoIndex == 2)
                {
                    temp.GetComponent<NPCDialog>().dialogInfoIndex++;
                }
                collider.GetComponent<Dog>().StarGo();
            }
        }

    }
}
