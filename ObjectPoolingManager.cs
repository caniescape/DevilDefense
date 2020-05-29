using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPoolingManager : MonoBehaviour
{
    public static ObjectPoolingManager instance;

    //List<GameObject> enemyList = new List<GameObject>();
    
    private Queue<GameObject> m_queue = new Queue<GameObject>();
    int missileNum;
    private GameObject missilePrefab = null;
    public GameObject stormMissilePrefab = null;
    public GameObject lifeMissilePrefab = null;
    public GameObject fireMissilePrefab = null;
    public GameObject frostMissilePrefab = null;
    public GameObject arcaneMissilePrefab = null;
    public GameObject lightMissilePrefab = null;
    public GameObject shadowMissilePrefab = null;

    void Start()
    {
        missileNum = gameObject.GetComponent<CardInfo>().MissileNum;
        instance = this;
        //미사일 프리팹 생성
        switch(missileNum)
        {
            case 1:
                missilePrefab = stormMissilePrefab;
                break;
            case 2:
                missilePrefab = lifeMissilePrefab;
                break;
            case 3:
                missilePrefab = fireMissilePrefab;
                break;
            case 4:
                missilePrefab = frostMissilePrefab;
                break;
            case 5:
                missilePrefab = arcaneMissilePrefab;
                break;
            case 6:
                missilePrefab = lightMissilePrefab;
                break;
            case 7:
                missilePrefab = shadowMissilePrefab;
                break;
        }
        for (int i = 0; i < 20; i++)
        {
            GameObject t_object = Instantiate(missilePrefab, new Vector3(transform.position.x, transform.position.y), Quaternion.identity);
            t_object.transform.SetParent(transform);
            m_queue.Enqueue(t_object);
            t_object.SetActive(false);
        }
    }
    //큐에 삽입
    public void InsertQueue(GameObject p_object)
    {
        p_object.transform.position = this.gameObject.transform.position;
        m_queue.Enqueue(p_object);
        p_object.SetActive(false);
    }
    //큐에서 가져오기
    public GameObject GetQueue()
    {
        GameObject t_object = m_queue.Dequeue();
        t_object.SetActive(true);
        return t_object;
    }
    //큐의 첫번째 오브젝트 찾기
    public GameObject SearchQueue()
    {
        GameObject t_object = m_queue.Peek();
        return t_object;
    }
}
