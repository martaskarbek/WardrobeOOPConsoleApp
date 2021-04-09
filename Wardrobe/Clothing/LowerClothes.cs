using System;

namespace WardrobeNamespace.Clothing
{
    public class LowerClothes : Clothes
    {
        public LowerClothes(string brandName, ClothesType type) : base(brandName)
        {
            if (!type.Equals(ClothesType.Skirt) && !type.Equals(ClothesType.Trousers))
            {
                throw new ArgumentException("This is no proper type of clothing!");
            }
            Type = type;
        }
    }
}
