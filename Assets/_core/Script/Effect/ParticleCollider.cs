using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleCollider : MonoBehaviour
{
    // Start is called before the first frame update

    public Action<GameObject> collisionRecall;
    
    private void OnParticleCollision(GameObject other)
    {
        collisionRecall?.Invoke(other);
    }
}
