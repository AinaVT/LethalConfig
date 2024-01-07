
namespace LethalConfig.ConfigItems.Options
{
    public class BaseOptions
    {
        public string Name { get; set; } = null;

        public string Description { get; set; } = null;

        public string Section { get; set; } = null;

        public bool RequiresRestart { get; set; } = true;
        
        public delegate CanModifyResult CanModifyDelegate();

        /// <summary>
        /// Callback that is used to determine if this config entry can be modified.
        /// <para>This can be used to disallow modifications to certain entries based on the current game state.</para>
        /// <remarks>If left unset the config entry is always allowed to be modified.</remarks>
        /// <returns>A <see cref="CanModifyResult"/> that determines if the config entry can be modified or not.</returns>
        /// </summary>
        public CanModifyDelegate CanModifyCallback { get; set; } = null;
    }
}
