using System;

[Serializable]
public class PlayerInfo
{
    public DateTime saveTime;
    public float currentHealth;

    public PlayerInfo(DateTime saveTime, float currentHealth)
    {
        this.saveTime = saveTime;
        this.currentHealth = currentHealth;
    }
}