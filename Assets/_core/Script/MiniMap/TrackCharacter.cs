using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;

public class TrackCharacter : MonoBehaviour
{
    // Start is called before the first frame update

    public Transform playerIconTransform;
    
    private Transform _playerTransform;

    private bool _canTrick = false;
    async void Start()
    {
        await UniTask.DelayFrame(5);
        _playerTransform = GameObject.FindWithTag("Player").transform;

        _canTrick = true;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //trick the player 
        if (_canTrick)
        {
            transform.position = _playerTransform.position + Vector3.up * 15;

            playerIconTransform.eulerAngles = new Vector3(0, 0, -_playerTransform.eulerAngles.y);
        }
    }
}
