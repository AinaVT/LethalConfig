namespace LethalConfig.ConfigItems.Options
{
    public sealed class GenericButtonOptions : BaseOptions
    {
        public delegate void GenericButtonHandler();

        public string ButtonText { get; set; } = "";

        public GenericButtonHandler ButtonHandler { get; set; }
    }
}