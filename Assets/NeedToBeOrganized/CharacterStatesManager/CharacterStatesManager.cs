using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterStatesManager : MonoBehaviour, ICharacterStateManager, ICharacterStateObserver
{
    /*
     * Dictionary for holding Character States 
     */
    private Dictionary<string, CharacterState> characterStatesDict = new Dictionary<string, CharacterState>();

    public void RegisterCharacterState(string name, CharacterState characterStateObject)
    {
        if (!characterStatesDict.ContainsKey(name))
        {
            characterStatesDict.Add(name, characterStateObject);
        }
        else
        {
            throw new CharacterStateOfNameAlreadyRegistered(
                "Character state with name: " + name + "is already registered");
        }
    }

    public object GetCharacterStateValue(string name){
        return characterStatesDict[name].GetStateValue();
    }

    public CharacterState.CharacterStateSubscription GetCharacterStateSubscription (string name){
        return characterStatesDict[name].GetCharacterStateSubscription();
    }

	public bool ExistsState(string name){
		return characterStatesDict.ContainsKey(name);
	}

	public CharacterState GetExistingCharacterState(string name){
		CharacterState characterState = characterStatesDict[name];
		return characterState;
	}

	public void DeregisterCharacterstate(string name){
		characterStatesDict.Remove(name);
	}
}

public class CharacterState
{
    public delegate void StateChanged(object newState);
    public event StateChanged OnStateChanged;

    public string name { get; private set; }
    private object state;
    private CharacterStateSubscription subscription;
	private object previousState;
    public CharacterState(string stateName, object stateObject)
    {
        name = stateName;
        this.state = stateObject;
        this.subscription = new CharacterStateSubscription(this);
    }

    public object GetStateValue(){
        return this.state;
    }

    public CharacterStateSubscription GetCharacterStateSubscription(){
        return this.subscription;
    }

    public void SetState(object newState) {
        this.state = newState;
		if (OnStateChanged != null){
			OnStateChanged(this.state);
		}
    }

    public class CharacterStateSubscription{
        private CharacterState stateObject;

        public CharacterStateSubscription(CharacterState characterStateObject){
            this.stateObject = characterStateObject;
        }

        public event StateChanged OnStateChanged{
            add {
                stateObject.OnStateChanged += value;
            }
            remove {
                stateObject.OnStateChanged -= value;
            }
            
        }
    }
}



//public class DepracatedCharacterStatesManager{
//	/**
//	 * Holds references to all of the TriggerColliderBroadcasters belonging to this character. These 
//	 * are meant for internal messaging to different components that may need to respond to 
//	 * collisions to different parts of the character.
//	 */
//    private Dictionary<TriggerColliderBroadcastersNames, TriggerColliderBroadcaster2D> colliderBroadcasters = 
//        new Dictionary<TriggerColliderBroadcastersNames, TriggerColliderBroadcaster2D>();

//	/**
//	 * Holds references to all of the characterBehaviorStates belonging to this character.
//	 */
//    private Dictionary<CharacterStateNames, BinaryBehaviorState> characterBehaviorStates = 
//        new Dictionary<CharacterStateNames, BinaryBehaviorState> ();

//	/**
//	 * Holds references to all of the numericalResources belonging to this character.
//	 */ 
//    private Dictionary<CharacterStateNames, NumericalResource> numericalResources = 
//        new Dictionary<CharacterStateNames, NumericalResource> ();

//    private Dictionary<WindowNames, MutualExclusionBinaryStateWindow> windows = 
//        new Dictionary<WindowNames, MutualExclusionBinaryStateWindow> ();

//	/**
//	 * Holds references to the numericalCombatStats
//	 */ 
//	//	private Dictionary<string, CombatStat> combatStats =
//	//		new Dictionary<string, CombatStat> ();

//	/**
//	 * Gets the trigger collider broadcaster to the Dictionary of trigger broadcasters. Use this to register 
//	 * functions to the events in the broadcaster
//	 */ 
//    public TriggerColliderBroadcaster2D GetTriggerColliderBroadcaster (TriggerColliderBroadcastersNames triggerBroadcaster){
//		return colliderBroadcasters[triggerBroadcaster];
//	}

