using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossCharacterAbilityCastingBehavior : MonoBehaviour
{

    private ICharacterStateManager stateManager;
    private MoveSet moveSet;

    private GameObject lightAttackHorizontalRight;
    private GameObject heavyAttackHorizontalRight;

    private GameObject lightAttackInstanceRight;
    private GameObject lightAttackInstanceLeft;

    private LightMeleeAttackTrigger lightAttackInstanceRightTrigger;
    private LightMeleeAttackTrigger lightAttackInstanceLeftTrigger;

    private float lightAttackDuration;
    private CharacterState lightAttackCastState;

    private GameObject heavyAttackInstanceRight;
    private GameObject heavyAttackInstanceLeft;

    private HeavyAttackTrigger heavyAttackInstanceRightTrigger;
    private HeavyAttackTrigger heavyAttackInstanceLeftTrigger;

    private float heavyAttackDuration = 3.0f;
    private CharacterState heavyAttackCastState;

    private CharacterState utilityAbilityCastState;

    private bool previousHeavyAttackCastState;
    private float[] currentHeavyAttackChargeDirection;

    private bool abilityCastingLock;
    private Vector2 playerDirection;
    private GameObject player;

    private new void Awake()
    {
        abilityCastingLock = false;
        stateManager = GetComponentInParent(typeof(ICharacterStateManager)) as ICharacterStateManager;
        moveSet = GetComponent<MoveSet>();
        GetAttackPrefabs();

        InstantiateAbilities();

        RegisterAttackLocks();
        SetTriggers();
        SetAttackDurations();
    }

    private void InstantiateAbilities()
    {
        lightAttackInstanceRight = Instantiate(lightAttackHorizontalRight, transform.position, Quaternion.identity, transform);
        lightAttackInstanceLeft = Instantiate(lightAttackHorizontalRight, transform.position, Quaternion.identity, transform);
        lightAttackInstanceLeft.transform.localScale = new Vector3(-1, 1, 1);
        
        heavyAttackInstanceRight = Instantiate(heavyAttackHorizontalRight, transform.position, Quaternion.identity, transform);
        heavyAttackInstanceLeft = Instantiate(heavyAttackHorizontalRight, transform.position, Quaternion.identity, transform);
        heavyAttackInstanceLeft.transform.localScale = new Vector3(-1, 1, 1);
    }

    private void RegisterAttackLocks()
    {
        if (stateManager.ExistsState(ConstantStrings.LIGHT_ATTACK_CAST))
        {
            lightAttackCastState =
                stateManager.GetExistingCharacterState(ConstantStrings.LIGHT_ATTACK_CAST);
            lightAttackCastState.SetState(false);
        }
        else
        {
            lightAttackCastState = new CharacterState(ConstantStrings.LIGHT_ATTACK_CAST, false);
            stateManager.RegisterCharacterState(lightAttackCastState.name, lightAttackCastState);
        }
        if (stateManager.ExistsState(ConstantStrings.HEAVY_ATTACK_CAST))
        {
            heavyAttackCastState =
                stateManager.GetExistingCharacterState(ConstantStrings.HEAVY_ATTACK_CAST);
            heavyAttackCastState.SetState(false);
        }
        else
        {
            heavyAttackCastState = new CharacterState(ConstantStrings.HEAVY_ATTACK_CAST, false);
            stateManager.RegisterCharacterState(heavyAttackCastState.name, heavyAttackCastState);
        }
        if (stateManager.ExistsState(ConstantStrings.UTILITY_ABILITY_CAST))
        {
            utilityAbilityCastState =
                stateManager.GetExistingCharacterState(ConstantStrings.UTILITY_ABILITY_CAST);
            utilityAbilityCastState.SetState(false);
        }
        else
        {
            utilityAbilityCastState = new CharacterState(ConstantStrings.UTILITY_ABILITY_CAST, false);
            stateManager.RegisterCharacterState(utilityAbilityCastState.name, utilityAbilityCastState);
        }
    }

    // Use this for initialization
    void Start () {
        
        //previousHeavyAttackCastState = (bool)stateManager.GetCharacterStateValue("TriggerHeavyAttack");
        CharacterState.CharacterStateSubscription lightAttackSub =
            stateManager.GetCharacterStateSubscription("InputLightAttack");
        CharacterState.CharacterStateSubscription heavyAttackSub =
                          stateManager.GetCharacterStateSubscription("InputHeavyAttack");

        lightAttackSub.OnStateChanged += CastLightAttack;
        heavyAttackSub.OnStateChanged += CastHeavyAttack;

        player = GameObject.FindGameObjectWithTag("Player");
    }
    
    private void SetAttackDurations()
    {
        lightAttackDuration =
            lightAttackHorizontalRight.GetComponent<MeleeAbilityStats>().GetAbilityDuration();
        //heavyAttackDuration =
            //heavyAttackHorizontalRight.GetComponent<HeavyAbilityStats>().GetAbilityDuration();
    }
    
    private void SetTriggers()
    {
        lightAttackInstanceRightTrigger = lightAttackInstanceRight.GetComponent<LightMeleeAttackTrigger>();
        lightAttackInstanceLeftTrigger = lightAttackInstanceLeft.GetComponent<LightMeleeAttackTrigger>();

        heavyAttackInstanceRightTrigger = heavyAttackInstanceRight.GetComponent<HeavyAttackTrigger>();
        heavyAttackInstanceLeftTrigger = heavyAttackInstanceLeft.GetComponent<HeavyAttackTrigger>();
    }

    private void GetAttackPrefabs()
    {
        lightAttackHorizontalRight = moveSet.GetLightAttackHorizontalRight();
        heavyAttackHorizontalRight = moveSet.GetHeavyAttackHorizontalRight();
    }

    protected void CastLightAttack(object castState)
    {
        if (!isCasting(castState))
        {
            return;
        }
        if (!abilityCastingLock)
        {
            abilityCastingLock = true;
        }
        else
        {
            //print(abilityCastingLock);
            return;
        }

        Vector2 playerDirection = GetPlayerDirection();

        /*
         * do less checks by mixing some of these together
         */
        // Facing right and attacking up
        if (!(bool)lightAttackCastState.GetStateValue())
        {
            if (playerDirection[0] > 0)
            {
                lightAttackInstanceRightTrigger.TriggerAttack();
                lightAttackCastState.SetState(true);
                lightAttackCastState.SetState(false);
            }
            else if (playerDirection[0] < 0)
            {
                lightAttackInstanceLeftTrigger.TriggerAttack();
                lightAttackCastState.SetState(true);
                lightAttackCastState.SetState(false);
            }
            abilityCastingLock = false;
        }
    }

    protected void CastHeavyAttack(object castState)
    {
        if (!isCasting(castState))
        {
            return;
        }
        if (!abilityCastingLock)
        {
            abilityCastingLock = true;
        }
        else
        {
            //print(abilityCastingLock);
            return;
        }

        Vector2 playerDirection = GetPlayerDirection();

        if (!(bool)heavyAttackCastState.GetStateValue())
        {
            if (playerDirection.x > 0)
            {
                heavyAttackInstanceRightTrigger.TriggerAttack();
                heavyAttackCastState.SetState(true);
                heavyAttackCastState.SetState(false);
            }
            else if (playerDirection.x < 0)
            {
                heavyAttackInstanceLeftTrigger.TriggerAttack();
                heavyAttackCastState.SetState(true);
                heavyAttackCastState.SetState(false);
            }
            abilityCastingLock = false;
        }

        previousHeavyAttackCastState = (bool)castState;
    }

    private Vector2 GetPlayerDirection()
    {
        return transform.root.position - player.transform.position;
    }

    private void Update()
    {
        //Get player direction

    }

    IEnumerator GetAttackLock(CharacterState attackLock, float duration)
    {
        yield return new WaitForSeconds(duration);
        attackLock.SetState(false);
    }

    protected bool isCasting(object castState)
    {
        return (bool)castState;
    }
}
