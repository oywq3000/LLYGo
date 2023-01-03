using System;
using System.Collections.Generic;
using Script.Abstract;
using UnityEngine;
using Random = UnityEngine.Random;

namespace _core.Script.Spawn
{
    public class FixedSpawn : MonoBehaviour
    {
        [SerializeField] private string monsterName;
        [SerializeField] private float cycle = 3; //spawn cycle for seconds
        [SerializeField] private float count = 1; //the spawning count for every spawn cycle
        [SerializeField] private int zoneRadius = 10; //the radius of zone spawn 
        [SerializeField] private int objectRadius =1;
        [SerializeField]private bool _canSpawn = false;

        private List<Vector3> alreadyPostion;
        
        
        private IGameObjectPool _assetFactory; 
        
        private float _timer;

        private Vector3 originalPoint;
        
        
     
        
        private void Start()
        {
            _assetFactory = GameFacade.Instance.GetInstance<IGameObjectPool>();
            
            _timer = cycle;

            originalPoint = transform.position;
        }
        
        
        

        private void FixedUpdate()
        {
            if (!_canSpawn) return;
            
            if (_timer>=cycle)
            {
                _timer = 0;

                for (int i = 0; i < count; i++)
                {
                    
                    //spawn monster
                    var dequeue = _assetFactory.Dequeue(monsterName,transform);
                    dequeue.transform.localPosition = GetSpawnPoint();
                }
            }
            _timer += Time.deltaTime;
        }


        Vector3 GetSpawnPoint()
        {
            //return the random position 
            var x = Random.Range(-zoneRadius / objectRadius, zoneRadius / objectRadius + 1);
            var y = Random.Range(-zoneRadius / objectRadius, zoneRadius / objectRadius + 1);

            var vector3 = new Vector3(x*objectRadius,0,y*objectRadius);

            var ray = new Ray(new Vector3(vector3.x, transform.position.y, vector3.z), Vector3.down);
            
            if (Physics.Raycast(ray,out RaycastHit raycastHit))
            {
                Debug.Log("Hit collider");
                vector3.y = raycastHit.point.y;
            }

            return vector3;
        }

        
        
        private void OnDrawGizmosSelected()
        {
            
            Gizmos.color = Color.green;
            
          
            var points = new List<Vector3>();


            var originalPoint = transform.position;
            
            //add original point
            points.Add(originalPoint);


            var index = 0;

            for (int i = -zoneRadius/objectRadius; i < zoneRadius/objectRadius+1; i++)
            {
                for (int j =  -zoneRadius/objectRadius; j <  zoneRadius/objectRadius+1; j++)
                {
                    var vector3 = new Vector3(originalPoint.x+i*objectRadius,10,originalPoint.z+j*objectRadius);
                  

                    var ray = new Ray(new Vector3(vector3.x, originalPoint.y, vector3.z), Vector3.down);

                    if (Physics.Raycast(ray,out RaycastHit raycastHit))
                    {
                        Debug.Log("Hit collider");
                        vector3.y = raycastHit.point.y;
                    }
                    
                    Gizmos.DrawSphere(vector3,0.5f);
                    points.Add(vector3);
                }
            }
        }
    }
}