//    public MutualExclusionBinaryStateWindow GetMutualExclusionBinaryWindow(WindowNames windowName){
//		return windows[windowName];
//	}


//	/**
//	 * Registers the trigger collider broadcaster to the Dictionary of trigger broadcasters
//	 */ 
//    public void RegisterTriggerColliderBroadcaster (TriggerColliderBroadcastersNames name, TriggerColliderBroadcaster2D broadcasterObject){
//		colliderBroadcasters.Add (name, broadcasterObject);
//	}

//	/**
//	 * Registers the behavior state to the Dictionary of behavior states.
//	 */ 
//    public void RegisterCharacterBinaryBehaviorState(CharacterStateNames name, BinaryBehaviorState stateObject){
//		characterBehaviorStates.Add (name, stateObject);
//	}

//	/**
//	 * Returns the value of the behavior state (true or false)
//	 */ 
//    public bool GetCharacterBinaryBehaviorState(CharacterStateNames name){
//		return characterBehaviorStates [name].GetState;
//	}

//	/**
//	 * Rerturns the subscription for a binary behavior state.
//	 */ 
//    public BinaryBehaviorState.BinaryBehaviorStateSubscription GetBinaryBehaviorStateSubcription(CharacterStateNames name){
//		return characterBehaviorStates [name].GetSubscription;
//	}


//	/**
//	 * Registers numerical resources such as Health, Flight Energy, or other types of
//	 * resources that could potentially be used by complex enemies for example.
//	 */
//    public void RegisterNumericalResource(CharacterStateNames name, NumericalResource resourceObject) {
//		numericalResources.Add (name, resourceObject);
//	}

//	/**
//	 * Returns a read only numerical resource.
//	 */ 
//    public NumericalResource.ReadOnlyNumericalResource GetReadOnlyNumericalResource(CharacterStateNames resourceName) {
//		return numericalResources[resourceName].GetReadOnly;
//	}

//	/**
//	 * Returns the subscription for a resource.
//	 */ 
//    public NumericalResource.NumericalResourceSubscription GetNumericalResourceSubscription(CharacterStateNames resourceName){
//		return numericalResources [resourceName].GetSubscription;
//	}

//	/**
//	 * Remove TriggerColliderBroadcaster from dict
//	 */ 
//    public void DeregisterTriggerColliderBroadcaster (TriggerColliderBroadcastersNames name){
//		colliderBroadcasters.Remove (name);
//	}

//	/**
//	 * Remove NumericalResources from dict
//	 */ 
//    public void DeregisterNumericalResource (CharacterStateNames name){
//		numericalResources.Remove (name);
//	}

//	/**
//	 * Remove BinaryBehaviorState from dict
//	 */ 
//    public void DeregisterBinaryBehaviorState(CharacterStateNames name){
//		characterBehaviorStates.Remove (name);
//	}

//	/**
//	 * Makes and registers a list of MUTUALLY EXCLUSIVE binary behavior states
//	 */ 
//	public CharacterStatesManager.BinaryBehaviorState[] MakeMutuallyExclusiveBinaryStates(
//        CharacterStateNames[] statesToMake, WindowNames bundleName){
//		List<CharacterStatesManager.BinaryBehaviorState> states = 
//			new List<CharacterStatesManager.BinaryBehaviorState>();

//        foreach (CharacterStateNames stateName in statesToMake){
//			CharacterStatesManager.BinaryBehaviorState state =
//				new CharacterStatesManager.BinaryBehaviorState (stateName);
//			this.RegisterCharacterBinaryBehaviorState (state.name, state);
//			states.Add (state);
//		}

//		foreach (CharacterStatesManager.BinaryBehaviorState currentState in states) {
//			HashSet<CharacterStatesManager.BinaryBehaviorState> otherStates = 
//				new HashSet<CharacterStatesManager.BinaryBehaviorState> (states);
//			HashSet<CharacterStatesManager.BinaryBehaviorState> currStateSet = 
//				new HashSet<CharacterStatesManager.BinaryBehaviorState> (
//					new CharacterStatesManager.BinaryBehaviorState[] {currentState});

