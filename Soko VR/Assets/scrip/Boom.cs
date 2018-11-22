using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boom : MonoBehaviour {
    public LayerMask whatIsProp;//적 레이마스트 ground
    public ParticleSystem explosionParticle;
     public AudioSource explosionAudio;
    
    private float explosionforce = 2500;//폭발 힘
    
    private float explosionRadius = 2f;//폭발 반경

    void Start()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, explosionRadius, whatIsProp);//위치,원의 반지름,필터링
        for (int i = 0; i < colliders.Length; i++)
        {
            Rigidbody targetRigidbody = colliders[i].GetComponent<Rigidbody>();
            targetRigidbody.AddExplosionForce(explosionforce, transform.position, explosionRadius);//반경내의 몬스터에게 폭발에너지 부여
            Enemy targetPro = colliders[i].GetComponent<Enemy>();
            targetPro.TakeDamage(explosionforce);//반경내 몬스터에게 데미지 부여
        }
        explosionParticle.transform.parent = null;
        explosionAudio.transform.parent = null;//폭탄이 먼저 파괴되기 때문에 자식상태에서 벗어납니다.
        explosionParticle.Play();
        explosionAudio.Play();
        Destroy(explosionParticle, 2f);//러닝타임
        Destroy(explosionAudio, 2f);
        Destroy(gameObject);//폭탄 제거
    }
}
