using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpMagicFire : MonoBehaviour
{
    public GameObject bullet = null;
    private GameObject closeEnemy = null;
    //적 리스트 선언
    private List<GameObject> collEnemys = new List<GameObject>();
    private float fTime = 0;
    OpEnemySpawner OpEs;
    void Start()
    {
        OpEs = GameObject.Find("OpEnemySpawnPoint").GetComponent<OpEnemySpawner>();
        StartCoroutine(missileFire());
    }

    void Update()
    {
        
    }
    //미사일 발사 코루틴
    IEnumerator missileFire()
    {
        //반복
        while (true)
        {
            //적리스트를 받아옴
            collEnemys = OpEs.ReturnEnemyList();
            //원 범위안에 적을 찾음
            //CircleCol();
            fTime += Time.deltaTime;
            //적리스트의 수가 0보다 클떄
            if (collEnemys.Count > 0)
            {
                //Debug.Log(collEnemys[0].name);
                //그중에 비활성화인것을 지움
                //for (int i = 0; i < collEnemys.Count; ++i)
                //{
                //    if (!collEnemys[i].activeInHierarchy)
                //    {
                //        collEnemys.Remove(collEnemys[i]);
                //    }
                //}
                //타겟지정
                GameObject target = collEnemys[0];
                if (target != null && fTime > 2f)
                {
                    fTime = 0.0f;
                    //오브젝트풀에서 가져온 미사일 오브젝트 레벨을 보냄
                    //var t_object = Instantiate(bullet, transform.position, Quaternion.identity, transform);
                    GameObject t_object = this.GetComponent<ObjectPoolingManager>().GetQueue();
                    //미사일 포지션은 카드중점으로 세팅
                    t_object.transform.position = this.transform.position;
                    Destroy(t_object.GetComponent<MissileMove>());
                    t_object.AddComponent<OPMissileMove>();
                    //미사일 공격력, 타겟 세팅
                    t_object.GetComponent<OPMissileMove>().target = target;
                    t_object.GetComponent<OPMissileMove>().atkUp = GetComponent<CardInfo>().AtkUp;
                    t_object.GetComponent<OPMissileMove>().targetPosition = (target.transform.position - transform.position).normalized;
                    //t_object.transform.localScale = new Vector3(0.5f, 0.5f);
                }
            }
            else
            {
                yield return new WaitUntil(() => collEnemys.Count > 0);
            }

            yield return new WaitForFixedUpdate();
        }
    }
    private void CircleCol()
    {
        Collider2D col = Physics2D.OverlapCircle(transform.position, 100.0f);
        if (col.tag == "OPEnemy")
        {
            collEnemys.Add(col.gameObject);
        }
    }
    //private void OnTriggerEnter2D(Collider2D collision)
    //{
    //    if(collision.tag == "PEnemy")
    //    {
    //        //Debug.Log("적추가"+collEnemys.Count);
    //        collEnemys.Add(collision.gameObject);
    //    }
    //}
    //private void OnTriggerExit2D(Collider2D collision)
    //{
    //    //collEnemys안의 go가 
    //    foreach(GameObject go in collEnemys)
    //    {
    //        if (!go.activeInHierarchy&&go == collision.gameObject)
    //        {
    //            Debug.Log("적지워짐");
    //            collEnemys.Remove(go);
    //            break;
    //        }
    //    }
    //}
}
