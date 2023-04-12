namespace Core.Weapons
{
    /// <summary>
    /// Interface for all weapons.
    /// </summary>
    public interface IWeapon
    {
        /// <summary>
        /// Method that is called when weapon shoots.
        /// </summary>
        void Shoot();

        /// <summary>
        /// Method called when weapon has to reload.
        /// </summary>
        void StartReload();
    }
}
