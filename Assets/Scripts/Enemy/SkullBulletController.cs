using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkullBulletController : MonoBehaviour
{
    public float SkullBulletMovingSpeed;
    public Vector3 TargetPosition;

    // Start is called before the first frame update
    void Start()
    {
        //SkullBulletMovingSpeed = 8f;
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(TargetPosition, transform.position) > 0.1f)
        {
            transform.position = Vector3.MoveTowards(transform.position, TargetPosition, SkullBulletMovingSpeed * Time.deltaTime);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }
}
