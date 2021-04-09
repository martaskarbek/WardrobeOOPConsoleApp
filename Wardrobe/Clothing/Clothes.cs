using System;

namespace WardrobeNamespace.Clothing
{
    public class Clothes
    {
        public Guid Id { get; }
        public string BrandName { get; }
        public ClothesType Type { get; protected set; }

        public Clothes(string brandName)
        {
            BrandName = brandName;
            Id = Guid.NewGuid();
        }
    }
}
