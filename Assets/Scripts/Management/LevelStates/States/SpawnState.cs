namespace SniperProject
{
    public class SpawnState : ILevelState
    {
        public void EnterState()
        {
            AstarPath.active.Scan();
            WaveManager.Instance.GetNextWave();
            PlayerBehaviour.Instance.StartFiring();
        }

        public void UpdateState()
        {
            
        }

        public void ExitState()
        {
            
        }

        public bool IsStateComplete()
        {
            return WaveManager.Instance.IsWaveComplete;
        }

    }
}
