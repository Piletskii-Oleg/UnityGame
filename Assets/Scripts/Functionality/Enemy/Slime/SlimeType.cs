namespace Functionality.Enemy.Slime
{
    public enum SlimeType
    {
        /// <summary>
        /// Doesn't attack back when it is attacked. 
        /// </summary>
        Passive,
        
        /// <summary>
        /// Attacks back when it is attacked.
        /// </summary>
        Neutral,
        
        /// <summary>
        /// Always attacks player if they are nearby.
        /// </summary>
        Aggressive,
    }
}