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
        /// Количество элементов для генерации псевдослучайной последовательности цветов
        /// Используется для проверки сортировки SortColorList
        /// Используется для проверки стандартной сортировки Sort и сравнения времени работы сортировок 
        /// </summary>
        private const int RandomColorItemCount = 60 << 10;

        [SetUp]
        public void Setup()
        {
        }

        /// <summary>
        /// TestCase: Проверка конструктора ColorItem(char) для допустимых символов 
        /// </summary>
        /// <param name="colorChar">Символ цвета</param>
        [TestCase('К')]
        [TestCase('к')]
        [TestCase('З')]
        [TestCase('з')]
        [TestCase('С')]
        [TestCase('с')]
        public void ColorItem_ValidChar_Test(char colorChar)
        {
            var item = new ColorItem(colorChar);

            switch (colorChar)
            {
                case 'К':
                    Assert.IsTrue(item.ValueColor == KnownColor.Red);
                    break;
                case 'к':
                    Assert.IsTrue(item.ValueColor == KnownColor.Red);
                    break;
                case 'З':
                    Assert.IsTrue(item.ValueColor == KnownColor.Green);
                    break;
                case 'з':
                    Assert.IsTrue(item.ValueColor == KnownColor.Green);
                    break;
                case 'С':
                    Assert.IsTrue(item.ValueColor == KnownColor.Blue);
                    break;
                case 'с':
                    Assert.IsTrue(item.ValueColor == KnownColor.Blue);
                    break;
            }
        }

        /// <summary>
        /// TestCase: Проверка конструктора ColorItem(char) для недопустимых символов
        /// </summary>
        /// <param name="colorChar">Символ цвета</param>
        [TestCase('R')] // английский
        [TestCase('Ц')] // русский
        [TestCase(' ')] // пробел
        [TestCase('\t')] // табуляция
        [TestCase('?')] // спецсимвол
        public void ColorItem_NotValidChar_Test(char colorChar)
        {
            Assert.Throws<ArgumentException>(() => new ColorItem(colorChar));
        }

        /// <summary>
        /// Test: Проверка конструктора ColorItemList(string) для допустимой строки
        /// </summary>
        [Test]
        public void ColorItemList_ValidStr_Test()
        {
            var inputStr = "КССЗКССЗК";
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
        /// Test: Проверка конструктора ColorItemList(string) для недопустимой строки
        /// </summary>
        [Test]
        public void ColorItemList_NotValidStr_Test()
        {
            var inputStr = "RCP ";
            Assert.Throws<ArgumentException>(() => new ColorItemList(inputStr));
        }


        /// <summary>
        /// TestCase: Сортировка SortColorList: Всевозможные правила сортировки
        /// </summary>
        /// <param name="inputStr">Входная строка </param>
        /// <param name="order">Правило сортировки </param>
        [TestCase("КЗС", ColorOrder.КЗС)]
        [TestCase("КЗС", ColorOrder.КСЗ)]
        [TestCase("КЗС", ColorOrder.ЗКС)]
        [TestCase("КЗС", ColorOrder.ЗСК)]
        [TestCase("КЗС", ColorOrder.СКЗ)]
        [TestCase("КЗС", ColorOrder.СЗК)]
        public void SortList_AllRulesOfSort_Test(string inputStr, ColorOrder order)
        {
            var expectedStr = order.ToString();
            var colorInput = new ColorItemList(inputStr);
            var colorExpected = new ColorItemList(expectedStr);

            colorInput.SortColorList(order);
            bool result = colorInput.Equals(colorExpected); 

            Assert.IsTrue(result, "Всевозможные правила сортировки для короткой валидной строки");
        }

        /// <summary>
        /// TestCase: Сортировка SortColorList: Валидные входные строки
        /// </summary>
        /// <param name="inputStr">Входная строка для создания списка цветов</param>
        /// <param name="order">Правило сортировки</param>
        /// <param name="expectedStr">Ожидаемая строка после сортировки</param>
        [TestCase("ККККККК", ColorOrder.ЗКС, "ККККККК")]
        [TestCase("ЗЗЗЗССС", ColorOrder.КСЗ, "СССЗЗЗЗ")]
        [TestCase("КЗСКЗСКЗСК", ColorOrder.СКЗ, "СССККККЗЗЗ")]
        [TestCase("", ColorOrder.СКЗ, "")]
        public void SortList_ShortValidStr_Test(string inputStr, ColorOrder order, string expectedStr)
        {
            var colorInput = new ColorItemList(inputStr);
            var colorExpected = new ColorItemList(expectedStr);

            colorInput.SortColorList(order);
            bool result = colorInput.Equals(colorExpected); 

            Assert.IsTrue(result, "Валидные строки");
        }

        /// <summary>
        /// Функция сравнения элементов ColorItem
        /// Является необходимым параметром для стандартной сортировки Sort (стандартная используется для сравнения времени работы)
        /// Сравнение согласно правилу Красный Зеленый Синий
        /// </summary>
        /// <param name="x">Первый элемент ColorItem</param>
        /// <param name="y">Второй элемент ColorItem</param>
        /// <returns>Результат сравнения: 0 (если равны) -1 (если меньше) 1 (если больше) </returns>
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
        /// Создание псевдослучайной строки цветов заданной длины
        /// </summary>
        /// <param name="colorItemCount">Количество элементов в строке</param>
        /// <returns>inputStr - случайная входная строка, expectedStr - ожидаемая строка после сортировки согласно правилу КЗС</returns>
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
                        inputRandomStr.Append("К");
                        break;
                    case 1:
                        greenCounter++;
                        inputRandomStr.Append("З");
                        break;
                    case 2:
                        blueCounter++;
                        inputRandomStr.Append("С");
                        break;
                }
            }

            var redStr = new string('К', redCounter);
            var greenStr = new string('З', greenCounter);
            var blueStr = new string('С', blueCounter);

            string expectedStr = redStr + greenStr + blueStr;

            return (inputRandomStr.ToString(), expectedStr);
        }

        /// <summary>
        /// Test: Проверка сортировки SortColorList для большой последовательности цветов согласно правилу КЗС
        /// Дополнительно запускается проверка стандартной сортировки Sort для сравнения времени выполнения сортировок
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

            colorInput.SortColorList(ColorOrder.КЗС);

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

            Assert.IsTrue(result, "Длинная псевдослучайная строка");
            Assert.Pass($"Время выполнения SortColorList (колич. эл-ов {RandomColorItemCount}) {elapsedTimeSortList}  \nВремя выполнения Sort {elapsedTimeSort}");
        }
    }
}