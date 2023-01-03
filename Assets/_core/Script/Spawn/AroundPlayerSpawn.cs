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
        [SerializeField] private int zoneRadius = 20; //the radius of zone spawn 
        [SerializeField] private int playerDistance = 10;
        [SerializeField] private int objectRadius = 1;
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
            await UniTask.DelayFrame(5);
            
            _assetFactory = GameFacade.Instance.GetInstance<IGameObjectPool>();

            _timer = cycle;

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
            int x;
            int y;

            if (Random.Range(0, 2) == 0)
            {
                x = Random.Range((zoneRadius - playerDistance) / objectRadius, zoneRadius / objectRadius + 1);
                y = Random.Range((zoneRadius - playerDistance) / objectRadius, zoneRadius / objectRadius + 1);
            }
            else
            {
                x = Random.Range(-zoneRadius / objectRadius, (-zoneRadius + playerDistance) / objectRadius + 1);
                y = Random.Range(-zoneRadius / objectRadius, (-zoneRadius + playerDistance) / objectRadius + 1);
            }

            var vector3 = new Vector3(x * objectRadius, 0, y * objectRadius);

            var ray = new Ray(new Vector3(vector3.x, transform.position.y, vector3.z), Vector3.down);

            if (Physics.Raycast(ray, out RaycastHit raycastHit))
            {
                vector3.y = raycastHit.point.y;
            }

            return vector3;
        }


        private void OnDrawGizmosSelected()
        {
            return;
            Gizmos.color = Color.green;


            var points = new List<Vector3>();


            var originalPoint = transform.position;

            //add original point
            points.Add(originalPoint);


            var index = 0;

            for (int i = -zoneRadius / objectRadius; i < zoneRadius / objectRadius + 1; i++)
            {
                if (i > (-zoneRadius + playerDistance) / objectRadius &&
                    i < (zoneRadius - playerDistance) / objectRadius)
                {
                    continue;
                }

                for (int j = -zoneRadius / objectRadius; j < zoneRadius / objectRadius + 1; j++)
                {
                    if (j > (-zoneRadius + playerDistance) / objectRadius &&
                        j < (zoneRadius - playerDistance) / objectRadius)
                    {
                        continue;
                    }

                    var vector3 = new Vector3(originalPoint.x + i * objectRadius, 10,
                        originalPoint.z + j * objectRadius);


                    var ray = new Ray(new Vector3(vector3.x, originalPoint.y, vector3.z), Vector3.down);

                    if (Physics.Raycast(ray, out RaycastHit raycastHit))
                    {
                        Debug.Log("Hit collider");
                        vector3.y = raycastHit.point.y;
                    }

                    Gizmos.DrawSphere(vector3, 0.5f);
                    points.Add(vector3);
                }
            }
        }
    }
}