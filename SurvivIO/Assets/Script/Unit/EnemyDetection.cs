using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDetection : MonoBehaviour
{
    private void OnTriggerStay(Collider other)
    {
        if(other.gameObject.GetComponent<Unit>())
            this.GetComponentInParent<EnemyFSM>().Target = other.gameObject.transform;
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.GetComponent<Unit>())
            this.GetComponentInParent<EnemyFSM>().Target = null;
    }
}
