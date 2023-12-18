using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeartBar : MonoBehaviour
{
    public GameObject HeartPrefab;
    public PlayerHealth pHealth;
    List<Heart> hearts = new List<Heart>();

    private void Start()
    {
        DrawHearts();
    }

    private void OnEnable()
    {
        PlayerHealth.OnPlayerDamaged += DrawHearts;
    }

    private void OnDisable()
    {
        PlayerHealth.OnPlayerDamaged -= DrawHearts;
    }

    public void DrawHearts()
    {
        ClearHearts();

        var maxHealthRemainder = pHealth.HealthMax % 2;
        var heartsToMake = (int)(pHealth.HealthMax / 2 + maxHealthRemainder);

        for (var i = 0; i < heartsToMake; i++)
        {
            CreateEmptyHeart();
        }

        for (var i =0; i < hearts.Count; i++)
        {
            var heartStatusRemainder = (int)Mathf.Clamp(pHealth.Health - (i * 2), 0, 2);
            hearts[i].SetHeartImage((HeartStatus)heartStatusRemainder);
        }
    }

    public void CreateEmptyHeart()
    {
        GameObject newHeart = Instantiate(HeartPrefab);
        newHeart.transform.SetParent(transform);

        Heart heart = newHeart.GetComponent<Heart>();
        heart.SetHeartImage(HeartStatus.Empty);
        hearts.Add(heart);
    }

    public void ClearHearts()
    {
        foreach (Transform t in transform)
        {
            Destroy(t.gameObject);
        }

        hearts = new List<Heart>();
    }
}
