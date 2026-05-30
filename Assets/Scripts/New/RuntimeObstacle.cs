using UnityEngine;

public class RuntimeObstacle : MonoBehaviour
{
    private ObstacleSpawnDataSO data;

    public void Setup(ObstacleSpawnDataSO data)
    {
        this.data = data;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") || other.CompareTag("Head") || other.CompareTag("Hazard"))
        {
            ParticleManager.instance.Play(transform.position , data.collectVFX);

            if (data.collectSound)
                SoundManager.Instance.PlaySFX(data.collectSound, .025f, .85f, 1.15f);


            GameManager.Instance.player.GetComponent<Rigidbody>().linearVelocity = GameManager.Instance.player.GetComponent<Rigidbody>().linearVelocity / 5;
            Debug.Log("ObstacleName :" + gameObject.name + gameObject.transform.position);
            gameObject.SetActive(false);
        }
    }
}

