using UnityEngine;

public class Judgment : MonoBehaviour
{
    //private Vector2 startPos;
    //private Vectore3 exitVelocity;
    //public float swipeThreshold = 35f;
    //public float speed = 5f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //if (Input.touchCount > 0)
        //{
            //Touch t = Input.GetTouch(0);
            //if (t.phase == TouchPhase.Began) startPos = t position;
            //if (t.phase == TouchPhase.Ended) ResolveSwipe(t.position);
        //}
        //transform.Translate(exitVelocity * Time.deltaTime);
    }

    //void ResolveSwipe(Vector2 endPos)
    //{
        //float xDist = endPos.x - startPos.x;
        //if (Mathf.Abs(xDist) > swipeThreshold)
        //{
            //float dir =  xDist > 0 ? 1 : -1;
            //exitVelocity  = new Vector3(dir, dir, 0) * speed;
            //Destroy(gameObject, 2f);
        //}
    //}
}
