using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerLifeTextController : MonoBehaviour
{
    [SerializeField] private Text lifeText;
    // Start is called before the first frame update
    void Start()
    {
        lifeText.text = PlayerData.PlayerLife.ToString();
    }
}
