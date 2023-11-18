using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    private void Update()
    {
        if (GameManager.instance.player != null)
        {
            Vector3 target = GameManager.instance.player.transform.position;
            this.transform.position = new Vector3(target.x, target.y, -100); 
        }
    }
}
