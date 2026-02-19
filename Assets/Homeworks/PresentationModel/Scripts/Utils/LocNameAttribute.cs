using System;

[AttributeUsage(AttributeTargets.Field, AllowMultiple = false)]
public class LocNameAttribute : Attribute
{
    public string Key { get; }

    public LocNameAttribute(string key)
    {
        Key = key;
    }

    public LocNameAttribute()
    {
        Key = null;
    }
}
