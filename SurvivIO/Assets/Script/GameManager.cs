using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public PlayerMovement player;
    
    private void Awake()
    {
        GameManager.instance = this;
    }
    
}
