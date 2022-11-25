namespace Shared
{
    /// <summary>
    /// Interface for health of an actor.
    /// </summary>
    public interface IHealth
    {
        /// <summary>
        /// Used when the <see cref="Actor"/> takes damage.
        /// </summary>
        /// <param name="damage">Damage taken.</param>
        void TakeDamage(float damage);

        /// <summary>
        /// Used when the <see cref="Actor"/> is healed.
        /// </summary>
        /// <param name="healAmount">Amount of health to recover.</param>
        void Heal(float healAmount);
    }
}