using System;

namespace LethalConfig.ConfigItems.Options
{
    public class BaseRangeOptions<T> : BaseOptions where T : IComparable<T>, IEquatable<T>
    {
        internal bool IsMinSet => !Min.Equals(default);
        internal bool IsMaxSet => !Max.Equals(default);
        public T Min { get; set; }
        public T Max { get; set; }
    }
}