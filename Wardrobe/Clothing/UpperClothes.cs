using System;

namespace WardrobeNamespace.Clothing
{
    public class UpperClothes : Clothes
    {
        public UpperClothes(string brandName, ClothesType type) : base(brandName)
        {
            if (!type.Equals(ClothesType.Blouse) && !type.Equals(ClothesType.Shirt))
            {
                throw new ArgumentException("This is no proper type of clothing!");
            }
            Type = type;
        }
    }
}
