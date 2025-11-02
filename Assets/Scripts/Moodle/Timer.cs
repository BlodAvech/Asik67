using System.Collections;
using UnityEngine;

public class Timer : MonoBehaviour
{
	private float timer;
	private float elapsed;
	private Coroutine timerCoroutine;
	private int[] stateIndexes;

	public delegate void TimerEventHandler(params int[] stateIndexes);
	public event TimerEventHandler OnTimerEnd;

	public void SetTimer(float min, float max , params int[] stateIndexes)
	{
		if (timerCoroutine != null)
			StopCoroutine(timerCoroutine);

		timer = Random.Range(min, max);
		elapsed = 0f;
		timerCoroutine = StartCoroutine(Elapsing());

		this.stateIndexes = stateIndexes;
	}

	private IEnumerator Elapsing()
	{
		while (elapsed < timer)
		{
			elapsed += Time.fixedDeltaTime;
			yield return new WaitForFixedUpdate();
		}

		OnTimerEnd?.Invoke(stateIndexes);
		timerCoroutine = null;
	}
}
