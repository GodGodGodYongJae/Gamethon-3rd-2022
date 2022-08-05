using UnityEngine;
using System.Collections;
using UnityEngine.Animations;


public class SelfDestruct : MonoBehaviour {
	public float selfdestruct_in = 4; // Setting this to 0 means no selfdestruct.
	public bool iseffect;

	ParentConstraint parentConstraint;
	ConstraintSource source;
	ParticleSystem particleSystem;

	public Transform parent;

    private void Awake()
    {
		parentConstraint = GetComponent<ParentConstraint>();
		source = new ConstraintSource();
	}
    void Start () {
		//if(iseffect)
  //      {
			
		//	source.sourceTransform = parent;/* EnemyFactoryMethod.Instance.player.effectParent.transform;*/
		//	source.weight = 1;
		//	parentConstraint.AddSource(source);
			 
		//}
		
		if ( selfdestruct_in != 0){ 
			Destroy (gameObject, selfdestruct_in);
		}
	}

	public void ChangeParent(Transform parent)
    {
		source.sourceTransform = parent;
		source.weight = 1;
        if (parentConstraint.sourceCount > 0)
        {
			parentConstraint.RemoveSource(0);
        }
	
		parentConstraint.AddSource(source);
	}

    private void OnEnable()
    {
		particleSystem = GetComponent<ParticleSystem>();
		particleSystem.Play();
		//source = new ConstraintSource();
		//source.sourceTransform = EnemyFactoryMethod.Instance.player.effectParent.transform;
		//source.weight = 1;
		//parentConstraint.AddSource(source);
	}
}
