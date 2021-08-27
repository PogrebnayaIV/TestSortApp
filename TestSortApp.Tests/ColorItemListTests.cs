using System;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using NUnit.Framework;
using TestSortApp.Library;

namespace TestSortApp.Tests
{
    public class ColorItemListTests
    {
        /// <summary>
        /// ���������� ��������� ��� ��������� ��������������� ������������������ ������
        /// ������������ ��� �������� ���������� SortColorList
        /// ������������ ��� �������� ����������� ���������� Sort � ��������� ������� ������ ���������� 
        /// </summary>
        private const int RandomColorItemCount = 60 << 10;

        [SetUp]
        public void Setup()
        {
        }

        /// <summary>
        /// TestCase: �������� ������������ ColorItem(char) ��� ���������� �������� 
        /// </summary>
        /// <param name="colorChar">������ �����</param>
        [TestCase('�')]
        [TestCase('�')]
        [TestCase('�')]
        [TestCase('�')]
        [TestCase('�')]
        [TestCase('�')]
        public void ColorItem_ValidChar_Test(char colorChar)
        {
            var item = new ColorItem(colorChar);

            switch (colorChar)
            {
                case '�':
                    Assert.IsTrue(item.ValueColor == KnownColor.Red);
                    break;
                case '�':
                    Assert.IsTrue(item.ValueColor == KnownColor.Red);
                    break;
                case '�':
                    Assert.IsTrue(item.ValueColor == KnownColor.Green);
                    break;
                case '�':
                    Assert.IsTrue(item.ValueColor == KnownColor.Green);
                    break;
                case '�':
                    Assert.IsTrue(item.ValueColor == KnownColor.Blue);
                    break;
                case '�':
                    Assert.IsTrue(item.ValueColor == KnownColor.Blue);
                    break;
            }
        }

        /// <summary>
        /// TestCase: �������� ������������ ColorItem(char) ��� ������������ ��������
        /// </summary>
        /// <param name="colorChar">������ �����</param>
        [TestCase('R')] // ����������
        [TestCase('�')] // �������
        [TestCase(' ')] // ������
        [TestCase('\t')] // ���������
        [TestCase('?')] // ����������
        public void ColorItem_NotValidChar_Test(char colorChar)
        {
            Assert.Throws<ArgumentException>(() => new ColorItem(colorChar));
        }

        /// <summary>
        /// Test: �������� ������������ ColorItemList(string) ��� ���������� ������
        /// </summary>
        [Test]
        public void ColorItemList_ValidStr_Test()
        {
            var inputStr = "���������";
            var itemList = new ColorItemList(inputStr);

            bool result = inputStr.Length == itemList.ColorItems.Count;

            var redCount = itemList.ColorItems.Count(x => x.ValueColor == KnownColor.Red);
            var greenCount = itemList.ColorItems.Count(x => x.ValueColor == KnownColor.Green);
            var blueCount = itemList.ColorItems.Count(x => x.ValueColor == KnownColor.Blue);

            if (!(redCount == 3 && greenCount == 2 && blueCount == 4))
                result = false;

            Assert.IsTrue(result);
        }

        /// <summary>
        /// Test: �������� ������������ ColorItemList(string) ��� ������������ ������
        /// </summary>
        [Test]
        public void ColorItemList_NotValidStr_Test()
        {
            var inputStr = "RCP ";
            Assert.Throws<ArgumentException>(() => new ColorItemList(inputStr));
        }


        /// <summary>
        /// TestCase: ���������� SortColorList: ������������ ������� ����������
        /// </summary>
        /// <param name="inputStr">������� ������ </param>
        /// <param name="order">������� ���������� </param>
        [TestCase("���", ColorOrder.���)]
        [TestCase("���", ColorOrder.���)]
        [TestCase("���", ColorOrder.���)]
        [TestCase("���", ColorOrder.���)]
        [TestCase("���", ColorOrder.���)]
        [TestCase("���", ColorOrder.���)]
        public void SortList_AllRulesOfSort_Test(string inputStr, ColorOrder order)
        {
            var expectedStr = order.ToString();
            var colorInput = new ColorItemList(inputStr);
            var colorExpected = new ColorItemList(expectedStr);

            colorInput.SortColorList(order);
            bool result = colorInput.Equals(colorExpected); 

            Assert.IsTrue(result, "������������ ������� ���������� ��� �������� �������� ������");
        }

