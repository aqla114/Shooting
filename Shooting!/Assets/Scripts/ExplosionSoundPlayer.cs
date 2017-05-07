using UnityEngine;
using System.Collections;

public class ExplosionSoundPlayer : MonoBehaviour {

	void Start ()
    {
        Destroy(this.gameObject, 1.0f);
	}
}
