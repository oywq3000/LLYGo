using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackCharacter : MonoBehaviour
{
    // Start is called before the first frame update

    public Transform playerIconTransform;
    
    private Transform _playerTransform;
    void Start()
    {
        _playerTransform = GameObject.FindWithTag("Player").transform;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //trick the player 
        transform.position = _playerTransform.position + Vector3.up * 15;

        playerIconTransform.eulerAngles = new Vector3(0, 0, -_playerTransform.eulerAngles.y);
    }
}
