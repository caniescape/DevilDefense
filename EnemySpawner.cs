using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemy1Prefab;
    public GameObject enemy2Prefab;
    public GameObject enemy3Prefab;
    public GameObject enemy4Prefab;
    public GameObject enemy5Prefab;
    public GameObject wave1;
    public GameObject wave2;
    public GameObject wave3;
    public GameObject wave4;
    public GameObject wave5;
    private int orderNum;
    public Transform spawnPoint;
    
    public List<GameObject> enemies = new List<GameObject>();
    
    private int wave=1;   //웨이브숫자
    private bool waveClear;   //웨이브클리어
    
    void FixedUpdate()
    {
        if (!waveClear&&wave==1)
        {
            //IsWave1 = false;
            StartCoroutine("Wave1");
            waveClear = true;
        }
        if (!waveClear && wave == 2)
        {
            StartCoroutine("Wave2");
            waveClear = true;
        }

        if (!waveClear && wave == 3)
        {
            StartCoroutine("Wave3");
            waveClear = true;
        }
        if (!waveClear && wave == 4)
        {
            StartCoroutine("Wave4");
            waveClear = true;
        }
        if (!waveClear && wave == 5)
        {
            StartCoroutine("Wave5");
            waveClear = true;
        }
        //웨이브 클리어 했을시
        if(waveClear&&enemies.Count==0)
        {
            wave++;
            orderNum = 0;
            GameManager.instance.ResetTime();
            waveClear = false;
        }
    }


    IEnumerator Wave1()
    {
        //Wave1 텍스트 on
        GameObject.Find("WaveText").transform.Find("WaveText1").gameObject.SetActive(true);
        //List enemies에 Wave1 오브젝트 생성
        Wave1Ins();
        //3초뒤에 Wave시작
        yield return new WaitForSeconds(3.0f);
        //Wave1 텍스트 off
        GameObject.Find("WaveText").transform.Find("WaveText1").gameObject.SetActive(false);
        //orderNum이 20이될때까지 반복
        while (orderNum < 20)
        {
            //0부터 2초마다 wave1하위순서대로 Active
            GameObject.Find("wave (1)").transform.GetChild(orderNum).gameObject.SetActive(true);
            orderNum++;
            yield return new WaitForSeconds(2.0f);
        }
        yield return null;
    }

    IEnumerator Wave2()
    {
        GameObject.Find("WaveText").transform.Find("WaveText2").gameObject.SetActive(true);
        Wave2Ins();
        //3초뒤에 Wave시작
        yield return new WaitForSeconds(3.0f);
        GameObject.Find("WaveText").transform.Find("WaveText2").gameObject.SetActive(false);
       
        //리스트에 적이 있다면 반복
        while (orderNum < 20)
        {
            GameObject.Find("wave (2)").transform.GetChild(orderNum).gameObject.SetActive(true);
            orderNum++;
            yield return new WaitForSeconds(2.0f);
        }
        yield return null;
    }

    IEnumerator Wave3()
    {
        GameObject.Find("WaveText").transform.Find("WaveText3").gameObject.SetActive(true);
        Wave3Ins();
        //3초뒤에 Wave시작
        yield return new WaitForSeconds(3.0f);
        GameObject.Find("WaveText").transform.Find("WaveText3").gameObject.SetActive(false);

        //리스트에 적이 있다면 반복
        while (orderNum < 20)
        {
            GameObject.Find("wave (3)").transform.GetChild(orderNum).gameObject.SetActive(true);
            orderNum++;
            yield return new WaitForSeconds(2.0f);
        }
        yield return new WaitUntil(() => enemies.Count == 0);
    }

    IEnumerator Wave4()
    {
        GameObject.Find("WaveText").transform.Find("WaveText4").gameObject.SetActive(true);
        Wave4Ins();
        //3초뒤에 Wave시작
        yield return new WaitForSeconds(3.0f);
        GameObject.Find("WaveText").transform.Find("WaveText4").gameObject.SetActive(false);

        //리스트에 적이 있다면 반복
        while (orderNum < 20)
        {
            GameObject.Find("wave (4)").transform.GetChild(orderNum).gameObject.SetActive(true);
            orderNum++;
            yield return new WaitForSeconds(2.0f);
        }
        yield return new WaitUntil(() => enemies.Count == 0);
    }

    IEnumerator Wave5()
    {
        GameObject.Find("WaveText").transform.Find("WaveText5").gameObject.SetActive(true);
        Wave5Ins();
        //3초뒤에 Wave시작
        yield return new WaitForSeconds(3.0f);
        GameObject.Find("WaveText").transform.Find("WaveText5").gameObject.SetActive(false);

        //리스트에 적이 있다면 반복
        while (orderNum < 20)
        {
            GameObject.Find("wave (5)").transform.GetChild(orderNum).gameObject.SetActive(true);
            orderNum++;
            yield return new WaitForSeconds(2.0f);
        }
        yield return new WaitUntil(() => enemies.Count == 0);
    }
    //private void SpawnWave()
    //{
    //    //웨이브 증가
    //    
    //    //웨이브*1.5에 반올림
    //    int spawnCount = 2;
    //        //Mathf.RoundToInt(wave * 1.5f);
    //    //spawnCount만큼 적을 생성
    //    //for(int i=0;i<spawnCount;i++)
    //    //{
    //        CreateEnemy();
    //    //}
    //}

    //private void CreateEnemy()
    //{
    //float health = 10;
    //적 프리팹으로부터 적 생성
    //GameObject enemy = Instantiate(enemy1Prefab, transform.position, Quaternion.identity);
    //enemy.SetActive(true);
    //생성한 적의 능력치와 추적대상 설정
    //enemy.GetComponent<Enemy>().SetUp(health);
    //생성된 적을 리스트에 추가
    //enemies.Add(enemy);
    //적의 onDeath이벤트에 익명 메서드 등록
    //사망한 적을 리스트에서 제거
    //enemy.onDeath += () => enemies.Remove(enemy);
    //사망한 적을 n초뒤에 파괴
    //enemy.onDeath += () =>
    //}

    void Wave1Ins()
    {
        //wave1오브젝트 생성 후 wave1 오브젝트 하위로 넣고 리스트에 넣어줌
        for (int i = 0; i < 20; i++)
        {
            GameObject e_object = Instantiate(enemy1Prefab, new Vector2(transform.position.x, transform.position.y), Quaternion.identity);
            e_object.transform.SetParent(wave1.transform);
            enemies.Add(e_object);
            e_object.SetActive(false);
        }
    }
    void Wave2Ins()
    {
        //wave2오브젝트 생성
        for (int i = 0; i < 20; i++)
        {
            GameObject e_object = Instantiate(enemy2Prefab, new Vector2(transform.position.x, transform.position.y), Quaternion.identity);
            e_object.transform.SetParent(wave2.transform);
            enemies.Add(e_object);
            e_object.SetActive(false);
        }
    }
    void Wave3Ins()
    {
        //wave3오브젝트 생성
        for (int i = 0; i < 20; i++)
        {
            GameObject e_object = Instantiate(enemy3Prefab, new Vector2(transform.position.x, transform.position.y), Quaternion.identity);
            e_object.transform.SetParent(wave3.transform);
            enemies.Add(e_object);
            e_object.SetActive(false);
        }
    }
    void Wave4Ins()
    {
        //wave4오브젝트 생성
        for (int i = 0; i < 20; i++)
        {
            GameObject e_object = Instantiate(enemy4Prefab, new Vector2(transform.position.x, transform.position.y), Quaternion.identity);
            e_object.transform.SetParent(wave4.transform);
            enemies.Add(e_object);
            e_object.SetActive(false);
        }
    }
    void Wave5Ins()
    {
        //wave5오브젝트 생성
        for (int i = 0; i < 5; i++)
        {
            GameObject e_object = Instantiate(enemy5Prefab, new Vector2(transform.position.x, transform.position.y), Quaternion.identity);
            e_object.transform.SetParent(wave5.transform);
            enemies.Add(e_object);
            e_object.SetActive(false);
        }
    }

    public List<GameObject> ReturnEnemyList()
    {
        return enemies;
    }

    public void EnemyRemove(GameObject enemy)
    {
        enemies.Remove(enemy);
    }
}
