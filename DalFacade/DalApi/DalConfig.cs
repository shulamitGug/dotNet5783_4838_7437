﻿namespace DalApi;
using System.Xml.Linq;
using DO;
static class DalConfig
{
    internal static string? s_dalName;
    internal static Dictionary<string, string> s_dalPackages;
    internal static Dictionary<string, string> s_dalNameSpaces;
    internal static Dictionary<string, string> s_dalClass;

    static DalConfig()
    {
        XElement dalConfig = XElement.Load(@"..\xml\dal-config.xml")
            ?? throw new DalConfigException("dal-config.xml file is not found");
        s_dalName = dalConfig?.Element("dal")?.Value
            ?? throw new DalConfigException("<dal> element is missing");
        var packages = dalConfig?.Element("dal-packages")?.Elements()
            ?? throw new DalConfigException("<dal-packages> element is missing");
        s_dalPackages = packages.ToDictionary(p => "" + p.Name, p => p.Value);
        s_dalNameSpaces = packages.ToDictionary(p => "" + p.Name, p => p.Attributes().FirstOrDefault(x => x.Name == "namespace").Value) ;
        s_dalClass = packages.ToDictionary(p => "" + p.Name, p => p.Attributes().FirstOrDefault(x => x.Name == "class").Value);

    }
}
