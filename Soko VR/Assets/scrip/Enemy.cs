using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class Enemy : MonoBehaviour
{
    public Rigidbody enemyRig;
    public Animator ani;
    private Player player;
    private NavMeshAgent agent;
    private GameManager gameManager;
    //public GameManager gameManager;
    float speed = 2;
    float r = 10;
    private float hp = 1f;
    // Use this for initialization
    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        if (gameManager.getLife() > 0){
            player = GameObject.Find("Player").GetComponent<Player>();
        }
        
        agent = GetComponent<NavMeshAgent>();

        StartCoroutine("setRoutine");
    }
      
IEnumerator setRoutine()//0.5초마다 데미지를 줄수있다.
{
    while (true)
    {
        yield return new WaitForSeconds(0.5f);
            if ((hp > 0) && (gameManager.getLife() > 0))
                if (r <= 1) gameManager.giveDmg(1);
        }
}
// Update is called once per frame
void Update()
    {
        if ((hp > 0)&&(gameManager.getLife()>0))
        {
            ani.SetBool("find", true);
            float X = player.transform.position.x - transform.position.x;
            float Z = player.transform.position.z - transform.position.z;
           r = Mathf.Pow(Mathf.Pow(X, 2) + Mathf.Pow(Z, 2), (float)0.5);//적과 주인공의 거리를 측작
            ani.SetFloat("distance", r);
            ani.SetFloat("hp", hp);
            agent.SetDestination(player.transform.position);
           
        }
    }
    public void TakeDamage(float damage)
    {
        hp -= damage;
        if (hp <= 0)
        {
            ani.Play("monster_die");
            Destroy(gameObject, 1.2f);
        }
    }
}