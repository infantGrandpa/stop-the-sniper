using System;
using UnityEngine;
using Sirenix.OdinInspector;

namespace SniperProject
{
    public class LevelStateMachine : MonoBehaviour
    {
        private ILevelState currentState;
        [SerializeField] float secsPerTransition;

        [Header("For Debugging")]
        [SerializeField, ReadOnly] string currentStateName = "None";
        [SerializeField, ReadOnly] string nextState = "N/A";

        private void Start()
        {
            ChangeState(new SpawnState());
        }

        private void Update()
        {
            currentState.UpdateState();
            CheckStateStatus();
        }

        private void CheckStateStatus()
        {
            if (!currentState.IsStateComplete())
            {
                return;
            }

            TransitionToNextState();
        }

        private void TransitionToNextState()
        {
            ILevelState nextState;
            switch (currentState)
            {
                case SpawnState:
                    nextState = CreateNewTransitionState<WaitState>();
                    break;
                case WaitState:
                    nextState = CreateNewTransitionState<DeployState>();
                    break;
                case DeployState:
                    nextState = CreateNewTransitionState<SpawnState>();
                    break;
                case TransitionState:
                    nextState = GetNextStateFromTransition();
                    break;
                default:
                    nextState = new PauseState();
                    break;
            }

            ChangeState(nextState);
        }

        private void ChangeState(ILevelState newState)
        {
            currentState?.ExitState();
            currentState = newState;
            currentState?.EnterState();

            currentStateName = currentState?.GetType().Name ?? "None";
            nextState = currentState is TransitionState ? ((TransitionState)currentState).nextState.GetType().Name : "N/A";
        }

        private ILevelState GetNextStateFromTransition()
        {
            if (currentState is not TransitionState)
            {
                DebugHelper.LogError("Current State (" + currentStateName + ") is not TransitionState");
                return null;
            }

            TransitionState transitionState = (TransitionState)currentState;
            ILevelState nextState = transitionState.nextState;
            return nextState;
        }

        private TransitionState CreateNewTransitionState<StateAfterTransition>() where StateAfterTransition : ILevelState
        {
            TransitionState transitionState = new();

            ILevelState nextState = Activator.CreateInstance<StateAfterTransition>();
            transitionState.nextState = nextState;
            transitionState.secsBeforeTransitionComplete = secsPerTransition;

            return transitionState;
        }
    }
}
