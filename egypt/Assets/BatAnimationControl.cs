using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatAnimationControl : MonoBehaviour
{
    [Header("�������")]
    public float life;

    Animator batAnimator;
    // Start is called before the first frame update
    void Start()
    {
        batAnimator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        batAnimator.SetFloat("x", GetComponentInParent<EnemyMovementNew>().movedir.x * 1000);
        if (GetComponentInParent<EnemyMovementNew>().currentspeed == 0)
        {
            batAnimator.SetBool("move", false);
        }
        else {
            batAnimator.SetBool("move", true);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "playerbullet")
        {
            life -= other.GetComponentInParent<BulletMovementNew>().damage;
            GetComponentInParent<EnemyMovementNew>().attacted = true;
            batAnimator.SetTrigger("attacked");
            if (life <= 0)
            {
                batAnimator.SetTrigger("die");
                GetComponentInParent<EnemyMovementNew>().die = true;
                Destroy(GetComponentInParent<EnemyMovementNew>().gameObject, 0.7f);
            }
        }
    }
    public void Attack() {
        batAnimator.SetTrigger("attack");
    }
}
