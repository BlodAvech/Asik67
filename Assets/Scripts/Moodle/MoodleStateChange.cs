// MoodleStateManager.cs
using UnityEngine;

public class MoodleStateManager : MonoBehaviour
{
    private Animator animator;
    private Timer timer;

    void Start()
    {
        animator = GetComponent<Animator>();
        timer = GetComponent<Timer>();
    }

    public void StartStateSelection(float minTime, float maxTime , params int[] stateIndexes)
    {
        timer.SetTimer(minTime, maxTime , stateIndexes);
        timer.OnTimerEnd += SelectNextState;
    }

    public void StopStateSelection()
    {
        if (timer != null)
        {
            timer.OnTimerEnd -= SelectNextState;
        }
    }

    private void SelectNextState(params int[] stateIndexes)
    {
        StopStateSelection(); 

        int stateIndex = Random.Range(0, stateIndexes.Length);
        Debug.Log(stateIndex);
		animator.SetInteger("StateIndex", stateIndexes[stateIndex]);
    }
}