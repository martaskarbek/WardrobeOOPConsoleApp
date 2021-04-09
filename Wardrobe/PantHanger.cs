using System;
using WardrobeNamespace.Clothing;

namespace WardrobeNamespace
{
    public class PantHanger : IHanger<Clothes>
    {
        private Clothes upperClothe;
        private Clothes lowerClothe;

        public void Put(Clothes item)
        {
            if ((item.Type == ClothesType.Blouse || item.Type == ClothesType.Shirt))
            {
                if (HasSlotFor(item.Type))
                {
                    upperClothe = item;
                }
                else
                {
                    throw new ArgumentOutOfRangeException("You can not put this clothe here!");
                }
            }
            else if ((item.Type == ClothesType.Trousers || item.Type == ClothesType.Skirt))
            {
                if (HasSlotFor(item.Type))
                {
                    lowerClothe = item;
                }
                else
                {
                    throw new ArgumentOutOfRangeException("You can not put this clothe here!");
                }
            }
        }

        public Clothes TakeOff()
        {
            Clothes temp = null;
            if (upperClothe != null && lowerClothe != null)
            {
                temp = upperClothe;
                upperClothe = null;
            }
            else if (upperClothe != null && lowerClothe == null)
            {
                temp = upperClothe;
                upperClothe = null;
            }
            else if (upperClothe == null && lowerClothe != null)
            {
                temp = lowerClothe;
                lowerClothe = null;
            }

            return temp;
        }

        public Clothes TakeOff(Guid id)
        {
            Clothes temp = null;
            if (upperClothe != null && upperClothe.Id == id)
            {
                temp = upperClothe;
            }
            if (lowerClothe != null && lowerClothe.Id == id)
            {
                temp = lowerClothe;
            }

            return temp;
        }

        public bool HasSlotFor(ClothesType type)
        {
            Func<Clothes, ClothesType, bool> predicate = (Clothes, type) => Clothes == null;
            if (type == ClothesType.Blouse || type == ClothesType.Shirt)
            {
                return predicate(upperClothe, type);
            }
            if (type == ClothesType.Skirt || type == ClothesType.Trousers)
            {
                return predicate(lowerClothe, type);
            }

            return true;
        }
    }
}
