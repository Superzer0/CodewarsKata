namespace TopologicalSort
{
    using System;

    [AttributeUsage(AttributeTargets.Class)]
    public class DependsOnAttribute : Attribute
    {
        public DependsOnAttribute(Type dependsOn)
        {
            DependsOn = dependsOn;
        }
        public Type DependsOn { get; }
    }

}

