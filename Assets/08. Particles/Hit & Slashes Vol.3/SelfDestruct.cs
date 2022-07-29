using UnityEngine;
using System.Collections;
using UnityEngine.Animations;


public class SelfDestruct : MonoBehaviour {
	public float selfdestruct_in = 4; // Setting this to 0 means no selfdestruct.
	public bool iseffect;
	ParentConstraint parentConstraint;
	void Start () {
		if(iseffect)
        {
			parentConstraint = GetComponent<ParentConstraint>();
			ConstraintSource source = new ConstraintSource();
			source.sourceTransform = EnemyFactoryMethod.Instance.player.transform;
			source.weight = 1;
			parentConstraint.AddSource(source);
		}
		
		if ( selfdestruct_in != 0){ 
			Destroy (gameObject, selfdestruct_in);
		}
	}
}
