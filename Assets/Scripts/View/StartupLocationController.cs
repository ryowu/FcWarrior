using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartupLocationController : MonoBehaviour
{
    private GameObject player;
    private GameObject cameraMain;
    // Start is called before the first frame update
    void Start()
    {
        if (GlobalVars.UseCheckPoint)
        {
            //set flag to false to make this check point only works once
            GlobalVars.UseCheckPoint = false;

            //Restore player position and camera position
            player = GameObject.FindGameObjectWithTag("Player");
            cameraMain = GameObject.FindGameObjectWithTag("MainCamera");
            player.transform.position = transform.position;
            CameraController cc = cameraMain.GetComponent<CameraController>();
            cc.followTarget = player;
            cc.ResetToPosition(player.transform.position);

            //keep playing original bgm
            GameObject bgmobject = GameObject.FindGameObjectWithTag("bgmusic");
            bgmobject.GetComponent<AudioSource>().Play();
        }
    }
}
