using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Honey : MonoBehaviour
{

    public Door door;
    public Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(door.die == true)
        {
            StartCoroutine(EndHoney());
        }
    }
    IEnumerator EndHoney()
    {
        anim.SetBool("isDoing", true);
        yield return new WaitForSeconds(anim.GetCurrentAnimatorStateInfo(0).length);
        Destroy(gameObject, 3);
    }
}
