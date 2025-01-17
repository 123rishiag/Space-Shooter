using CosmicCuration.Audio;
using CosmicCuration.Bullets;
using CosmicCuration.VFX;
using System.Collections;
using UnityEngine;

namespace CosmicCuration.Player
{
    public class PlayerController
    {
        // Dependencies
        private PlayerView playerView;
        private PlayerScriptableObject playerScriptableObject;
        private BulletPool bulletPool;

        // Variables
        private WeaponMode currentWeaponMode;
        private ShootingState currentShootingState;
        private ShieldState currentShieldState;
        private int currentHealth;
        private float currentRateOfFire;

        public PlayerController(PlayerView playerViewPrefab, PlayerScriptableObject playerScriptableObject, BulletPool bulletPool)
        {
            playerView = Object.Instantiate(playerViewPrefab);
            playerView.SetController(this);
            this.playerScriptableObject = playerScriptableObject;
            this.bulletPool = bulletPool;

            InitializeVariables();
        }

        private void InitializeVariables()
        {
            currentWeaponMode = WeaponMode.SingleCanon;
            currentHealth = playerScriptableObject.maxHealth;
            currentRateOfFire = playerScriptableObject.defaultFireRate;
            currentShieldState = ShieldState.Deactivated;
            currentShootingState = ShootingState.NotFiring;
            GameService.Instance.GetUIService().UpdateHealthUI(currentHealth);
        }


        // Input Handling:
        public void HandlePlayerInput()
        {
            HandlePlayerMovement();
            HandlePlayerRotation();
            HandleShooting();
        }

        private void HandlePlayerMovement()
        {
            if (Input.GetKey(KeyCode.W))
                playerView.transform.Translate(Vector2.up * Time.deltaTime * playerScriptableObject.movementSpeed);
            if (Input.GetKey(KeyCode.S))
                playerView.transform.Translate(Vector2.down * Time.deltaTime * playerScriptableObject.movementSpeed);
            if (Input.GetKey(KeyCode.A))
                playerView.transform.Translate(Vector2.left * Time.deltaTime * playerScriptableObject.movementSpeed);
            if (Input.GetKey(KeyCode.D))
                playerView.transform.Translate(Vector2.right * Time.deltaTime * playerScriptableObject.movementSpeed);
        }

        private void HandlePlayerRotation()
        {
            // Rotate the player to look in the direction of mouse position.
            var dir = Input.mousePosition - Camera.main.WorldToScreenPoint(playerView.transform.position);
            var angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            playerView.transform.rotation = Quaternion.AngleAxis(angle - 90, Vector3.forward);
        }

        private void HandleShooting()
        {
            if (Input.GetKeyDown(KeyCode.Space))
                GameService.Instance.StartGameCoroutine(FireWeapon());
            if (Input.GetKeyUp(KeyCode.Space))
                currentShootingState = ShootingState.NotFiring;
        }

        // Firing Weapons:
        private IEnumerator FireWeapon()
        {
            if (currentShootingState == ShootingState.Firing)
                yield break;

            currentShootingState = ShootingState.Firing;
            while (currentShootingState == ShootingState.Firing)
            {
                switch (currentWeaponMode)
                {
                    case WeaponMode.SingleCanon:
                        FireBulletAtPosition(playerView.canonTransform);
                        break;
                    case WeaponMode.DoubleTurret:
                        FireBulletAtPosition(playerView.turretTransform1);
                        FireBulletAtPosition(playerView.turretTransform2);
                        break;
                }
                yield return new WaitForSeconds(currentRateOfFire);
            }
        }

        private void FireBulletAtPosition(Transform fireLocation)
        {
            BulletController bulletToFire = bulletPool.GetBullet();
            bulletToFire.ConfigureBullet(fireLocation);
            GameService.Instance.GetSoundService().PlaySoundEffects(SoundType.PlayerBullet);
        }

        // PowerUp Logic:
        public void SetShieldState(ShieldState shieldStateToSet) => currentShieldState = shieldStateToSet;

        public void ToggleDoubleTurret(bool doubleTurretActive) => currentWeaponMode = doubleTurretActive ? WeaponMode.DoubleTurret : WeaponMode.SingleCanon;

        public void ToggleRapidFire(bool rapidFireActive) => currentRateOfFire = rapidFireActive ? playerScriptableObject.rapidFireRate : playerScriptableObject.defaultFireRate;

        public void TakeDamage(int damageToTake)
        {
            if (currentShieldState != ShieldState.Activated)
            {
                currentHealth -= damageToTake;
                GameService.Instance.GetUIService().UpdateHealthUI(currentHealth);
            }

            if (currentHealth <= 0)
                GameService.Instance.StartGameCoroutine(PlayerDeath());
        }

        private IEnumerator PlayerDeath()
        {
            Object.Destroy(playerView.gameObject);
            GameService.Instance.GetVFXService().PlayVFXAtPosition(VFXType.PlayerExplosion, playerView.transform.position);
            GameService.Instance.GetSoundService().PlaySoundEffects(SoundType.PlayerDeath);

            currentShootingState = ShootingState.NotFiring;
            GameService.Instance.GetEnemyService().SetEnemySpawning(false);
            GameService.Instance.GetPowerUpService().SetPowerUpSpawning(false);

            // Wait for Player Ship Destruction.
            yield return new WaitForSeconds(playerScriptableObject.deathDelay);
            GameService.Instance.GetUIService().EnableGameOverUI();
        }

        public Vector3 GetPlayerPosition() => playerView != null ? playerView.transform.position : default;

        // Enums
        private enum WeaponMode
        {
            SingleCanon,
            DoubleTurret
        }

        private enum ShootingState
        {
            Firing,
            NotFiring
        }
    }

    public enum ShieldState
    {
        Activated,
        Deactivated
    }
}