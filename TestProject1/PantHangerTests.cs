using System;
using System.Collections.Generic;
using NUnit.Framework;
using WardrobeNamespace;
using WardrobeNamespace.Clothing;

namespace TestProject1
{
    [TestFixture]
    public class PantHangerTests
    {
        private IHanger<Clothes> _hanger;
        private Clothes _upperClothes;
        private Clothes _lowerClothes;

        [SetUp]
        public void Setup()
        {
            _hanger = new PantHanger();
            _upperClothes = new UpperClothes("Adidas", ClothesType.Shirt);
            _lowerClothes = new LowerClothes("Adidas", ClothesType.Trousers);
        }

        [Test] 
        public void TestTakeOffEmptyHangerReturnsNull()
        {
            Assert.IsNull(_hanger.TakeOff());
        }

        [Test]
        public void TestPutUpperClothesStoredOnHanger()
        {
            _hanger.Put(_upperClothes);
            Assert.AreEqual(_upperClothes, _hanger.TakeOff());
        }

        [Test]
        public void TestPutUpperClothesAlreadyOccupiedHangerThrowsArgumentOutOfRangeException()
        {
            var item2 = new UpperClothes("Adidas", ClothesType.Blouse);
            _hanger.Put(_upperClothes);

            Assert.Throws<ArgumentOutOfRangeException>(() => _hanger.Put(item2));
        }

        [Test]
        public void TestPutUpperAndLowerClothesAsWellStoredClothes()
        {
            _hanger.Put(_lowerClothes);
            _hanger.Put(_upperClothes);

            var receivedClothes1 = _hanger.TakeOff();
            var receivedClothes2 = _hanger.TakeOff();
            var emptyItem = _hanger.TakeOff();

            Assert.IsNull(emptyItem);
            Assert.AreNotEqual(receivedClothes1, receivedClothes2);
            Assert.IsTrue(receivedClothes1 == _lowerClothes || receivedClothes2 == _lowerClothes);
            Assert.IsTrue(receivedClothes1 == _upperClothes || receivedClothes2 == _upperClothes);
        }

        [Test]
        public void TestPutLowerClothesAlreadyOccupiedThrowsArgumentOutOfRangeException()
        {
            var item2 = new LowerClothes("Prada", ClothesType.Skirt);
            _hanger.Put(_lowerClothes);

            Assert.Throws<ArgumentOutOfRangeException>(() => _hanger.Put(item2));
        }

        [Test]
        public void TestTakeOffOnlyUpperClothesReturnsUpperClothes()
        {
            _hanger.Put(_upperClothes);
            Assert.AreEqual(_upperClothes, _hanger.TakeOff());
        }

        [Test]
        public void TestTakeOffOnlyLowerClothesReturnsLowerClothes()
        {
            _hanger.Put(_lowerClothes);
            Assert.AreEqual(_lowerClothes, _hanger.TakeOff());
        }

        [Test]
        public void TestTakeOffFullyOccupiedHangerReturnsFirstUpperSecondLower()
        {
            _hanger.Put(_upperClothes);
            _hanger.Put(_lowerClothes);

            var firstReceived = _hanger.TakeOff() as UpperClothes;
            var secondReceived = _hanger.TakeOff() as LowerClothes;

            Assert.AreEqual(_upperClothes, firstReceived);
            Assert.AreEqual(_lowerClothes, secondReceived);
        }

        [Test]
        public void TestTakeOffByIdEmptyHangerReturnsNull()
        {
            Assert.IsNull(_hanger.TakeOff(Guid.NewGuid()));
        }

        [Test]
        public void TestTakeOffByIdNotOnHangerReturnsNull()
        {
            _hanger.Put(_upperClothes);
            Assert.IsNull(_hanger.TakeOff(Guid.NewGuid()));
        }

        [Test]
        public void TestTakeOffByIdUpperClothesOnHangerReturnsClothes()
        {
            _hanger.Put(_upperClothes);
            var upperClothesId = _upperClothes.Id;

            Assert.AreEqual(_upperClothes, _hanger.TakeOff(upperClothesId));
        }

        [Test]
        public void TestTakeOffByIdLowerClothesOnHangerReturnsClothes()
        {
            _hanger.Put(_lowerClothes);
            var lowerClothesId = _lowerClothes.Id;

            Assert.AreEqual(_lowerClothes, _hanger.TakeOff(lowerClothesId));
        }

        [Test, TestCaseSource(nameof(ProvideClothesTypes))]
        public void TestHasSlotFor(ClothesType type, bool putUpper, bool putLower, bool expected)
        {
            if (putLower)
                _hanger.Put(_lowerClothes);

            if (putUpper)
                _hanger.Put(_upperClothes);

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
                // Clothes type, put upper, put lower, expected
                yield return new TestCaseData(ClothesType.Shirt, false, false, true).SetName("HasSlotForEmptyHangerForUpperClothesReturnsTrue1");
                yield return new TestCaseData(ClothesType.Blouse, false, false, true).SetName("HasSlotForEmptyHangerForUpperClothesReturnsTrue2");
                yield return new TestCaseData(ClothesType.Shirt, true, false, false).SetName("HasSlotForOccupiedHangerForUpperClothesReturnsFalse1");
                yield return new TestCaseData(ClothesType.Blouse, true, false, false).SetName("HasSlotForOccupiedHangerForUpperClothesReturnsFalse2");
                yield return new TestCaseData(ClothesType.Trousers, false, true, false).SetName("HasSlotForOccupiedHangerForLowerClothesReturnsFalse1");
                yield return new TestCaseData(ClothesType.Skirt, false, true, false).SetName("HasSlotForOccupiedHangerForLowerClothesReturnsFalse2");
                yield return new TestCaseData(ClothesType.Trousers, true, false, true).SetName("HasSlotForSemiOccupiedHangerForLowerClothesReturnsTrue1");
                yield return new TestCaseData(ClothesType.Skirt, true, false, true).SetName("HasSlotForSemiOccupiedHangerForLowerClothesReturnsTrue2");
                yield return new TestCaseData(ClothesType.Trousers, false, false, true).SetName("HasSlotForEmptyHangerForLowerClothesReturnsTrue1");
                yield return new TestCaseData(ClothesType.Skirt, false, false, true).SetName("HasSlotForEmptyHangerForLowerClothesReturnsTrue2");
            }
        }
    }
}