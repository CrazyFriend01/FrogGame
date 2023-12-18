using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HeartBarEnemy : MonoBehaviour
{
    public GameObject SmallHeartPrefab;
    public Health eHealth;
    public GridLayoutGroup HealthBar;
    List<Heart> hearts = new List<Heart>();

    private void Start()
    {
        DrawHearts();
    }

    private void OnEnable()
    {
        Health.OnDamaged += DrawHearts;
    }

    private void OnDisable()
    {
        Health.OnDamaged -= DrawHearts;
    }

    public void DrawHearts()
    {
        ClearHearts();

        var maxHealthRemainder = eHealth.MaxHealth % 2;
        var heartsToMake = (int)(eHealth.MaxHealth / 2 + maxHealthRemainder);

        for (var i = 0; i < heartsToMake; i++)
        {
            CreateEmptyHeart();
        }

        for (var i = 0; i < hearts.Count; i++)
        {
            var heartStatusRemainder = (int)Mathf.Clamp(eHealth.HealthCurrent - (i * 2), 0, 2);
            hearts[i].SetHeartImage((HeartStatus)heartStatusRemainder);
        }
    }

    public void CreateEmptyHeart()
    {
        GameObject newHeart = Instantiate(SmallHeartPrefab);
        newHeart.transform.SetParent(transform);
        newHeart.transform.localScale = new Vector3(1f, 1f);

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
