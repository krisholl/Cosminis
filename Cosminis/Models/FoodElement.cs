using System;
using System.Collections.Generic;

namespace Models
{
    public enum ElementOfFood                          
    {
        Spicy, Cold, Leafy, Fluffy, Blessed, Cursed
    }

    public class FoodElementWithEnum
    {
        /// <summary>
        /// Takes an ElementOfFood and return the string equivalent of the enumeration
        /// </summary>
        /// <param name="ElementOfFood"></param>
        /// <returns>String of the particular element</returns>
        /// <exception cref="ElementNotFound">Occurs if no element exist matching the given input</exception>
        public string FoodElementToString(ElementOfFood foodElement)                                   
        {
            Dictionary<ElementOfFood,string> dictElement = new Dictionary<ElementOfFood, string>()
            {
                {ElementOfFood.Spicy, "Spicy"},
                {ElementOfFood.Cold, "Cold"},
                {ElementOfFood.Leafy, "Leafy"},
                {ElementOfFood.Fluffy, "Fluffy"},
                {ElementOfFood.Blessed, "Blessed"},
                {ElementOfFood.Cursed, "Cursed"}
            };

            if(dictElement.ContainsKey(foodElement))
            {
                return dictElement[foodElement];
            };
            return "NotFound";
        }

        public FoodElementWithEnum()
        {

        }

        public int FoodElementId { get; set; }
        public ElementOfFood foodElement { get; set; }

        public override string ToString()
        { 
            return $"FoodElementId: {this.FoodElementId}, FoodElement: {FoodElementToString(foodElement)}";
        }  
    }
}