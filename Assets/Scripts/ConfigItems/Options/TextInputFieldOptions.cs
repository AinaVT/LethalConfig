namespace LethalConfig.ConfigItems.Options
{
    public sealed class TextInputFieldOptions : BaseOptions
    {
        public int CharacterLimit { get; set; }
        public int NumberOfLines { get; set; } = 1;
        public bool TrimText { get; set; }
    }
}