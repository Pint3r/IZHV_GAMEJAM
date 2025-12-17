using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class HealtHeartsScript : MonoBehaviour
{
    public GameObject heartPrefab;
    public PlayerHealth playerHealth;

    List<HeartHealth> hearts = new List<HeartHealth>();

    private void OnEnable()
    {
        PlayerHealth.OnPlayerDamaged += DrawHearts;
    }
    private void OnDisable()
    {
        PlayerHealth.OnPlayerDamaged -= DrawHearts;
    }


    private void Start()
    {
        DrawHearts();
    }
    public void DrawHearts()
    {
        //8hp --> 4hearts
        ClearHearts();

        //determine total hearts based on max health

        float maxHeathRemainder = playerHealth.maxHealth % 2;
        int heartsToMake = (int)((playerHealth.maxHealth/2)+maxHeathRemainder);

        for (int i = 0; i < heartsToMake; i++)
        {
            CreateEmptyHeart(); //makes total hearts needed
        }

        for (int i = 0; i < hearts.Count; i++) {
            //crazy math that i definetly did not get from a tutorial :DD 
            int heartStatusRemainder = (int)Mathf.Clamp(playerHealth.health - (i * 2), 0, 2);
            hearts[i].SetHeartImage((HeartStatus)heartStatusRemainder); //temeraf
        }
        
    }
    public void CreateEmptyHeart()
    {
        GameObject newHeart = Instantiate(heartPrefab);
        newHeart.transform.SetParent(transform);

        HeartHealth heartComponent = newHeart.GetComponent<HeartHealth>();
        heartComponent.SetHeartImage(HeartStatus.Empty);
        hearts.Add(heartComponent);
    }
    public void ClearHearts()
    {
        foreach(Transform t in transform)
        {
            Destroy(t.gameObject);
        }
        hearts = new List<HeartHealth>();
    }

}
