using System;
using System.Drawing;

namespace TestSortApp.Library
{
    /// <summary>
    /// Класс, содержащий признак цвета
    /// </summary>
    public class ColorItem
    {
        /// <summary>
        /// Признак цвета
        /// </summary>
        public KnownColor ValueColor { get; }

        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="charColor">Символ цвета</param>
        public ColorItem(char charColor)
        {
            switch (char.ToUpper(charColor))
            {
                case 'К':
                    ValueColor = KnownColor.Red;
                    break;
                case 'З':
                    ValueColor = KnownColor.Green;
                    break;
                case 'С':
                    ValueColor = KnownColor.Blue;
                    break;
                default:
                    throw new ArgumentException($"Неизвестное значение: {charColor}", nameof(charColor));
            }
        }

        /// <summary>
        /// Преобразование объекта в строку - признак цвета
        /// </summary>
        /// <returns>Строка - признак цвета</returns>
        public override string ToString()
        {
            switch (ValueColor)
            {
                case KnownColor.Red:
                    return "К";
                case KnownColor.Green:
                    return "З";
                case KnownColor.Blue:
                    return "С";
                default:
                    throw new Exception($"Неизвестное значение: {ValueColor}");
            }
        }

        /// <summary>
        /// Сравнение объектов
        /// </summary>
        /// <param name="obj">Объект, с которым происходит сравнение</param>
        /// <returns>True, если объекты равны</returns>
        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj))
                return false;
            if (ReferenceEquals(this, obj))
                return true;
            if (obj.GetType() != this.GetType())
                return false;

            return Equals((ColorItem) obj);
        }

        /// <summary>
        /// Сравнение цветов объектов
        /// </summary>
        /// <param name="other">Объект, с которым происходит сравнение</param>
        /// <returns>True, если цвета объектов равны</returns>
        protected bool Equals(ColorItem other)
        {
            return ValueColor == other.ValueColor;
        }

        /// <summary>
        /// Получение HashCode
        /// </summary>
        /// <returns>HashCode</returns>
        public override int GetHashCode()
        {
            return (int) ValueColor;
        }

        /// <summary>
        /// Переопределение оператора ==
        /// </summary>
        /// <param name="left">Первый объект</param>
        /// <param name="right">Второй объект</param>
        /// <returns>True, если равны</returns>
        public static bool operator ==(ColorItem left, ColorItem right)
        {
            return Equals(left, right);
        }

        /// <summary>
        /// Переопределение оператора !=
        /// </summary>
        /// <param name="left">Первый объект</param>
        /// <param name="right">Второй объект</param>
        /// <returns>True, если не равны</returns>
        public static bool operator !=(ColorItem left, ColorItem right)
        {
            return !Equals(left, right);
        }
    }
}
