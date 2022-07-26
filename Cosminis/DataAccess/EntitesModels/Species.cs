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
        /// <summary>
        /// Takes a ElementSpecies and return the string equivalent of the enumeration
        /// </summary>
        /// <param name="elementSpecies"></param>
        /// <returns>String of the particular element</returns>
        /// <exception cref="ElementNotFound">Occurs if no element exist matching the given input</exception>
        public string ElementToString(ElementSpecies elementSpecies)                                   
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

            if(dictElement.ContainsKey(elementSpecies))
            {
                return dictElement[elementSpecies];
            };
            return "NotFound";
        }

    }
}
