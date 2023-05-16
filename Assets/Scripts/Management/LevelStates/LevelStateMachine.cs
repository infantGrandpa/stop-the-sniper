using System;
using UnityEngine;

namespace SniperProject
{
    public class LevelStateMachine : MonoBehaviour
    {
        private ILevelState currentState;
        [SerializeField] float secsPerTransition;

        private void Start()
        {
            currentState = new SpawnState();
            currentState.EnterState();
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
            currentState.ExitState();

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

            currentState = nextState;
            currentState.EnterState();
        }

        private ILevelState GetNextStateFromTransition()
        {
            if (currentState is not TransitionState)
            {
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
