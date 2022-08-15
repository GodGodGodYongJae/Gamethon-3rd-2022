/*This script created by using docs.unity3d.com/ScriptReference/MonoBehaviour.OnParticleCollision.html*/
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ParticleCollisionInstance : MonoBehaviour
{
    public GameObject[] EffectsOnCollision;
    public float DestroyTimeDelay = 5;
    public bool UseWorldSpacePosition;
    public float Offset = 0;
    public Vector3 rotationOffset = new Vector3(0,0,0);
    public bool useOnlyRotationOffset = true;
    public bool UseFirePointRotation;
    public bool DestoyMainEffect = false;
    private ParticleSystem part;
    private List<ParticleCollisionEvent> collisionEvents = new List<ParticleCollisionEvent>();
    private ParticleSystem ps;

    public int Damage;
    public bool isInGame;
    public bool isPlayerDamage = true;

    void Start()
    {
        part = GetComponent<ParticleSystem>();
    }
    private void Update()
    {
        if(isInGame && part.isPlaying.Equals(false))
        {
            ObjectPoolManager.Instance?.Free(gameObject);
        }
    }
    void OnParticleCollision(GameObject other)
    {
        //Debug.Log(other.tag);
        if((isPlayerDamage.Equals(false) && other.tag == "Enemy") ||
            (isPlayerDamage.Equals(true) && other.tag == "Player"))
        {
            int numCollisionEvents = part.GetCollisionEvents(other, collisionEvents);
            for (int i = 0; i < numCollisionEvents; i++)
            {
                foreach (var effect in EffectsOnCollision)
                {
                    var instance = Instantiate(effect, collisionEvents[i].intersection + collisionEvents[i].normal * Offset, new Quaternion()) as GameObject;
                    if (!UseWorldSpacePosition) instance.transform.parent = transform;
                    if (UseFirePointRotation) { instance.transform.LookAt(transform.position); }
                    else if (rotationOffset != Vector3.zero && useOnlyRotationOffset) { instance.transform.rotation = Quaternion.Euler(rotationOffset); }
                    else
                    {
                        instance.transform.LookAt(collisionEvents[i].intersection + collisionEvents[i].normal);
                        instance.transform.rotation *= Quaternion.Euler(rotationOffset);
                    }
                    Destroy(instance, DestroyTimeDelay);
                }
            }

            if (isInGame)
            {
                IDamageable damageable = other.GetComponent<IDamageable>();
                damageable.Damage(Damage, this.gameObject);
                if (DestoyMainEffect.Equals(true))
                    ObjectPoolManager.Instance?.Free(gameObject);
            }
            else
                Destroy(gameObject, DestroyTimeDelay + 0.5f);
        }
        


    }

    void DeleteObj()
    {
        if (isInGame)
            ObjectPoolManager.Instance?.Free(gameObject);
        else
            Destroy(gameObject, DestroyTimeDelay + 0.5f);
    }
}
