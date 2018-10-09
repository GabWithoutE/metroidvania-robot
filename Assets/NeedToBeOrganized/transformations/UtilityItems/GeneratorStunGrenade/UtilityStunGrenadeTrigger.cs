using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UtilityStunGrenadeTrigger : MonoBehaviour
{
	public GameObject stunGrenade;
	private ICharacterStateObserver stateObserver;
	private GameObject attacker;

	private void Awake()
	{
		stateObserver = GetComponentInParent(typeof(ICharacterStateObserver)) as ICharacterStateObserver;
		attacker = transform.root.gameObject;
	}

	public void TriggerAttack()
	{
		float[] playerdirection = (float[])stateObserver.GetCharacterStateValue(ConstantStrings.DIRECTION);
		if (playerdirection[1] > 0)
		{
			GameObject instance = Instantiate(stunGrenade, transform.position + Vector3.up, Quaternion.identity);
			instance.GetComponent<UtilityStunGrenadeMovement>().SetDirection(Vector2.up);

		}
		else if (playerdirection[1] < 0)
		{
			GameObject instance = Instantiate(stunGrenade, transform.position + Vector3.down, Quaternion.identity);
			instance.GetComponent<UtilityStunGrenadeMovement>().SetDirection(Vector2.down);

		}
		else if (playerdirection[0] < 0)
		{
			GameObject instance = Instantiate(stunGrenade, transform.position + Vector3.left, Quaternion.identity);
			instance.GetComponent<UtilityStunGrenadeMovement>().SetDirection(Vector2.left);

		}
		else
		{
			GameObject instance = Instantiate(stunGrenade, transform.position + Vector3.right, Quaternion.identity);
			instance.GetComponent<UtilityStunGrenadeMovement>().SetDirection(Vector2.right);

		}
	}

	// Use this for initialization
	void Start()
	{

	}

	// Update is called once per frame
	void Update () {
        
	}
}
