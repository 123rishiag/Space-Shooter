using CosmicCuration.Player;
using System.Collections;
using UnityEngine;

namespace CosmicCuration.PowerUps
{
    public class PowerUpController : IPowerUp
    {
        private PowerUpView powerUpView;
        private float activeDuration;
        private bool isActive;

        public PowerUpController(PowerUpData powerUpData)
        {
            powerUpView = Object.Instantiate(powerUpData.powerUpPrefab);
            powerUpView.SetController(this);
            activeDuration = powerUpData.activeDuration;
        }

        public void Configure(Vector2 spawnPosition)
        {
            isActive = false;
            powerUpView.transform.position = spawnPosition;
            powerUpView.gameObject.SetActive(true);
        }

        public IEnumerator StartTimer()
        {
            if (isActive)
            {
                yield return new WaitForSeconds(activeDuration);
                Deactivate();
            }
        }

        public void PowerUpTriggerEntered(GameObject collidedObject)
        {
            if (collidedObject.GetComponent<PlayerView>() != null)
                Activate();
        }

        public virtual void Activate()
        {
            isActive = true;
            powerUpView.StartCoroutine(StartTimer());
            powerUpView.gameObject.SetActive(false);
        }

        public virtual void Deactivate()
        {
            isActive = false;
            GameService.Instance.GetPowerUpService().ReturnPowerUpToPool(this);
        }
    }
}