//			CharacterStatesManager.BinaryBehaviorState[] otherStatesArray = 
//				new CharacterStatesManager.BinaryBehaviorState[states.Count - 1];

//			otherStates.ExceptWith (currStateSet);

//			otherStates.CopyTo (otherStatesArray);
//			/**
//			 * THIS RIGHT HERE IS SUPER SKETCHY BUT UH... I'LL KEEP THIS FOR NOW
//			 */ 
//			new MutualExculsionBinaryState (currentState, otherStatesArray);
//		}
//		CharacterStatesManager.BinaryBehaviorState[] stateArray = 
//			new CharacterStatesManager.BinaryBehaviorState[statesToMake.Length];
//		states.CopyTo (stateArray);

//		windows.Add (bundleName, new MutualExclusionBinaryStateWindow(stateArray));
////		print ("Added");

//		return stateArray;
//	}
		

//	/**
//	 * Shows which mutually exclusive binary state is currently active
//	 */ 
//	public class MutualExclusionBinaryStateWindow{
//		public delegate void OnMutualExculsionBinarystateChanged();
//		public event OnMutualExculsionBinarystateChanged OnWindowStateChanged;

//		private CharacterStatesManager.BinaryBehaviorState[] mutuallyExclusivedStates;
//		private CharacterStatesManager.BinaryBehaviorState visibleState;

//		public MutualExclusionBinaryStateWindow (CharacterStatesManager.BinaryBehaviorState[] states){
//			mutuallyExclusivedStates = states;
//			foreach (CharacterStatesManager.BinaryBehaviorState state in mutuallyExclusivedStates){
//				if (state.GetState){
//					visibleState = state;
//				}
//				state.OnTrue += UpdateVisible;
//			}
//		}

//        public CharacterStateNames GetVisible(){
//			return visibleState.name;
//		}

//		void UpdateVisible (){
//			foreach (CharacterStatesManager.BinaryBehaviorState state in mutuallyExclusivedStates) {
//				if (state.GetState) {
//					visibleState = state;
//					/**
//					 * Firing event so window holder can receive event
//					 * 
//					 */
//					if (OnWindowStateChanged != null) {
//						OnWindowStateChanged ();
//					}
//					break;
//				}
//			}

//		}
//	}

//	/**
//	 * A higher level abstraction of binary states built to be usable as a switch
//	 * showing the current dominant state of a bunch of binary states. 
//	 * 
//	 */ 
//	public class MutualExculsionBinaryState{

//		private CharacterStatesManager.BinaryBehaviorState[] mutuallyExclusivedStates;
//		private CharacterStatesManager.BinaryBehaviorState state;

//		public MutualExculsionBinaryState(CharacterStatesManager.BinaryBehaviorState ontrueState,
//			CharacterStatesManager.BinaryBehaviorState[] states){
//			mutuallyExclusivedStates = states;
//			this.state = ontrueState;
//			MutuallyExclude ();
//		}

//		private void MutuallyExclude (){
//			this.state.OnTrue += SetOthersFalse;
//		}

//		void SetOthersFalse() {
//			foreach (CharacterStatesManager.BinaryBehaviorState mutuallyExcluded in mutuallyExclusivedStates) {
//				mutuallyExcluded.SetStateFalse ();
//			}
//		}

//	}
		

//	public class NumericalResource {
//		public delegate void OnResourceChange (int delta);
//		public event OnResourceChange OnCurrentChanged;
//		public event OnResourceChange OnMaximumChanged;
//		private ReadOnlyNumericalResource readOnly;
//		private NumericalResourceSubscription subscription;

//        public CharacterStateNames name { get; private set; }
//		private int maximum;
//		private int current;

//        public NumericalResource (CharacterStateNames name, int max, int curr){
//			this.name = name;
//			this.maximum = max;
//			this.current = curr;
//			readOnly = new ReadOnlyNumericalResource(this);
//			subscription = new NumericalResourceSubscription(this);
//		}

//		public void AddToMax(int amount){
//			this.maximum += amount;
//			MaximumChanged (amount);
//		}

