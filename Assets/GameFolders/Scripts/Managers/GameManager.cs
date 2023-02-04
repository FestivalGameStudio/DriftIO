using UnityEngine;

public class GameManager : MonoSingleton<GameManager>
{
    
    public int PlayerScore
    {
        get => PlayerPrefs.GetInt("PlayerScore", 0);
        set => PlayerPrefs.SetInt("PlayerScore", value);
    }

    public int EnemyScore
    {
        get => PlayerPrefs.GetInt("EnemyScore", 0);
        set => PlayerPrefs.SetInt("EnemyScore", value);
    }
    
    private void Awake()
    {
        Singleton(true);
    }
    
}
