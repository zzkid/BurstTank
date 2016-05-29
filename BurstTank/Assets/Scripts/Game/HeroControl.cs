using UnityEngine;
using System.Collections;

public class HeroControl : MonoBehaviour
{
    #region Properties
    GameObject Root;
    public float movespeed = 0.01f;
    public float bulletforce_X =2;
    public float bulletforce_Y =2;
    public GameObject Bullet;

    int hero_Diretion = -1;//1表示向右，-1表示向左
    #endregion

    #region UnityFunc
    void Start()
    {
        Root = GameObject.Find("Game");
    }

    void Update()
    {
        if(Input.GetKey(KeyCode.LeftArrow))
        {
            hero_Diretion = -1;
            transform.localScale = new Vector3(1, 1, 1);
            transform.position = new Vector3(transform.position.x - movespeed, transform.position.y, transform.position.z);
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            hero_Diretion = 1;
            transform.localScale = new Vector3(-1, 1, 1);
            transform.position = new Vector3(transform.position.x + movespeed, transform.position.y, transform.position.z);
        }

        if(Input.GetKeyDown(KeyCode.Space))
        {
            GameObject go = Instantiate(Bullet);
            go.transform.parent = Root.transform;
            go.transform.localScale = new Vector3(hero_Diretion, 1, 1);
            go.transform.position = new Vector3(transform.position.x,transform.position.y+1,transform.position.z);
            go.GetComponent<Rigidbody2D>().AddForce(new Vector2(bulletforce_X * hero_Diretion , bulletforce_Y));
            StartCoroutine(DestoryRocket(go));
        }
    }

    void FixUpdate()
    {

    }

    IEnumerator DestoryRocket(GameObject go)
    {
        yield return new WaitForSeconds(10);
        Destroy(go);
        yield return null;
    }
    #endregion

    #region MyFunc
    
    #endregion
}