//		public void AddToCurrent(int amount){
//			this.current += amount;
//			CurrentChanged (amount);
//		}

//		public void SetCurrent (int amount){
//			int delta = amount - this.current;
//			this.current = amount;
//			CurrentChanged (delta);
//		}

//		public void SetMax (int amount){
//			int delta = amount - this.maximum;
//			this.maximum = amount;
//			MaximumChanged (delta);
//		}

//		public void SetCurrentToMax (){
//			int delta = this.maximum - this.current;
//			this.current = this.maximum;
//			CurrentChanged (delta);
//		}

//		private void MaximumChanged (int delta){
//			if (OnMaximumChanged != null) {
//				OnMaximumChanged (delta);
//			}
//		}

//		private void CurrentChanged (int delta){
//			if (OnCurrentChanged != null) {
//				OnCurrentChanged (delta);
//			}
//		}

//		public int GetCurrent{
//			get {return this.current;}
//		}

//		public int GetMaximum{
//			get {return this.maximum;}
//		}

//		public float GetPercentage{
//			get { return this.current / this.maximum; }
//		}

//		public ReadOnlyNumericalResource GetReadOnly{
//			get {return readOnly;}
//		}

//		public NumericalResourceSubscription GetSubscription{
//			get { return subscription; }
//		}

//		public class NumericalResourceSubscription{
//			private NumericalResource fullResourceObject;
//			public event OnResourceChange OnCurrentChanged {
//				add {
//					fullResourceObject.OnCurrentChanged += value;
//				}
//				remove {
//					fullResourceObject.OnCurrentChanged -= value;
//				}
//			}

//			public event OnResourceChange OnMaximumChanged {
//				add {
//					fullResourceObject.OnMaximumChanged += value;
//				}
//				remove {
//					fullResourceObject.OnMaximumChanged -= value;
//				}
//			}

//			public NumericalResourceSubscription(NumericalResource resourceObject){
//				fullResourceObject = resourceObject;
//			}

//		}

//		public class ReadOnlyNumericalResource{
//			private NumericalResource fullResourceObject;
//			public ReadOnlyNumericalResource(NumericalResource resourceObject){
//				fullResourceObject = resourceObject;
//			}

//			public int GetMaximum{
//				get { return fullResourceObject.maximum;}
//			}

//			public int GetCurrent{
//				get { return fullResourceObject.current; }
//			}

//			public float GetPercentage{
//				get { return fullResourceObject.current / fullResourceObject.maximum; }
//			}
//		}

//	}

//	public class BinaryBehaviorState  {
//		public delegate void OnStateChange();
//		public event OnStateChange OnTrue;
//		public event OnStateChange OnFalse;
//		private bool state;
//		private BinaryBehaviorStateSubscription subscription;

//        public CharacterStateNames name { get; private set; }

//        public BinaryBehaviorState(CharacterStateNames name){
//			this.name = name;
//			subscription = new BinaryBehaviorStateSubscription(this);
//		}

//		public void SetStateTrue(){
//			if (!state) {
//				state = true;
//				if (OnTrue != null) {
//					OnTrue ();
//				}
//			}
//		}

//		public void SetStateFalse(){
//			if (state) {
//				state = false;
//				if (OnFalse != null) {
//					OnFalse ();
//				}
//			}
//		}

//		public bool GetState{
//			get { return state; }
//		}


//		public BinaryBehaviorStateSubscription GetSubscription{
//			get {return subscription;}
//		}

//		public class BinaryBehaviorStateSubscription{
//			private BinaryBehaviorState fullBinaryStateObject;
//			public event OnStateChange OnTrue{
//				add {
//					fullBinaryStateObject.OnTrue += value;
//				}
//				remove {
//					fullBinaryStateObject.OnTrue -= value;
//				}
//			}

//			public event OnStateChange OnFalse {
//				add {
//					fullBinaryStateObject.OnFalse += value;
//				}
//				remove {
//					fullBinaryStateObject.OnFalse -= value;
//				}
//			}

//			public BinaryBehaviorStateSubscription (BinaryBehaviorState stateObject){
//				fullBinaryStateObject = stateObject;
//			}

//		}

//	}


//}
