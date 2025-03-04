using JetBrains.Annotations;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] GameObject destroyedVFX;
    [SerializeField] int hitPoints = 2;
    [SerializeField] int score = 0;

    Score scoreIncrease;

    private void Start()
    {
        scoreIncrease = FindFirstObjectByType<Score>();
    }
    private void OnParticleCollision(GameObject other)
    {
        hitPoints --;
        if (hitPoints <= 0)
        {
            Instantiate(destroyedVFX, transform.position, Quaternion.identity);
            Destroy(this.gameObject);
            scoreIncrease.IncreaseScore(score);
        }
      
    }
}
