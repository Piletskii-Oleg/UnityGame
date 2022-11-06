using UnityEngine;

/// <summary>
/// Data for health of an entity in the world.
/// </summary>
[CreateAssetMenu(fileName = "Health")]
public class HealthData : ScriptableObject
{
    [Header("Info")]
    public new string name;

    [Header("Health parameters")]
    public float maxHealth;
    public float currentHealth;

    [Header("Invulnerability")]
    public bool isInvincible;

    [Header("Miscellaneous")]
    [Tooltip("Health ratio at which the critical health vignette starts appearing")]
    public float criticalHealthRatio; // currently unused
}
