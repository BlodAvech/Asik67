using System;
using UnityEngine;

public class HealthChangedEventArgs : EventArgs
{
	public float OldValue { get;}
	public float NewValue { get;}
	public float MaxValue { get; }
	
	public HealthChangedEventArgs(float o , float n , float m)
	{
		OldValue = o;
		NewValue = n;
		MaxValue = m;
	}
}