        /// <summary>
        /// TestCase: ���������� SortColorList: �������� ������� ������
        /// </summary>
        /// <param name="inputStr">������� ������ ��� �������� ������ ������</param>
        /// <param name="order">������� ����������</param>
        /// <param name="expectedStr">��������� ������ ����� ����������</param>
        [TestCase("�������", ColorOrder.���, "�������")]
        [TestCase("�������", ColorOrder.���, "�������")]
        [TestCase("����������", ColorOrder.���, "����������")]
        [TestCase("", ColorOrder.���, "")]
        public void SortList_ShortValidStr_Test(string inputStr, ColorOrder order, string expectedStr)
        {
            var colorInput = new ColorItemList(inputStr);
            var colorExpected = new ColorItemList(expectedStr);

            colorInput.SortColorList(order);
            bool result = colorInput.Equals(colorExpected); 

            Assert.IsTrue(result, "�������� ������");
        }

        /// <summary>
        /// ������� ��������� ��������� ColorItem
        /// �������� ����������� ���������� ��� ����������� ���������� Sort (����������� ������������ ��� ��������� ������� ������)
        /// ��������� �������� ������� ������� ������� �����
        /// </summary>
        /// <param name="x">������ ������� ColorItem</param>
        /// <param name="y">������ ������� ColorItem</param>
        /// <returns>��������� ���������: 0 (���� �����) -1 (���� ������) 1 (���� ������) </returns>
        private static int CompareColorItems(ColorItem x, ColorItem y)
        {
            if (x.ValueColor == KnownColor.Red)
            {
                if (y.ValueColor == KnownColor.Red)
                    return 0;
                if (y.ValueColor == KnownColor.Green || y.ValueColor == KnownColor.Blue)
                    return -1;
            }
            if (x.ValueColor == KnownColor.Green)
            {
                if (y.ValueColor == KnownColor.Green)
                    return 0;
                if (y.ValueColor == KnownColor.Red)
                    return 1;
                if (y.ValueColor == KnownColor.Blue)
                    return -1;
            }
            if (x.ValueColor == KnownColor.Blue)
            {
                if (y.ValueColor == KnownColor.Blue)
                    return 0;
                if (y.ValueColor == KnownColor.Green || y.ValueColor == KnownColor.Red)
                    return 1;
            }

            return 0;
        }

        /// <summary>
        /// �������� ��������������� ������ ������ �������� �����
        /// </summary>
        /// <param name="colorItemCount">���������� ��������� � ������</param>
        /// <returns>inputStr - ��������� ������� ������, expectedStr - ��������� ������ ����� ���������� �������� ������� ���</returns>
        private (string inputStr, string expectedStr) CreateRandomColorStr(int colorItemCount)
        {
            var inputRandomStr = new StringBuilder();
            var random = new Random();
            int redCounter = 0;
            int greenCounter = 0;
            int blueCounter = 0;

            for (int i = 0; i < colorItemCount; i++)
            {
                int randomNumber = random.Next(0, 3);
                switch (randomNumber)
                {
                    case 0:
                        redCounter++;
                        inputRandomStr.Append("�");
                        break;
                    case 1:
                        greenCounter++;
                        inputRandomStr.Append("�");
                        break;
                    case 2:
                        blueCounter++;
                        inputRandomStr.Append("�");
                        break;
                }
            }

            var redStr = new string('�', redCounter);
            var greenStr = new string('�', greenCounter);
            var blueStr = new string('�', blueCounter);

            string expectedStr = redStr + greenStr + blueStr;

            return (inputRandomStr.ToString(), expectedStr);
        }

        /// <summary>
        /// Test: �������� ���������� SortColorList ��� ������� ������������������ ������ �������� ������� ���
        /// ������������� ����������� �������� ����������� ���������� Sort ��� ��������� ������� ���������� ����������
        /// </summary>
        [Test]
        public void SortColorList_LongValidStr_Test()
        {
            // arrange
            var (inputStr, expectedStr) = CreateRandomColorStr(RandomColorItemCount);

            var colorInput = new ColorItemList(inputStr);
            var colorExpected = new ColorItemList(expectedStr);

            // act
            var stopWatch = new Stopwatch();
            stopWatch.Start();

            colorInput.SortColorList(ColorOrder.���);

            stopWatch.Stop();
            var tsSortList = stopWatch.Elapsed;
            var elapsedTimeSortList = $"{tsSortList.Hours:00}:{tsSortList.Minutes:00}:{tsSortList.Seconds:00}.{tsSortList.Milliseconds:000}";

            bool result = colorInput.Equals(colorExpected);

            colorInput = new ColorItemList(inputStr);
            stopWatch.Start();

            colorInput.ColorItems.Sort(CompareColorItems);

            stopWatch.Stop();
            var tsSort = stopWatch.Elapsed;
            var elapsedTimeSort = $"{tsSort.Hours:00}:{tsSort.Minutes:00}:{tsSort.Seconds:00}.{tsSort.Milliseconds:000}";

            Assert.IsTrue(result, "������� ��������������� ������");
            Assert.Pass($"����� ���������� SortColorList (�����. ��-�� {RandomColorItemCount}) {elapsedTimeSortList}  \n����� ���������� Sort {elapsedTimeSort}");
        }
    }
}