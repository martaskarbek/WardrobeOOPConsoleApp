using System;
using WardrobeNamespace.Clothing;

namespace WardrobeNamespace
{
    public interface IHanger<T> where T : Clothes
    {
        T TakeOff();
        T TakeOff(Guid id);
        void Put(T item);
        bool HasSlotFor(ClothesType type);
    }
}
