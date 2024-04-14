using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    public Animator anim;
    public bool die;
    public float speed;
    public Rigidbody2D rb;
    public SpriteRenderer spriteRenderer;

    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        die = false;
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (die)
        {
            spriteRenderer.flipX = false;
            anim.SetBool("isActive", true);
            rb.bodyType = RigidbodyType2D.Dynamic;
            rb.gravityScale = 1;
            transform.position += new Vector3(1, 0, 0).normalized * speed * Time.deltaTime;
            StartCoroutine(Died());

        }
    }
    IEnumerator Died()
    {
        yield return new WaitForSeconds(anim.GetCurrentAnimatorStateInfo(0).length);
        Destroy(gameObject, 5);
    }
}
