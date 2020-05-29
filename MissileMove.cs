using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissileMove : MonoBehaviour
{
    //EnemySpawner es;
    //public GameObject pEnemy;
    //Rigidbody2D m_rigid = null;
    //List<GameObject> enemyTargetList;
    //Transform enemyTarget = null;
    //[SerializeField] float m_speed = 5f;
    //float m_currentSpeed = 0f;
    ////원하는 레이어만 검출
    //public LayerMask m_layerMask = 1<<8;
    //[SerializeField] ParticleSystem m_psEffect = null;
    //GameObject enemyColObj1;
    //GameObject enemyColObj2;
    //GameObject enemyColObj3;
    ////미사일공격력
    public int atkUp;
    //====================================================================
    public GameObject target = null;
    public Vector2 targetPosition = Vector2.zero;
    public GameObject ExplosionParticle = null;
    //====================================================================
    //void SearchEnemy()
    //{
    //    Collider2D[] enemyCol1 =Physics2D.OverlapBoxAll(enemyColObj1.transform.position, 3400.0f, m_layerMask);
    //    Collider2D[] enemyCol2 =Physics2D.OverlapCircleAll(enemyColObj2.transform.position, 4000.0f, m_layerMask);
    //    Collider2D[] enemyCol3 =Physics2D.OverlapCircleAll(enemyColObj3.transform.position, 3400.0f, m_layerMask);
    //    ////원 안에서 검출
    //    Debug.Log(enemyCol1.Length);
    //    //첫번째 충돌체 배열에 있다면
    //    if(enemyCol1.Length>0)
    //    {
    //        //0번째 오브젝트가 타겟
    //        enemyTarget = enemyCol1[0].transform;
    //    }
    //    //두번째 충돌체 배열에 있다면
    //    else if(enemyCol2.Length>0)
    //    {
    //        //0번째 오브젝트가 타겟
    //        enemyTarget = enemyCol2[0].transform;
    //    }
    //    else if(enemyCol3.Length>0)
    //    {
    //        //0번째 오브젝트가 타겟
    //        enemyTarget = enemyCol3[0].transform;
    //    }
    //    //없다면
    //    else
    //    {
    //        //큐에 다시 넣어줌
    //        ObjectPoolingManager.instance.InsertQueue(this.gameObject);
    //        //위치와 회전값 초기화
    //        this.gameObject.transform.Translate(Vector3.zero);
    //        this.gameObject.transform.rotation = Quaternion.Euler(0, 0, 0);
    //    }
    //    //enemyTarget = es.FindEnemy().transform;
    //    if (enemyTargetList.Count > 0)
    //    {
    //        enemyTarget = enemyTargetList[0].transform;
    //    }
    //}
    
    //public void AddEnemy(GameObject enemy)
    //{
    //    Debug.Log("addenemy2");
    //    m_tfTargetList.Add(enemy);
    //}

    //IEnumerator LaunchDelay()
    //{
    //    //yield return new WaitUntil(() => m_rigid.velocity.y < 0f);
    //    yield return new WaitForSeconds(0.1f);
    //    SearchEnemy();
    //    //yield return new WaitUntil(()=> enemyTarget != null);
    //}
    //public float speed = 10.0f;
    //Vector3 pEnemyPos;
    void Start()
    {
        //enemyColObj1 = GameObject.Find("EnemyCol").transform.GetChild(0).gameObject;
        //enemyColObj2 = GameObject.Find("EnemyCol").transform.GetChild(1).gameObject;
        //enemyColObj3 = GameObject.Find("EnemyCol").transform.GetChild(2).gameObject;
        //m_rigid = GetComponent<Rigidbody2D>();
        //StartCoroutine(LaunchDelay());
    }
    
    void FixedUpdate()
    {
        transform.Translate(targetPosition * Time.deltaTime * 8.0f);

        //타겟 오브젝트가 사라지면 insertQueue
        if(!target.activeInHierarchy)
        {
            transform.GetComponentInParent<ObjectPoolingManager>().InsertQueue(this.gameObject);
        }
        //표적이 null값이 아니면
        //if(enemyTarget!=null)
        //{
        //    //Debug.Log("다시셋"+enemies[0]);
        //    //현재속도가 정해진 속도 보다 작으면
        //    if (m_currentSpeed <= m_speed)
        //        m_currentSpeed += m_speed * Time.deltaTime;
        //    m_currentSpeed = m_speed;
        //    transform.position += transform.up * m_currentSpeed * Time.deltaTime;
        //    //표적과 자신의 거리
        //    Vector3 t_dir = (enemyTarget.position - transform.position).normalized;
        //    
        //    transform.up = Vector3.Lerp(transform.up, t_dir, 0.25f);
        //}
    }

    //미사일 충돌시 비활성화
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //플레이어의 적과 충돌했을시
        if (collision.transform.CompareTag("PEnemy"))
        {
            //enemy의 hp감소함수 실행
            //Debug.Log("미사일 공격력:" + atkUp);
            collision.GetComponent<EnemyMove>().DecreaseHp(atkUp);
            
            //위치와 회전값 초기화
            //this.gameObject.transform.Translate(Vector3.zero);
            //this.gameObject.transform.rotation = Quaternion.Euler(0, 0, 0);

            //큐에 다시 넣어줌
            gameObject.GetComponentInParent<ObjectPoolingManager>().InsertQueue(this.gameObject);
        }
    }
}
