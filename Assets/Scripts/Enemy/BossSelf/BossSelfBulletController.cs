using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossSelfBulletController : MonoBehaviour
{
    public Vector2 TargetPostion;
    public float MovingSpeed;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(TargetPostion, transform.position) > 0.1f)
        {
            transform.position = Vector2.MoveTowards(transform.position, TargetPostion, MovingSpeed * Time.deltaTime);
        }
        else
        {
            transform.position = TargetPostion;
            Destroy(this.gameObject);
        }
    }
}
