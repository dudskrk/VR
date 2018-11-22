using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawnzone : MonoBehaviour
{
    private int ratio = 45;//유령비율
    public GameObject[] proPrefabs;
    private BoxCollider area;
    GameManager gameManager;
    int count = 3;
    int j;
    public float zenTime = 10f;
    float time = 0;
    private List<GameObject> props = new List<GameObject>();
    // Use this for initialization
    void Start()
    {
        
        area = GetComponent<BoxCollider>();
        area.enabled = false;//혹시 박스콜라이더가 방해할까봐 비화럿ㅇ화 합니다.
        StartCoroutine("setRoutine");
    }
    IEnumerator setRoutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(zenTime);
            Spawn();//젠타임마다 스폰하면 스테이지마다 그 초가 다릅니다.
        }
    }
    private void Spawn()
    {
        int selection; 
        j=Random.Range(0,ratio+1);
        if (j < ratio/3) selection = 0;//토끼,박쥐,슬라임,유령의 발생 비율 15:15:15:1
        else if ((j >= ratio/3) && (j < (ratio*2)/3)) selection = 1;
        else if ((j >= (ratio*2)/3) && (j < ratio)) selection = 2;
        else  selection = 3;

        GameObject selectedPrefab = proPrefabs[selection];
        Vector3 spawnPos = GetRandomPos();
        GameObject instance = Instantiate(selectedPrefab, spawnPos, Quaternion.identity);//몬스터 생성
        props.Add(instance);
    }
    private Vector3 GetRandomPos()
    {
        Vector3 basePos = transform.position;
        Vector3 size = area.size;//스폰존의 랜덤한 위치에서 생성
        float posX = basePos.x + Random.Range(-size.x / 2f, size.x / 2f);//
        float posY = 0;
        float posZ = basePos.z + Random.Range(-size.z / 2f, size.z / 2f);

        Vector3 spawnPos = new Vector3(posX, posY, posZ);
        return spawnPos;
    }
    

    // Update is called once per frame
    void Update()
    {
      
    }
}

