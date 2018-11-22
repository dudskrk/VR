using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour {
    private float speed = 7;
    private bool move = false;
    // public GameManager gameManager;
    // Use this for initialization
    public AudioSource shootingAudio;
    private bool die = false;//죽음삭태 변수
    public Animator ani;
    public Rigidbody playerRigidbody;
    public GameManager gameManager;
    public Transform pos;
    public Rigidbody Boom;
    
    private bool fired= false;
    float inputX = 0;
    float inputZ = 0;
  
    void Start () {
        StartCoroutine("setRoutine");
        DontDestroyOnLoad(this);
        
       
    }
    IEnumerator setRoutine()//총쏘는데 장전시간은 최소 1초
    {
        while (true)
        {
            
            yield return new WaitForSeconds(1f);
            firedChange();
        }
    }
    IEnumerator iMdie()//죽었을때 다시하기 위한 설정
    {
        while (true)
        {
            Debug.Log("후랄라");
            yield return new WaitForSeconds(5f);
            die = false;
            ani.SetBool("die", die);
            gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();//게임매니져 다시 찾음
            if (gameManager.gameOver)
            {
                if (gameManager.getSnumb() == 2)//스테이2,3 시작위치
                {
                    transform.position = new Vector3(9.1f, -0.40f, 1f);
                }

                else if (gameManager.getSnumb() == 3)
                    transform.position = new Vector3(11.1f, -0.40f, 1f);
            }
        }
    }
    IEnumerator findGameM()//다음 스테이지 넘어갈때의 설정
    {
        Debug.Log("차차차찿");
        yield return new WaitForSeconds(0.1f);
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        if (gameManager.getSnumb() == 2)
        {
            transform.position = new Vector3(9.1f, -0.40f, 1f);
        }
        else if (gameManager.getSnumb() == 3)
            transform.position = new Vector3(11.1f, -0.40f, 1f);
        
    }

    // Update is called once per frame
    void Update () {
        inputX = 0;
        inputZ = 0;
      
        if (Input.GetMouseButton(0))//시연을 위해 마우스로  쏘기로 했습니다.
        {
            Fire();
        }
        if (die != true)
        {
           inputX = Input.GetAxis("Horizontal");
            inputZ = Input.GetAxis("Vertical");
        }
        ani.SetBool("move", move);
        
        
        Vector3 velocity = new Vector3(inputX, 0, inputZ);//방향키 받은쪽으로 속력을 준다
        velocity *= speed;
                   
        playerRigidbody.velocity = velocity;

        if ((inputX == 0) && (inputZ == 0))
            move = false;
        else
        {
            move = true;
            transform.rotation = Quaternion.LookRotation(velocity);//가는방향으로 바라보게하는 메서드
        }
        
        if (gameManager.gameOver == true)
        {
            if ((die == false))
            {
                
                StartCoroutine("iMdie");
            }
            die = true;
          
            ani.SetBool("die", die);
            ani.Play("m_weapon_death_A");//죽음애니메이션
          

            if (gameManager.getSnumb() == 1)
                Destroy(gameObject, 4.5f);//1단계의 경우만 죽었으면 객체르 파괴한다
            
        }
            
        
    }
    public void reset()//스테이 넘어가면서 세팅 변화 함수
    {
        StartCoroutine("findGameM");
        

    }
    private void Fire()
    {
        if ((fired == false)&&(die==false))
        {
            ani.SetTrigger("shoot");
            fired = true;
            Rigidbody boomInstance = Instantiate(Boom, pos.position, gameObject.transform.rotation);//폭탄 생성
                       
            boomInstance.velocity =10*pos.forward;//플레엉 전방으로 발싸
       
            shootingAudio.Play();    //발사소리
        }
    }
    public void firedChange()//쏘지 않은상태로 변경
    {
        fired = false;
    }
}
