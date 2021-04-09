using System;
using System.Collections.Generic;
using NUnit.Framework;
using WardrobeNamespace;
using WardrobeNamespace.Clothing;

namespace TestProject1
{
    [TestFixture]
    public class ShirtHangerTests
    {
        private IHanger<Clothes> _hanger;
        private UpperClothes _shirt;

        [SetUp]
        public void Setup()
        {
            _hanger = new ShirtHanger();
            _shirt = new UpperClothes("Adidas", ClothesType.Shirt);
        }

        [Test]
        public void TestTakeOffEmptyHangerReturnsNull()
        {
            Assert.IsNull(_hanger.TakeOff());
        }

        [Test]
        public void TestPutUpperClothesCanBeTakenOff()
        {
            _hanger.Put(_shirt);
            Assert.AreEqual(_shirt, _hanger.TakeOff());
        }

        [Test]
        public void TestPutAlreadyOccupiedHangerThrowsArgumentOutOfRangeException()
        {
            var item2 = new UpperClothes("Adidas", ClothesType.Blouse);

            _hanger.Put(_shirt);
            Assert.Throws<ArgumentOutOfRangeException>(() => _hanger.Put(item2));
        }

        [Test]
        public void TestTakeOffByIdEmptyHangerReturnsNull()
        {
            Assert.IsNull(_hanger.TakeOff(Guid.NewGuid()));
        }

        [Test]
        public void TestTakeOffByIdNotOnHangerReturnsNull()
        {
            _hanger.Put(_shirt);
            Assert.IsNull(_hanger.TakeOff(Guid.NewGuid()));
        }

        [Test]
        public void TakeOffByIdOnHangerReturnsClothes()
        {
            _hanger.Put(_shirt);
            var shirtId = _shirt.Id;

            Assert.AreEqual(_shirt, _hanger.TakeOff(shirtId));
        }

        [Test, TestCaseSource(nameof(ProvideClothesTypes))]
        public void TestHasSlotFor(ClothesType type, bool occupied, bool expected)
        {
            if (occupied)
                _hanger.Put(_shirt);

            if (expected)
            {
                Assert.IsTrue(_hanger.HasSlotFor(type));
            }
            else
            {
                Assert.IsFalse(_hanger.HasSlotFor(type));
            }
        }

        public static IEnumerable<TestCaseData> ProvideClothesTypes
        {
            get
            {
                // Clothes type, occupied, expected
                yield return new TestCaseData(ClothesType.Shirt, false, true).SetName("TestHasSlotForEmptyHangerForUpperClothesReturnsTrue1");
                yield return new TestCaseData(ClothesType.Blouse, false, true).SetName("TestHasSlotForEmptyHangerForUpperClothesReturnsTrue2");
                yield return new TestCaseData(ClothesType.Trousers, false, false).SetName("TestHasSlotForLowerClothesTypesReturnsFalse1");
                yield return new TestCaseData(ClothesType.Skirt, false, false).SetName("TestHasSlotForLowerClothesTypesReturnsFalse2");
                yield return new TestCaseData(ClothesType.Shirt, true, false).SetName("TestHasSlotForOccupiedHangerForUpperClothesReturnsFalse1");
                yield return new TestCaseData(ClothesType.Blouse, true, false).SetName("TestHasSlotForOccupiedHangerForUpperClothesReturnsFalse2");
            }
        }
    }
}