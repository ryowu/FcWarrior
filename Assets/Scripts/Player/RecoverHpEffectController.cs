using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecoverHpEffectController : MonoBehaviour
{
    private void OnRecoverEffectComplete()
    {
        Destroy(this.gameObject);
    }
}
