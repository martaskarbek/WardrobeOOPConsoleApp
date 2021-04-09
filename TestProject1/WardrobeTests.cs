using System;
using System.Collections.Generic;
using NUnit.Framework;
using WardrobeNamespace.Clothing;
using WardrobeNamespace;

namespace TestProject1
{
    [TestFixture]
    public class WardrobeTests
    {
          private Wardrobe _wardrobe;

        [SetUp]
        public void Setup()
        {
            _wardrobe = new Wardrobe(3);
        }

        [Test]
        public void TestConstructWithLimitSetLimit()
        {
             var wardrobe = new Wardrobe(5);

             Assert.AreEqual(5, wardrobe.Limit);
        }

        [Test]
        public void TestConstructOverAllowedLimitThrowArgumentOutOfRangeException()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => new Wardrobe(500));
        }

        [Test]
        public void TestCountNoHangerReturnsZero()
        {
            Assert.AreEqual(0, _wardrobe.Count);
        }

        [Test]
        public void TestPutEmptyHangersIncreaseCountAccordingly()
        {
            _wardrobe.Put(new ShirtHanger());
            _wardrobe.Put(new PantHanger());

            Assert.AreEqual(2, _wardrobe.Count);
        }

        [Test]
        public void TestPutHangersWithClothesIncreaseCountAccordingly()
        {
            var hanger1 = new ShirtHanger();
            var hanger2 = new PantHanger();

            hanger1.Put(Shirt);
            hanger2.Put(Trousers);

            _wardrobe.Put(hanger1);
            _wardrobe.Put(hanger2);

            Assert.AreEqual(2, _wardrobe.Count);
        }

        [Test]
        public void TestGetHangerShirtReturnsShirtHanger()
        {
            var hanger = new ShirtHanger();
            _wardrobe.Put(hanger);
            var received = _wardrobe.GetHanger(ClothesType.Shirt);

            Assert.AreEqual(hanger, received);
        }

        [Test]
        public void TestGetHangerRequestedShirtTypeHasOnlyFreePantHangerReturnsPantHanger()
        {
            var hanger = new ShirtHanger();
            hanger.Put(Shirt);

            var hanger2 = new PantHanger();
            _wardrobe.Put(hanger);
            _wardrobe.Put(hanger2);

            var received = _wardrobe.GetHanger(ClothesType.Shirt);

            Assert.AreEqual(hanger2, received);
        }

        [Test]
        public void TestGetHangerNoSuchHangerThrowsInvalidOperationException()
        {
            var hanger = new ShirtHanger();
            hanger.Put(Shirt);

            _wardrobe.Put(hanger);

            Assert.Throws<InvalidOperationException>(() => _wardrobe.GetHanger(ClothesType.Shirt));
        }

        [Test]
        public void TestGetClothesNotInWardrobeThrowsInvalidOperationException()
        {
            var hanger = new ShirtHanger();
            var shirt = Shirt;
            var clothesId = shirt.Id;

            _wardrobe.Put(hanger);

            Assert.Throws<InvalidOperationException>(() => _wardrobe.GetClothes(clothesId));
        }

        [Test, TestCaseSource(nameof(ProvideUpperClothes))]
        public void TestGetClothesUpperClothesInWardrobeOnShirtHangerReturnsWithUpperClothes(Clothes clothes, Guid id)
        {
            var hanger = new ShirtHanger();
            hanger.Put(clothes);
            _wardrobe.Put(hanger);

            var received = _wardrobe.GetClothes(id);

            Assert.AreEqual(clothes, received);
        }

        [Test, TestCaseSource(nameof(ProvideLowerClothes))]
        public void TestGetClothesLowerClothesInWardrobeOnPantHangerReturnsWithLowerClothes(Clothes clothes, Guid id)
        {
            var hanger = new PantHanger();
            hanger.Put(clothes);
            _wardrobe.Put(hanger);

            var received = _wardrobe.GetClothes(id);

            Assert.AreEqual(clothes, received);
        }

        public static IEnumerable<TestCaseData> ProvideUpperClothes
        {
            get
            {
                var shirt = Shirt;
                var blouse = Blouse;

                yield return new TestCaseData(shirt, shirt.Id).SetName("TestGetClothesUpperClothesInWardrobeOnShirtHangerReturnsWithUpperClothesShirt");
                yield return new TestCaseData(blouse, blouse.Id).SetName("TestGetClothesUpperClothesInWardrobeOnShirtHangerReturnsWithUpperClothesBlouse");
            }
        }

        public static IEnumerable<TestCaseData> ProvideLowerClothes
        {
            get
            {
                var trousers = Trousers;
                var skirt = Skirt;

                yield return new TestCaseData(trousers, trousers.Id).SetName("TestGetClothesLowerClothesInWardrobeOnPantHangerReturnsWithLowerClothesTrousers");
                yield return new TestCaseData(skirt, skirt.Id).SetName("TestGetClothesLowerClothesInWardrobeOnPantHangerReturnsWithLowerClothesSkirt");
            }
        }

        public static Clothes Shirt => new UpperClothes("Adidas", ClothesType.Shirt);
        public static Clothes Blouse => new UpperClothes("Louis Vuitton", ClothesType.Blouse);
        public static Clothes Trousers => new LowerClothes("Adidas", ClothesType.Trousers);
        public static Clothes Skirt => new LowerClothes("Louis Vuitton", ClothesType.Skirt);
    }
}