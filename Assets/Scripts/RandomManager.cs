using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum EnemyType
{
    MoveSpider,
    Bat,
    Ball,

}


public class RandomManager : MonoBehaviour {

    public Transform RandomPosition;

    //判断这个位置是否可用
    bool RandomEnemy(Vector3 RandomPos)
    {
        RaycastHit2D hit1 = Physics2D.Raycast(RandomPos, Vector2.up, 9f, 1 << LayerMask.NameToLayer("Obstacle"));
        if(hit1 && hit1.collider.gameObject.name == "UpCollider")
        {
            hit1 = Physics2D.Raycast(RandomPos, Vector2.down, 9f, 1 << LayerMask.NameToLayer("Obstacle"));
            if(hit1 && hit1.collider.gameObject.name == "DownCollider")
            {
                if(RandomPos.y>=0)
                {
                    RandomPosition.position = new Vector3(9, RandomPos.y - 1,0);
                }
                else
                {
                    RandomPosition.position = new Vector3(9, RandomPos.y + 1 ,0);

                }
                return true;
            }
        }
        return false;
    }

	// Use this for initialization
	void Start ()
    {
        do
        {
            float y = Random.Range(-4f, 4f);
            RandomPosition.position = new Vector3(9, y, 0);
        } while (!RandomEnemy(RandomPosition.position));
        GameObject prefab = Resources.Load<GameObject>(((EnemyType)Random.Range(0, 3)).ToString());
        Instantiate(prefab, RandomPosition.position, new Quaternion());
        timer = 0;
        mTimer = Random.Range(minTimer, maxTimer);
    }

    public float maxTimer;
    public float minTimer;
    float mTimer;
    float timer;
	// Update is called once per frame
	void FixedUpdate () {
        timer += Time.deltaTime;
        if(timer >= mTimer)
        {
            do
            {
                float y = Random.Range(-4f, 4f);
                RandomPosition.position = new Vector3(9, y, 0);
            } while (!RandomEnemy(RandomPosition.position));
            GameObject prefab = Resources.Load<GameObject>(((EnemyType)Random.Range(0, 3)).ToString());
            Instantiate(prefab, RandomPosition.position, new Quaternion());
            timer = 0;
            mTimer = Random.Range(minTimer, maxTimer);           
        }
	}
}
