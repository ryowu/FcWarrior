using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetCameraOffset : MonoBehaviour
{
    [SerializeField] private GameObject cameraMain;
    [SerializeField] private float xOffset;
    [SerializeField] private float yOffset;
    [SerializeField] private bool UpdateFollowedObject;
    [SerializeField] private GameObject NewFollowedObject;


    [SerializeField] private float NewBoundYBottom;

    // Start is called before the first frame update
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            CameraController cc = cameraMain.GetComponent<CameraController>();
            cc.xOffset = xOffset;
            cc.yOffset = yOffset;
            cc.boundYBottom = NewBoundYBottom;

            if (UpdateFollowedObject)
                cc.followTarget = NewFollowedObject;
        }
    }
}
