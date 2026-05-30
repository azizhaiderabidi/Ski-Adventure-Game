using UnityEngine;

public class RuntimePowerUp : MonoBehaviour
{
    private PowerUpSpawnDataSO data;

    public void Setup(PowerUpSpawnDataSO data)
    {
        this.data = data;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") || other.CompareTag("Head"))
        {
            ParticleManager.instance.Play(transform.position, data.collectVFX);

            if (data.collectSound)
                SoundManager.Instance.PlaySFX(data.collectSound, .025f, .85f, 1.15f);

            PowerUpHandler.ActivatePowerUp(data.powerUpType);
            ParticleManager.instance.Play(transform.position, data.collectVFX);

            gameObject.SetActive(false);
        }
    }
}

