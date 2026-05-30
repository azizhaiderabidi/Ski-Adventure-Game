using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SignPlacer : MonoBehaviour
{
    [SerializeField] float distance = 100;
    [SerializeField] float distaceTrashHold;
    [SerializeField] MeterSign sign;
    [SerializeField] Vector3 offset;

    float nextDistance = 0;

    List<MeterSign> spawned = new List<MeterSign>();

    private void Start()
    {
        nextDistance = distance;
        Spawn();
    }
    void Update()
    {
        if (GameManager.Instance.score > nextDistance)
        {
            Spawn();
        }
    }

    void Spawn()
    {
        MeterSign sign = Instantiate(this.sign,transform);

        sign.meter.text = nextDistance.ToString("0m");
        sign.transform.position = new Vector3(0, 0, distaceTrashHold * nextDistance) + offset;

        StartCoroutine(Place(sign.gameObject));
        spawned.Add(sign);

        nextDistance = distance * spawned.Count;
    }
    IEnumerator Place(GameObject gameObject)
    {
        bool cast = false;
        RaycastHit hit;
        while (true)
        {
            cast = Physics.Raycast(gameObject.transform.position +
                     (Vector3.up * 1000) + (Vector3.right * 2), Vector3.down,out hit);

            if (cast)
                { 
                    // print("Hit");
                break;
            }
            else
            {
              //  print("No Hit");
            }

            yield return new WaitForEndOfFrame();
        }

       // print("Hit");

        gameObject.transform.position = new Vector3(gameObject.transform.position.x,
            hit.point.y - 25, gameObject.transform.position.z);
    }
}
