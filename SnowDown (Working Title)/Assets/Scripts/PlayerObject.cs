using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerObject : MonoBehaviour
{
    public GameObject playerUnitPrefab;

    // Start is called before the first frame update
    void Start()
    {
        Instantiate(playerUnitPrefab);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
