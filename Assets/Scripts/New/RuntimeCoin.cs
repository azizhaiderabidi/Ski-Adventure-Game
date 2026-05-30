using UnityEngine;

public class RuntimeCoin : MonoBehaviour
{
    private CoinSpawnDataSO data;

    public void Setup(CoinSpawnDataSO data)
    {
        this.data = data;
    }

    private void OnTriggerEnter(Collider other)
    {
       // Debug.Log(other.gameObject.name);

        if (other.CompareTag("Player") || other.CompareTag("Head"))
        {
            //if (data.collectVFX == ParticleEffect.StoneExplodion)
            //    Instantiate(data.collectVFX, transform.position, Quaternion.identity);

            if (data.collectSound)
                SoundManager.Instance.PlaySFX(data.collectSound ,.025f, .85f , 1.15f);

           // Debug.Log("Coin");
            CoinEffect.instance.EmitInWorld(transform.position , 1 * PowerUpHandler.CoinMulti);
            EventManager.Raise(new GameEvents.ScoreAdded(data.coinValue * PowerUpHandler.CoinMulti));
            gameObject.SetActive(false);
        }
    }
}   
