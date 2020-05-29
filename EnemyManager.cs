using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    
    public static EnemyManager instance;
    //적보스 프리팹
    public GameObject e_bossPrefab = null;
    public Queue<GameObject> m_queue = new Queue<GameObject>();
    
    void Start()
    {
        instance = this;
        for(int i=0;i<50;i++)
        {
            GameObject e_object = Instantiate(e_bossPrefab, new Vector3(transform.position.x, transform.position.y), Quaternion.identity);
            m_queue.Enqueue(e_object);
            e_object.SetActive(false);
        }
    }
    public GameObject GetQueue()
    {
        GameObject e_object = m_queue.Dequeue();
        e_object.SetActive(true);
        return e_object;
    }

    public GameObject SearchQueue()
    {
        GameObject e_object = m_queue.Peek();
        return e_object;
    }
    
}
