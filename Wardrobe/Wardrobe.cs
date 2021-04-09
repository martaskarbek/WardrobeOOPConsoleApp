using System;
using System.Collections.Generic;
using WardrobeNamespace.Clothing;

namespace WardrobeNamespace
{
    public class Wardrobe
    {
        public int Limit { get; }
        public List<IHanger<Clothes>> wardrobe;
        public Wardrobe(int limit)
        {
            if (limit > 120)
            {
                throw new ArgumentOutOfRangeException("Maximum limit is 120.");
            }
            Limit = limit;
            wardrobe = new List<IHanger<Clothes>>();
        }

        public void Put(IHanger<Clothes> hanger)
        {
            Func<int, int, bool> isFreeSlot = (limit, count) => limit < count;
            if (!isFreeSlot(Limit, Count))
            {
                wardrobe.Add(hanger);
                Count += 1;
            }
            else
            {
                throw new ArgumentOutOfRangeException("The wardrobe currently is full.");
            }
        }

        public IHanger<Clothes> GetHanger(ClothesType type)
        {
            IHanger<Clothes> hanger = null;
            Func<ClothesType, IHanger<Clothes>, bool> isFreeHanger = (type, IHanger) => IHanger.HasSlotFor(type);
            foreach (var _hanger in wardrobe)
            {
                if (isFreeHanger(type, _hanger))
                {
                    hanger = _hanger;
                }
            }

            if (hanger == null)
            {
                throw new InvalidOperationException("No empty hangers for your clothes");
            }
            return hanger;
        }

        public Clothes GetClothes(Guid clothesId)
        {
            Clothes clothe = null;

            for (int i = 0; i < wardrobe.Count; i++)
            {
                if (wardrobe[i].TakeOff(clothesId) != null)
                {
                    clothe = wardrobe[i].TakeOff();
                }
            }

            if (clothe == null)
            {
                throw new InvalidOperationException("Clothes not found.");
            }

            return clothe;
        }

        public int Count { get; set; } = 0;
    }
}
