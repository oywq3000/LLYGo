using System;
using System.Collections.Generic;
using _core.Script.Enemy;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using Script.Abstract;
using Script.Event;
using Script.Facade;
using UnityEngine;
using Random = UnityEngine.Random;

namespace _core.Script.Spawn
{
    public class AroundPlayerSpawn : MonoBehaviour
    {
        [SerializeField] private string[] monsterNames;
        [SerializeField] private float cycle = 3; //spawn cycle for seconds
        [SerializeField] private float count = 1; //the spawning count for every spawn cycle
        [SerializeField] private int minDistance = 20;
        [SerializeField] private int maxDistance = 40;
        [SerializeField] private int orientCount = 12;
        [SerializeField] private int monsterView = 500;
        [SerializeField] private bool canSpawn = false;
        [SerializeField] private bool followPlayer = false;

        private List<Vector3> alreadyPostion;
        private IGameObjectPool _assetFactory;
        private float _timer;

        private Transform _playerTransform;


        private async void Start()
        {

            canSpawn = false;
            await UniTask.Delay(TimeSpan.FromSeconds(2));
            
            _assetFactory = GameFacade.Instance.GetInstance<IGameObjectPool>();

            

            //get player Transform
            _playerTransform = GameObject.FindWithTag("Player").transform;

            GameFacade.Instance.RegisterEvent<OnPlayerDead>(e =>
            {
                canSpawn = false;
            }).UnRegisterOnDestroy(gameObject);
            canSpawn = true;
        }


        private void FixedUpdate()
        {
            if (canSpawn)
            {
                //to follow the position of player
                if (followPlayer)
                {
                    transform.position = new Vector3(_playerTransform.position.x, transform.position.y,
                        _playerTransform.transform.position.z);
                }
                
                
                if (_timer >= cycle)
                {
                    _timer = 0;

                    for (int i = 0; i < count; i++)
                    {
                        //spawn monster
                        var dequeue = _assetFactory.Dequeue(monsterNames[Random.Range(0, monsterNames.Length)]);
                        dequeue.transform.localPosition = GetSpawnPoint();
                        dequeue.GetComponent<EnemyAI>().viewRange = monsterView;
                    }
                }

                _timer += Time.deltaTime;
            }

           
        }


        Vector3 GetSpawnPoint()
        {
            //return the random position 
            float x;
            float y;

            var range = Random.Range(minDistance, maxDistance + 1);
            var pi = Random.Range(0,orientCount)*2*Mathf.PI/orientCount;

            Debug.Log(range*Mathf.Cos(pi));
            x = range*Mathf.Sin(pi);
            y = range*Mathf.Cos(pi);

            var transformPosition = transform.position;

            //offset the point
            x = x + transformPosition.x;
            y = y + transformPosition.z;
            
            Debug.Log($"X:{x},Y{y}");
            
            var vector3 = new Vector3(x, 0, y);

            var ray = new Ray(new Vector3(vector3.x, transformPosition.y, vector3.z), Vector3.down);

            if (Physics.Raycast(ray, out RaycastHit raycastHit))
            {
                vector3.y = raycastHit.point.y;
            }

            return vector3;
        }


       
        
    }
}