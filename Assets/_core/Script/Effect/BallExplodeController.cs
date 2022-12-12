using System;
using System.Collections.Generic;
using _core.Script.Live;
using Cysharp.Threading.Tasks;
using Script.Abstract;
using UnityEngine;

namespace _core.Script.Effect
{
    public class BallExplodeController : MonoBehaviour, IPoolable
    {
        [SerializeField] private int _damage = 100;

        private List<IDamageable> cache = new List<IDamageable>();

        
        
        private void Start()
        {
            //assign delegate
            GetComponentInChildren<ParticleCollider>().collisionRecall = CollisionRecall;
        }


        private void CollisionRecall(GameObject other)
        {
            if (other.gameObject.CompareTag("Enemy"))
            {
                var damageable = other.gameObject.GetComponent<IDamageable>();
                
                
                //only the first collision will cause damage
                if (!cache.Contains(damageable))
                {
                    damageable.GetHit(_damage);
                
                    cache.Add(damageable);
                }
            }
        }
        

       
        public void Disable()
        {
            gameObject.SetActive(false);
        }

        public void Init()
        {
            gameObject.SetActive(true);
            
            
            cache.Clear();
            
            
            Invoke("Dequeue",1);
        }

        public void Dequeue()
        {
            GameFacade.Instance.GetInstance<IGameObjectPool>().Enqueue(gameObject);
        }
    }
}