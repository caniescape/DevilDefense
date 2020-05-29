using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OpEnemyMove : MonoBehaviour
{
    public TextMesh hpText;
    float speed = 5.0f;
    public int hp;
    public int sp;
    bool isUp;
    bool isRight;
    bool isDown=true;
    bool trigger = true;
    //bool isAtk;
    OpEnemySpawner OpEs;


    private void Start()
    {
        OpEs = GameObject.Find("OpEnemySpawnPoint").GetComponent<OpEnemySpawner>();

        hpText = transform.GetChild(0).GetComponent<TextMesh>();
    }

    void FixedUpdate()
    {
        //hp업데이트
        hpText.text = hp.ToString();
        //방향 업데이트
        if (isDown)
        {
            transform.Translate(Vector3.down * Time.deltaTime * 0.5f);
        }
        if (isRight)
        {
            transform.Translate(Vector3.right * Time.deltaTime * 0.5f);
        }
        if (isUp)
        {
            transform.Translate(Vector3.up * Time.deltaTime * 0.5f);
        }
        if (!trigger)
        {
            StartCoroutine("Move");
            trigger = true;
        }
    }
    //wayPoint와 부딪혔을시
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.CompareTag("Finish"))
        {
            if (isDown && trigger)
            {
                isDown = false;
                isRight = true;
                trigger = false;
            }
            if (isRight && trigger)
            {
                isUp = true;
                isRight = false;
                trigger = false;
            }
            if (isUp && trigger)
            {
                GameManager.instance.OpDecreaseHp();
                trigger = false;
                isUp = false;
                //isAtk = true;
                OpEs.EnemyRemove(gameObject);
                gameObject.SetActive(false);
            }
        }
    }
    //방향 바꿔주는 코루틴
    IEnumerator Move()
    {
        yield return new WaitForSeconds(1.0f);
    }
    //hp감소시키는 함수
    public void DecreaseHp(int AtkUp)
    {
        //hp에서 미사일공격력뺌
        hp = hp - AtkUp;
        //Debug.Log("남은HP" + hp);
        //hp가 0보다 작거나 같을때
        if (hp <= 0)
        {
            //enemyfalse로 변경하고 리스트에서 제거하고 돈 증가
            OpEs.EnemyRemove(gameObject);
            gameObject.SetActive(false);
        }
    }
}
