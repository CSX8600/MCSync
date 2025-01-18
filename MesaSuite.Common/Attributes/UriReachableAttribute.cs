using System;

namespace MesaSuite.Common.Attributes
{
    [AttributeUsage(AttributeTargets.Class)]
    public class UriReachableAttribute : Attribute
    {
        public string UriPath { get; set; }

        public UriReachableAttribute(string uriPath)
        {
            this.UriPath = uriPath;
        }
    }
}
