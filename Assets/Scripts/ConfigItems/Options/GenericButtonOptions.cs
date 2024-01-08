
namespace LethalConfig.ConfigItems.Options
{
    public sealed class GenericButtonOptions : BaseOptions
    {
        public string ButtonText { get; set; } = "";

        public delegate void GenericButtonHandler();

        public GenericButtonHandler ButtonHandler { get; set; } = null; 
    }
}
