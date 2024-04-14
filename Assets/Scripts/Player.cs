using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteAlways]
public class Player : MonoBehaviour
{
    [Header("Movimiento")]
    public float speed;
    public float jumpForce;

    [Header("Deteccion de suelo")]
    [Range(0, 2)]
    public float raycastDistance;
    public LayerMask layerMask;
    public bool grounded;

    public Door door;
    public bool hasKey;
    float counter;
    public Transform respawn;


    public Vector2[] abajo;
    public Vector2[] arriba;
    public Vector2[] derecha;
    public Vector2[] izquierda;

    public Animator animator;

    float horizontal;
    float jump;
    bool jumping;

     public Rigidbody2D rb;

   
    public SpriteRenderer spriteRenderer;

    public GemManager gems;
    public bool win;
    public bool takingDamage;
    public Hearts hts;
    public AudioSource jumpSound;

    private void Start()
    {
        transform.position = respawn.position;
        spriteRenderer  = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
        takingDamage = false;
         hts = GetComponent<Hearts>();

    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && grounded)
        {
            Salto();
        }
        if (grounded)
        {
            animator.SetBool("isJumping", false);
            animator.SetBool("isFalling", false);
            jumping = false;
        }
        if(!grounded && rb.velocity.y > 0)
        {
            animator.SetBool("isJumping", true);
            jumping = true;
            
        }
        if(!grounded && rb.velocity.y < 0 && jumping == false)
        {
            animator.SetBool("isFalling", true);
        }
        counter += Time.deltaTime;
        
    }
    [ExecuteAlways]
    private void FixedUpdate()
    {
        //Player movement
        transform.position += new Vector3(Input.GetAxis("Horizontal") * speed * Time.fixedDeltaTime,0, 0);

        //Animación Horizontal
        horizontal = Input.GetAxisRaw("Horizontal") * speed;
        animator.SetFloat("speed", Mathf.Abs(horizontal));

        //SPRITE FLIP 
        if (horizontal < 0)
        {
            spriteRenderer.flipX = true;
        }
        if (horizontal > 0)
        {
            spriteRenderer.flipX = false;
        }
        

        grounded = false;
        
        foreach (Vector2 p in abajo) 
        {
            RaycastHit2D hit = Physics2D.Raycast(transform.position + (Vector3)p, -Vector2.up, raycastDistance, layerMask);//ABAJO //Depende del mundo para mirar el up                                                                                                
            //ABAJO                                                                                                        //RaycastHit2D hit = Physics2D.Raycast(transform.position, -transform.up); Este depende de la rotacion del objeto
            Debug.DrawRay(transform.position + (Vector3)p, -Vector2.up * raycastDistance, Color.red);
            Debug.DrawRay(transform.position + (Vector3)p, -Vector2.up * hit.distance, Color.green);
          
            //Ground collision
            if (hit.collider != null)
            {
                grounded = true;
            }
            //Parent restart
            if(hit.collider == null)
            {
                this.transform.parent = null;
            }
            if(hit.collider != null && hit.collider.tag == ("PlatformMov"))
            {
                this.transform.parent = hit.transform;
            }
            if (hit.collider != null && hit.collider.tag == ("suelo"))
            {
                this.transform.parent = null;
            }
            if (hit.collider != null && hit.collider.tag == ("WeakPoint"))
            {
                Enemy enemigo = hit.collider.GetComponentInParent<Enemy>();

                enemigo.StartCoroutine(enemigo.DestroyAfterAnimation());

                Salto();
            }
            

        }
        
        foreach(Vector2 p in derecha)
        {
            RaycastHit2D hit4 = Physics2D.Raycast(transform.position + (Vector3)p, Vector2.left, raycastDistance, layerMask);

            //DERECHA
            Debug.DrawRay(transform.position + (Vector3)p, Vector2.left * raycastDistance, Color.red);
            Debug.DrawRay(transform.position + (Vector3)p, Vector2.left * hit4.distance, Color.green);
            if (hit4.collider != null && hit4.collider.tag == ("Enemy"))
            {
                rb.AddForce(Vector2.right * 2f, ForceMode2D.Impulse);
                animator.SetBool("damaged", true);
                StartCoroutine(DestroyAfterAnimation());
                takingDamage = true;
                if (takingDamage && counter > 2)
                {
                    hts.health -= 1;
                    takingDamage = false;
                    counter = 0;
                }
            }

        }
        foreach(Vector2 p in izquierda)
        {
            RaycastHit2D hit3 = Physics2D.Raycast(transform.position + (Vector3)p, -Vector2.left, raycastDistance, layerMask);
            //IZQUIERDA
            Debug.DrawRay(transform.position + (Vector3)p, Vector2.right * raycastDistance, Color.red);
            Debug.DrawRay(transform.position + (Vector3)p, Vector2.right * hit3.distance, Color.green);
            if (hit3.collider != null && hit3.collider.tag == ("Enemy"))
            {
                rb.AddForce(Vector2.right * -2f, ForceMode2D.Impulse);
                animator.SetBool("damaged", true);
                StartCoroutine(DestroyAfterAnimation());
                takingDamage = true;
                if (takingDamage && counter > 2)
                {
                    
                    hts.health -= 1;
                    takingDamage = false;
                    counter = 0;
                }
            }
        }
        

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("key"))
        {
            hasKey = true;
        }
        if(collision.CompareTag("door") && hasKey)
        {
            door.die = true;
            
 
        }
        if (collision.CompareTag("gem"))
        {
            gems.gemCount++;
        }
        if (collision.CompareTag("FinalFlag"))
        {
            win = true;
        }

        
    }

    void Salto()
    {
        jumpSound.Play(0);
        rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
    }
    IEnumerator DestroyAfterAnimation()
    {

        yield return new WaitForSeconds(animator.GetCurrentAnimatorStateInfo(0).length);
        animator.SetBool("damaged", false);
    }

}
