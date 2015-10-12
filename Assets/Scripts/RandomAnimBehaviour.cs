using UnityEngine;
using System.Collections;

public class RandomAnimBehaviour : StateMachineBehaviour
{
    public  string parametrName = "IdleAnimID";
    public int[] states;



    override public void OnStateMachineEnter(Animator animator, int stateMachinePathHash)
    {        
        if(states.Length == 0)
            animator.SetInteger(parametrName, 0);
        else
        {
            int index = Random.Range(0, states.Length);
            animator.SetInteger(parametrName, states[index]);
        }
    }
}   // Karol Sobanski
