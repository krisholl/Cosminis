using System;
using System.Collections.Generic;

namespace DataAccess.Entities
{
    public enum ElementSpecies              
    {
        Volcanic, Glacial, Forest, Sky, Holy, Dark
    }
    public partial class Species
    { 
        public string StatusToString(ElementSpecies stringSpecies)                                   
        {
            Dictionary<ElementSpecies,string> dictElement = new Dictionary<ElementSpecies, string>()
            {
                {ElementSpecies.Volcanic, "Volcanic"},
                {ElementSpecies.Glacial, "Glacial"},
                {ElementSpecies.Forest, "Forest"},
                {ElementSpecies.Sky, "Sky"},
                {ElementSpecies.Holy, "Holy"},
                {ElementSpecies.Dark, "Dark"}
            };

            if(dictElement.ContainsKey(stringSpecies))
            {
                return dictElement[stringSpecies];
            };
            return "NotFound";
        }
    }
}
