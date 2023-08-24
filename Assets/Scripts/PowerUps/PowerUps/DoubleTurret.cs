namespace CosmicCuration.PowerUps
{
    public class DoubleTurret : PowerUpController
    {
        public DoubleTurret(PowerUpData powerUpData) : base(powerUpData) { }

        public override void Activate()
        {
            base.Activate();
            GameService.Instance.PlayerService.GetPlayerController().ToggleDoubleTurret(true);
            GameService.Instance.GetUIService().TogglePowerUpUI(true, PowerUpType.DoubleTurret);
        }

        public override void Deactivate()
        {
            base.Deactivate();
            GameService.Instance.PlayerService.GetPlayerController().ToggleDoubleTurret(false);
            GameService.Instance.GetUIService().TogglePowerUpUI(false, PowerUpType.DoubleTurret);
        }
    }
}