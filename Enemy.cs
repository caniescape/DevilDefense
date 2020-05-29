using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {
    
    float health=0.0f;

    public void SetUp(float health)
    {
        this.health = health;
    }
    //public void Instantiate(GameObject enemyPrefab)
    //{
        //enemy = enemyPrefab.GetComponent<Enemy>();

        //return enemy;
    //}
}
