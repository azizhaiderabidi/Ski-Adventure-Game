using UnityEngine;

public class ItemBehaviour : MonoBehaviour
{
    public enum ItemType
    {
        Collectable,
        Obstacle,
        MovingObstacle,
        head,
        destroyAble
    }

    public ItemType itemType;
    public float moveSpeed = 2f;
    public float moveDistance = 3f;
    public int coinValue = 10; // Reward for collecting
    public int obstacleDamage = 1; // Damage to player if hit
    private Vector3 startPosition;
    private bool movingForward = true;

    void Start()
    {
        startPosition = transform.position;
    }

    void Update()
    {
        if (itemType == ItemType.MovingObstacle)
        {
            MoveObstacle();
        }
    }

    void MoveObstacle()
    {
        float movement = moveSpeed * Time.deltaTime;
        if (movingForward)
        {
            transform.position += Vector3.right * movement;
            if (Vector3.Distance(startPosition, transform.position) >= moveDistance)
            {
                movingForward = false;
            }
        }
        else
        {
            transform.position -= Vector3.right * movement;
            if (Vector3.Distance(startPosition, transform.position) <= 0)
            {
                movingForward = true;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            switch (itemType)
            {
                case ItemType.Collectable:
                    Collect();
                    break;
                case ItemType.Obstacle:
                    HitObstacle();
                    break;
                case ItemType.MovingObstacle:
                    HitObstacle();
                    break;
                case ItemType.destroyAble:
                    Destruction();
                    break;
                //case ItemType.Finish:
                //    GameManager.Instance.LevelComplete();
                //    break;
            }
        }
    }

    void Collect()
    {
        Debug.Log("Collected item! Coins: +" + coinValue);
        GameManager.Instance.AddCoins(1);
        // Add coin value to player's score (implement in Player script)
        Destroy(gameObject);
    }

    void HitObstacle()
    {
        Debug.Log("Hit an obstacle! Damage: -" + obstacleDamage);
        // Reduce player health (implement in Player script)
        GameManager.Instance.GameOver();
    }

    void Destruction()
    {
        transform.GetChild(1).GetComponent<EasyDestruction>().Collsion();
    }
}
