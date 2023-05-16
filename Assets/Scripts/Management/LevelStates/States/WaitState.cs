using UnityEngine;

namespace SniperProject
{
    public class WaitState : ILevelState
    {
        private bool allTargetsCleared;

        private float secsToWait;
        private float secsSinceStart;

        public void EnterState()
        {
            allTargetsCleared = false;

            secsToWait = WaveManager.Instance.secsBetweenWaves;
            secsSinceStart = 0;
        }
        public void UpdateState()
        {
            allTargetsCleared = LevelManager.Instance.targets.Count <= 0;
            secsSinceStart += Time.deltaTime;
        }

        public void ExitState()
        {
            WaveManager.Instance.GetWinLossThisWave();
        }

        public bool IsStateComplete()
        {
            bool waitTimeCompleted = secsSinceStart >= secsToWait;

            bool stateComplete = allTargetsCleared && waitTimeCompleted;
            return stateComplete;
        }
    }
}
