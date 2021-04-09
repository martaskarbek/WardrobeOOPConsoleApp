using System;
using System.Collections.Generic;
using NUnit.Framework;
using WardrobeNamespace.Clothing;

namespace TestProject1
{
    [TestFixture]
    public class ClothesTests
    {
        [Test]
        public void TestConstructorCreatesId()
        {
            var tShirt = new Clothes("");
            var blouse = new Clothes("");

            Assert.AreNotEqual(Guid.Empty, tShirt.Id);
            Assert.AreNotEqual(blouse.Id, tShirt.Id);
        }

        [Test]
        public void TestContructorWithBrandNameSetBrandName()
        {
            var tShirt = new Clothes("Adidas");
            Assert.AreEqual("Adidas", tShirt.BrandName);
        }

        [Test, TestCaseSource(nameof(ProvideClothesWithValidTypes))]
        public void TestClothesTypeSet(Clothes clothes, ClothesType expectedType)
        {
            Assert.AreEqual(expectedType, clothes.Type);
        }

        [Test, TestCaseSource(nameof(ProvideClothesWithInvalidTypes))]
        public void TestClothesInvalidTypeSet(bool upper, ClothesType type)
        {
            if (upper)
            {
                Assert.Throws<ArgumentException>(() => new UpperClothes("Adidas", type));
            }
            else
            {
                Assert.Throws<ArgumentException>(() => new LowerClothes("Adidas", type));
            }
        }

        public static IEnumerable<TestCaseData> ProvideClothesWithValidTypes
        {
            get
            {
                yield return new TestCaseData(new UpperClothes("Adidas", ClothesType.Shirt), ClothesType.Shirt).SetName("TestUpperClothesTypeShirtSetTypeAccordingly");
                yield return new TestCaseData(new UpperClothes("Adidas", ClothesType.Blouse), ClothesType.Blouse).SetName("TestUpperClothesTypeBlouseSetTypeAccordingly");
                yield return new TestCaseData(new LowerClothes("Adidas", ClothesType.Skirt), ClothesType.Skirt).SetName("TestLowerClothesTypeSkirtSetTypeAccordingly");
                yield return new TestCaseData(new LowerClothes("Adidas", ClothesType.Trousers), ClothesType.Trousers).SetName("TestLowerClothesTypeTrousersSetTypeAccordingly");
            }
        }

        public static IEnumerable<TestCaseData> ProvideClothesWithInvalidTypes
        {
            get
            {
                yield return new TestCaseData(true, ClothesType.Trousers).SetName("TestUpperClothesLowerType1ThrowsArgumentException");
                yield return new TestCaseData(true, ClothesType.Skirt).SetName("TestUpperClothesLowerType2ThrowsArgumentException");
                yield return new TestCaseData(false, ClothesType.Shirt).SetName("TestLowerClothesUpperType1ThrowsArgumentException");
                yield return new TestCaseData(false, ClothesType.Blouse).SetName("TestLowerClothesUpperType2ThrowsArgumentException");
            }
        }
    }
}