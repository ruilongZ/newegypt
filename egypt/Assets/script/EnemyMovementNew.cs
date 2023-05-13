using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class EnemyMovementNew : MonoBehaviour
{
    [Header("��������")]
    public float speed;
    public Vector3 movedir;
    public bool IsCloseCombat;
    public bool die;

    [Header("Զ�̹�������")]
    public float DistanceToAttack;
    public float currentspeed ;
    public float AttackCD;
    private float attackpasstime;
    public GameObject BatBullet;

    private CharacterController EnemyController;
    public float distanceToPlayer;
    GameObject player;
    private float passtime;
    public bool attacted=false;


    // Start is called before the first frame update
    void Start()
    {
        EnemyController = GetComponent<CharacterController>();
        player = GameObject.FindGameObjectWithTag("Player");
        currentspeed = speed;
    }

    // Update is called once per frame
    void Update()
    {
        distanceToPlayer = Vector3.Distance(player.transform.position, transform.position);
        movedir = (player.transform.position - transform.position).normalized;

        if (IsCloseCombat)
        {
       currentspeed = speed;
        }
        else {
            if (distanceToPlayer < DistanceToAttack)
            {
                currentspeed = 0;
                WaitToAttack();

            }
            else {
                currentspeed = speed;
            }
        }
        if (attacted) {
            Attacked();
        }
        if (die) {
            Die();
        }
        attackpasstime += Time.deltaTime;
        EnemyController.Move(movedir * currentspeed * Time.deltaTime);
    }

    public void Attacked() {
        if (passtime < 0.2)
        {
            passtime += Time.deltaTime;
            currentspeed = 0;
        }
        else {
            currentspeed = speed;
            passtime = 0;
            attacted = false;
        }
    }
    public void Die() {
        currentspeed = 0;
    }
    void WaitToAttack() {
        if (attackpasstime > AttackCD)
        {
            Debug.Log(1);
            GetComponentInChildren<BatAnimationControl>().Attack(); ;
            Instantiate(BatBullet,transform.position,Quaternion.identity);
            attackpasstime = 0;
        }
    }
}
