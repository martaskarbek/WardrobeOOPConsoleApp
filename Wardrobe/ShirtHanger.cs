using System;
using WardrobeNamespace.Clothing;

namespace WardrobeNamespace
{
    public class ShirtHanger : IHanger<Clothes>
    {
        private Clothes _clothe;
        public Clothes TakeOff()
        {
            Clothes temp = null;
            if (_clothe != null)
            {
                temp = _clothe;
                _clothe = null;
            }
            return temp;
        }

        public Clothes TakeOff(Guid id)
        {
            Clothes temp = null;
            if (_clothe != null && _clothe.Id == id)
            {
                temp = _clothe;
            }

            return temp;
        }

        public void Put(Clothes item)
        {
            if (HasSlotFor(item.Type))
            {
                Func<Clothes, Clothes> put = item => _clothe = item;
                put(item);
            }
            else
            {
                throw new ArgumentOutOfRangeException("You can not put this clothe here!");
            }
        }

        public bool HasSlotFor(ClothesType type)
        {
            return (type == ClothesType.Blouse || type == ClothesType.Shirt) && _clothe == null;
        }
    }
}
