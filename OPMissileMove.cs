using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OPMissileMove : MonoBehaviour
{
   
    ////미사일공격력
    public int atkUp;
    //====================================================================
    public GameObject target = null;
    public Vector2 targetPosition = Vector2.zero;
    public GameObject ExplosionParticle = null;
    //====================================================================
    
    void FixedUpdate()
    {
        transform.Translate(targetPosition * Time.deltaTime * 8.0f);

        //타겟 오브젝트가 사라지면 insertQueue
        if (!target.activeInHierarchy)
        {
            transform.GetComponentInParent<ObjectPoolingManager>().InsertQueue(this.gameObject);
        }
        
    }

    //미사일 충돌시 비활성화
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //플레이어의 적과 충돌했을시
        if (collision.transform.CompareTag("OpEnemy"))
        {
            //enemy의 hp감소함수 실행
            //Debug.Log("미사일 공격력:" + atkUp);
            collision.GetComponent<OpEnemyMove>().DecreaseHp(atkUp);

            //위치와 회전값 초기화
            //this.gameObject.transform.Translate(Vector3.zero);
            //this.gameObject.transform.rotation = Quaternion.Euler(0, 0, 0);

            //큐에 다시 넣어줌
            gameObject.GetComponentInParent<ObjectPoolingManager>().InsertQueue(this.gameObject);
        }
    }
}
