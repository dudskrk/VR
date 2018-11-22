using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemBox : MonoBehaviour {
    public bool isOveraped = false;
    private Renderer myRenderer;
    private Color OriginColor;
    public Color TouchColor;
    // Use this for initialization
    void Start()
    {
        myRenderer = GetComponent<Renderer>();
        OriginColor = myRenderer.material.color;
    }

    // Update is called once per frame
    void Update()
    {

    }
    void OnTriggerEnter(Collider other)//트리거인 콜리더와 충돌했을때
    {
        if (other.tag == "wetag")
        {
            myRenderer.material.color = TouchColor;
            isOveraped = true;
        }

    }
    void OnTriggerExit(Collider other)//충돌이 끝났을때
    {
        myRenderer.material.color = OriginColor;
        isOveraped = false;
    }
    void OnTriggerStay(Collider other)//충돌되어있는 동안
    {
        if (other.tag == "wetag")
        {
            myRenderer.material.color = TouchColor;
            isOveraped = true;
        }
    }
    void OnCollisionEnter(Collision ohter)//일반 콜리더와 충돌했을때
    {
        
    }
}
