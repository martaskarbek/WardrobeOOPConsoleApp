using System;
using WardrobeNamespace.Clothing;

namespace WardrobeNamespace
{
    class Program
    {
        static void Main(string[] args)
        {
            Wardrobe wardrobe = new Wardrobe(30);
            IHanger<Clothes> emptyHanger = new ShirtHanger();
            IHanger<Clothes> emptyHanger2 = new ShirtHanger();
            IHanger<Clothes> emptyHanger3 = new PantHanger();
            Clothes shirt = new UpperClothes("Versace", ClothesType.Shirt);
            Clothes pants = new LowerClothes("Nike", ClothesType.Trousers);
            emptyHanger3.Put(pants);
            emptyHanger2.Put(shirt);
            wardrobe.Put(emptyHanger);
            wardrobe.Put(emptyHanger2);
            wardrobe.Put(emptyHanger3);
            Console.WriteLine("Clothes in the wardrobe:");
            Console.WriteLine(wardrobe.GetClothes(pants.Id).BrandName);
            Console.WriteLine(wardrobe.GetClothes(shirt.Id).BrandName);
        }
    }
}
