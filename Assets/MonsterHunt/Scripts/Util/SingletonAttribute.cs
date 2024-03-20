using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


[AttributeUsage(AttributeTargets.Class, Inherited = true)]
public class SingletonAttribute : Attribute
{
    public readonly string Name;
    public readonly bool IsDontDestroy;

    public SingletonAttribute(string name, bool isDontDestroy)
    {
        Name = name;
        IsDontDestroy = isDontDestroy;
    }

    public SingletonAttribute(string name)
    {
        Name = name;
        IsDontDestroy = false;
    }
}