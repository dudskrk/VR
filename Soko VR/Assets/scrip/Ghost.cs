using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ghost : MonoBehaviour
{
    public Rigidbody enemyRig;
    public Animator ani;
    private Player player;
    private GameManager gameManager;
    float speed = 1;//이동속도
    float r = 15;//적과 유령의 거리
    private float hp = 1f;//체력
    // Use this for initialization
    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        if (gameManager.getLife() > 0)
        {
            player = GameObject.Find("Player").GetComponent<Player>();
        }
        StartCoroutine("setRoutine");
    }

    IEnumerator setRoutine()
    {
        while (true)//초마다 1데미지 씩 줄수있습니다.
        {
            yield return new WaitForSeconds(0.5f);
            if ((hp > 0) && (gameManager.getLife() > 0))
                if (r < 1) gameManager.giveDmg(1);
        }
    }
    // Update is called once per frame
    void Update()
    {
        if ((hp > 0) && (gameManager.getLife() > 0)) //자신의 hp가 0이상,주인공캐릭터의 체력이 0이상일대만 이동한다.
        {
            ani.SetBool("find", true);
            float X = player.transform.position.x - transform.position.x;

            float Z = player.transform.position.z - transform.position.z;
             r = Mathf.Pow(Mathf.Pow(X, 2) + Mathf.Pow(Z, 2), (float)0.5);//주인공과 적과의 거리 측정
            Vector3 velocity = new Vector3(X / r * speed, 0, Z / r * speed);//유령의 속도
            transform.rotation = Quaternion.LookRotation(velocity);//가는방향으로 바라본다
            enemyRig.velocity = velocity;
            ani.SetFloat("distance", r);

            
        }
    }
    public void TakeDamage(float damage)//데미지 받았을때 처리함수
    {
        hp -= damage;
        if (hp <= 0)
        {
            ani.Play("monster_die");
            Destroy(gameObject, 1.2f);//죽음 애니메이션을 실행후 죽습니다.
        }
    }
}