using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashParticle : MonoBehaviour
{
    private new ParticleSystem particleSystem;
    private void Awake()
    {
        particleSystem = GetComponent<ParticleSystem>();
    }
    public void OnParticle(bitFlags.PlayerMoveDirection pd, Transform target)
    {
        particleSystem.Play();

        switch (pd)
        {
            case bitFlags.PlayerMoveDirection.Left:
                transform.localEulerAngles = new Vector3(0, 90, 0); 
                break;
            case bitFlags.PlayerMoveDirection.Right:
                transform.localEulerAngles = new Vector3(0, -90, 0);
                break;
            case bitFlags.PlayerMoveDirection.Front:
                transform.localEulerAngles = new Vector3(0, -180, 0);
                break;
            case bitFlags.PlayerMoveDirection.Back:
                transform.localEulerAngles = new Vector3(0, 0, 0);
                break;
            case bitFlags.PlayerMoveDirection.Dash:
                transform.localEulerAngles = new Vector3(0, -180, 0);
                break;
            default:
                break;
        }
    }
}
