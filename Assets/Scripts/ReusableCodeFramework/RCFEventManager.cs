using UnityEngine;
using UnityEngine.Events;
using System;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// The Base Event class.
/// All events must inherit from this class.
/// </summary>
public class EventBase
{
	
}

/// <summary>
/// A Generic event manager.
/// Allows other classes to add and remove their fucntions as listeners for certain events, allowing them to be called when
/// the specific is triggered
/// </summary>
public class RCFEventManager
{
	/// <summary>
	/// Static reference to the event manager.
	/// </summary>
	static RCFEventManager _instance = null;
	public static RCFEventManager Instance
	{
		get
		{
			if (_instance == null)
			{
				_instance = new RCFEventManager();
			}

			return _instance;
		}
	}

	public delegate void EventDelegate<T> (T e) where T : EventBase;
	private delegate void EventDelegate (EventBase e);

	private Dictionary<System.Type, EventDelegate> delegates = new Dictionary<System.Type, EventDelegate>();
	private Dictionary<System.Delegate, EventDelegate> delegateLookup = new Dictionary<System.Delegate, EventDelegate>();

	/// <summary>
	/// Adds the listening method to listen for the provided event type <T>.
	/// </summary>
	/// <param name="methodToAddForListening">Method to add for listening.</param>
	/// <typeparam name="T">The Event type to listen for.</typeparam>
	public void AddListener<T> (EventDelegate<T> methodToAddForListening) where T : EventBase
	{	
		// Early-out if we've already registered this delegate
		if (delegateLookup.ContainsKey(methodToAddForListening))
			return;

		// Create a new non-generic delegate which calls our generic one.
		// This is the delegate we actually invoke.
		EventDelegate internalDelegate = (e) => methodToAddForListening((T)e);
		delegateLookup[methodToAddForListening] = internalDelegate;

		EventDelegate tempDel;
		if (delegates.TryGetValue(typeof(T), out tempDel))
		{
			delegates[typeof(T)] = tempDel += internalDelegate; 
		}
		else
		{
			delegates[typeof(T)] = internalDelegate;
		}
	}

	/// <summary>
	/// Removes the listening method from listening for the provided event type <T>.
	/// </summary>
	/// <param name="methodToRemoveFromListening">Method to remove from listening.</param>
	/// <typeparam name="T">The Event type to stop listening for.</typeparam>
	public void RemoveListener<T> (EventDelegate<T> methodToRemoveFromListening) where T : EventBase
	{
		EventDelegate internalDelegate;
		if (delegateLookup.TryGetValue(methodToRemoveFromListening, out internalDelegate))
		{
			EventDelegate tempDel;
			if (delegates.TryGetValue(typeof(T), out tempDel))
			{
				tempDel -= internalDelegate;
				if (tempDel == null)
				{
					delegates.Remove(typeof(T));
				}
				else
				{
					delegates[typeof(T)] = tempDel;
				}
			}

			delegateLookup.Remove(methodToRemoveFromListening);
		}
	}

	/// <summary>
	/// Triggers the event.
	/// </summary>
	/// <param name="eventTypeToTrigger">Event type to trigger.</param>
	public void TriggerEvent (EventBase eventTypeToTrigger)
	{
		EventDelegate del;
		if (delegates.TryGetValue(eventTypeToTrigger.GetType(), out del))
		{
			del.Invoke(eventTypeToTrigger);
		}
	}
}