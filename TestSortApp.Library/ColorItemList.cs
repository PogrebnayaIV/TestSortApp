using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace TestSortApp.Library
{
    /// <summary>
    /// Список цветов
    /// </summary>
    public class ColorItemList
    {
        /// <summary>
        /// Список цветов
        /// </summary>
        public List<ColorItem> ColorItems { get; } = new();

        /// <summary>
        /// Конструктор списка цветов из строки
        /// </summary>
        /// <param name="colorStr">Список цветов в виде строки</param>
        public ColorItemList(string colorStr)
        {
            foreach (var s in colorStr)
            {
                var item = new ColorItem(s);
                ColorItems.Add(item);
            }
        }

        /// <summary>
        /// Сортировка неупорядоченной последовательности цветов
        /// Применяется Блочная сортировка
        /// Цвета разбиваются на 3 блока (красные, зеленые, синие) за один проход списка
        /// Затем 3 блока сливаются в один согласно правилу упорядочивания цветов
        /// Скорость работы алгоритма линейная
        /// </summary>
        /// <param name="ruleCode">Правило сортировки, заданное кодом</param>
        public void SortColorList(ColorOrder ruleCode)
        {
            var redList = new List<ColorItem>();
            var greenList = new List<ColorItem>();
            var blueList = new List<ColorItem>();

            foreach (var cItem in ColorItems)
            {
                switch (cItem.ValueColor)
                {
                    case KnownColor.Red:
                        redList.Add(cItem);
                        break;
                    case KnownColor.Green:
                        greenList.Add(cItem);
                        break;
                    case KnownColor.Blue:
                        blueList.Add(cItem);
                        break;
                    default:
                        throw new Exception($"Неизвестное значение: {cItem.ValueColor}");
                }
            }

            ColorItems.Clear();

            switch (ruleCode)
            {
                case ColorOrder.КЗС:
                    ColorItems.AddRange(redList);
                    ColorItems.AddRange(greenList);
                    ColorItems.AddRange(blueList);
                    break;
                case ColorOrder.КСЗ:
                    ColorItems.AddRange(redList);
                    ColorItems.AddRange(blueList);
                    ColorItems.AddRange(greenList);
                    break;
                case ColorOrder.ЗКС:
                    ColorItems.AddRange(greenList);
                    ColorItems.AddRange(redList);
                    ColorItems.AddRange(blueList);
                    break;
                case ColorOrder.ЗСК:
                    ColorItems.AddRange(greenList);
                    ColorItems.AddRange(blueList);
                    ColorItems.AddRange(redList);
                    break;
                case ColorOrder.СКЗ:
                    ColorItems.AddRange(blueList);
                    ColorItems.AddRange(redList);
                    ColorItems.AddRange(greenList);
                    break;
                case ColorOrder.СЗК:
                    ColorItems.AddRange(blueList);
                    ColorItems.AddRange(greenList);
                    ColorItems.AddRange(redList);
                    break;
            }
        }

        /// <summary>
        /// Преобразование списка цветов в строку
        /// </summary>
        /// <returns>Строка цветов, заданная буквами</returns>
        public override string ToString()
        {
            var sb = new StringBuilder();
            foreach (var colorItem in ColorItems)
            {
                sb.Append(colorItem);
            }

            return sb.ToString();
        }

        /// <summary>
        /// Сравнение списков цветов
        /// </summary>
        /// <param name="obj">Список, с которым происходит сравнение</param>
        /// <returns>True, если списки равны</returns>
        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj))
                return false;
            if (ReferenceEquals(this, obj))
                return true;
            if (obj.GetType() != this.GetType())
                return false;

            return Equals((ColorItemList) obj);
        }

        /// <summary>
        /// Поэлементное сравнение списков цветов
        /// </summary>
        /// <param name="other">Список, с которым происходит сравнение</param>
        /// <returns>True, если списки равны</returns>
        protected bool Equals(ColorItemList other)
        {
            if (other.ColorItems.Count != ColorItems.Count)
                return false;

            for (int i = 0; i < other.ColorItems.Count; i++)
            {
                if (!other.ColorItems[i].Equals(this.ColorItems[i]))
                    return false;
            }

            return true;
        }

        /// <summary>
        /// Получение HashCode
        /// </summary>
        /// <returns>HashCode</returns>
        public override int GetHashCode()
        {
            return (ColorItems != null ? ColorItems.GetHashCode() : 0);
        }

        /// <summary>
        /// Переопределение оператора == для списков цветов
        /// </summary>
        /// <param name="left">Первый список цветов</param>
        /// <param name="right">Второй список цветов</param>
        /// <returns>True, если списки равны</returns>
        public static bool operator ==(ColorItemList left, ColorItemList right)
        {
            return Equals(left, right);
        }

        /// <summary>
        /// Переопределение оператора != для списка цветов
        /// </summary>
        /// <param name="left">Первый список цветов</param>
        /// <param name="right">Второй список цветов</param>
        /// <returns>True, если списки не равны</returns>
        public static bool operator !=(ColorItemList left, ColorItemList right)
        {
            return !Equals(left, right);
        }
    }
}
