using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;
    public bool HasCard { get; set; }

    public static GameManager Instance {
        get {
            if (_instance == null) Debug.Log("instance is null");
            return _instance;
        }
    }

    private void Awake() {
        _instance = this;
    }

    
}
