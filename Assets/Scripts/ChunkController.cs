using Sirenix.OdinInspector;
using UnityEngine;

public class ChunkController : MonoBehaviour
{
    public ParentItemContainer[] itemParents; // Sare item parents ka array (Collectables Parent, Obstacles Parent etc.)
    public GameObject obstacle;
    [ContextMenu("Spawn Items on Points")]
    [Button]
    public void SpawnItemsOnPoints()
    {
        ClearExistingItems();

        foreach (ParentItemContainer itemParent in itemParents)
        {
            SpawnItemsFromParent(itemParent);
        }
    }

    private void SpawnItemsFromParent(ParentItemContainer itemParent)
    {
        Transform[] points = GetPointsFromParent(itemParent); // Get points directly from parent

      

        for (int i = 0; i < points.Length; i++)
        {
            ItemBehaviour go = Instantiate(obstacle, points[i].position, points[i].rotation, points[i]).GetComponent<ItemBehaviour>();
            go.itemType = itemParent.itemType;
            if(go.itemType == ItemBehaviour.ItemType.Obstacle)
            {
                go.gameObject.GetComponent<MeshRenderer>().enabled = true;
                go.gameObject.transform.GetChild(0).gameObject.SetActive(false);
                go.gameObject.transform.GetChild(1).gameObject.SetActive(false);

            }
            else if (go.itemType == ItemBehaviour.ItemType.Collectable)
            {
                go.gameObject.GetComponent<MeshRenderer>().enabled = false;
                go.gameObject.transform.GetChild(0).gameObject.SetActive(true);
                go.gameObject.transform.GetChild(1).gameObject.SetActive(false);

            }
            else if (go.itemType == ItemBehaviour.ItemType.destroyAble)
            {
                go.gameObject.GetComponent<MeshRenderer>().enabled = false;
                go.gameObject.transform.GetChild(0).gameObject.SetActive(false);
                go.gameObject.transform.GetChild(1).gameObject.SetActive(true);
            }


        }
    }

    private Transform[] GetPointsFromParent(ParentItemContainer itemParent)
    {
        Transform[] points = new Transform[itemParent.transform.childCount];
        for (int i = 0; i < points.Length; i++)
        {
            points[i] = itemParent.transform.GetChild(i);
        }
        return points;
    }

    private void ClearExistingItems()
    {
        foreach (ParentItemContainer itemParent in itemParents)
        {
            Transform[] points = GetPointsFromParent(itemParent);

            foreach (Transform point in points)
            {
                for (int i = point.childCount - 1; i >= 0; i--)
                {
                    DestroyImmediate(point.GetChild(i).gameObject);
                }
            }
        }
    }
}
