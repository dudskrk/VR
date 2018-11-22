using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {
    
    private Player player;
    public Itembox[] itemBoxes;
    public Text textstring;
    int life;//주인공의 캐릭터 관리
    int snumber;//스테이지 관리
    int count;//겹친 아이템박스 수 관리
    float timer = 0;//클리어 and 죽었을대 다음탄으로 넘어가는 시간 변수
    public bool gameOver;// 죽었나 살았나
    
	// Use this for initialization
    public void giveDmg(int x) //enemy에서 공격을했으면 라이프를 깎는다.
    {
        life -= x;
    }
    public int getSnumb()
    {
        return snumber;
    }
	void Start () {
        life = 5;
        snumber = SceneManager.GetActiveScene().buildIndex;
        gameOver = false;
        
        textstring.text = "";
        player = GameObject.Find("Player").GetComponent<Player>();
    }
	void talkNext()
    {
        player.reset();
    }
    public int getLife()
    {
        return life;
    }
	// Update is called once per frame
	void Update () {
        count = 0;
        textstring.text = "HP = " + life;
        if(Input.GetMouseButton(1))
        {
            count = 6;
        }
        if (life <= 0)
        {
            gameOver = true;
            timer += Time.deltaTime;
            textstring.text = "죽었습니다!\n " + (5 - (int)timer) + " 초후 재시작합니다";

            if (timer >= 5)
            {
               
                Application.LoadLevel(snumber);//다시하기
                timer = 0;
            }
        }
        for (int i = 0; i < 6; i++)
        {
            if (itemBoxes[i].isOveraped == true)
            {
                count++;
            }
        }
        if (count >= 6)
        {
            timer += Time.deltaTime;
            textstring.text = "Stage" + snumber + "성공!\n " + (5 - (int)timer) + " 초후 \n다음 단계 시작합니다";


            if (timer >= 5)
            {
                talkNext();
                Application.LoadLevel(snumber + 1);//다음스테이지로
            }
        }
            //winui.SetActive(true);
    }
}
