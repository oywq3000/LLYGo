using System.Collections.Generic;
using _core.Script.Live;
using Script.Abstract;
using UnityEngine;

namespace _core.Script.Effect
{
    public class ElementController : MonoBehaviour
    {
        [SerializeField] private int _damage = 1000;
        
        public float speed = 5;

        public float duration = 10;

        private float timer;
        
        
      

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
        
        private void FixedUpdate()
        {
            transform.Translate(new Vector3(0, 0, 1) * speed*Time.deltaTime);

            timer += Time.deltaTime;
            
            if (timer>duration)
            {
                GameFacade.Instance.GetInstance<IGameObjectPool>().Enqueue(gameObject);
                //reset timer
                timer = 0;
            }
        }
    }
}