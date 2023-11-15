using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDetection : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.GetComponent<Unit>())
        {
            this.GetComponentInParent<EnemyFSM>().Target = other.gameObject.transform;
            Debug.Log(other.name);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.GetComponent<Unit>())
        {
            this.GetComponentInParent<EnemyFSM>().Target = null;
        }
    }
}
