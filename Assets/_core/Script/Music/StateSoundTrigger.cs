using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.Animations;

namespace _core.Script.Music
{
   public enum MachineState
    {
        OnEntry,
        OnExit
    }
    
    public class StateSoundTrigger : StateMachineBehaviour
    {
        public string audioName;
        public MachineState state = MachineState.OnEntry;
        
        public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex,
            AnimatorControllerPlayable controller)
        {
            
            base.OnStateEnter(animator, stateInfo, layerIndex, controller);
            if (state==MachineState.OnEntry)
            {
                AudioManager.Instance.PlayOneClip(audioName);
            }
        }
        
        public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            base.OnStateExit(animator, stateInfo, layerIndex);
            if (state==MachineState.OnExit)
            {
                AudioManager.Instance.PlayOneClip(audioName);
            }
        }
    }
}