# TestSortApp

Выбор основного алгоритма

Основной алгоритм – упорядочивание цветов согласно заданному правилу.<br/>
Так как количество различных значений в последовательности цветов всего 3, то был выбран алгоритм сортировки без перестановок – Блочный.<br/> 
Для хранения последовательности объектов выбран список List<>, для которого существует стандартный метод Add() добавления объекта к списку.<br/> 
Блочный алгоритм пробегает список объектов, помеченных тремя цветами, и разделяет их на 3 блока (списка) по цвету.<br/> 
Затем три списка соединяются друг с другом согласно правилу сортировки.<br/>

Для количества объектов в неупорядоченном списке до 60 млн (на моем компьютере), время работы сортировки SortColorList возрастает линейно.<br/> 
В отличие от стандартной сортировки Sort для списка, которая использует перестановки элементов списка, Блочная сортировка работает тем быстрее, чем больше количество сортируемых элементов.<br/>

Unit test  SortColorList_LongValidStr_Test() для сравнения выводит время работы как сортировки SortColorList(), так и стандартной сортировки Sort().<br/>

Как немного другой вариант можно реализовать сортировку Подсчетом.<br/> 
Неупорядоченный список пробегаем и подсчитываем количество элементов каждого цвета.<br/> 
Затем формируем итоговый упорядоченный список согласно заданному правилу сортировки.<br/> 
Этот алгоритм не выбран, так как здесь не сортировка, а создание нового упорядоченного списка по известному числу объектов каждого цвета.<br/>

Быстрая сортировка предполагает выбор двунаправленного списка для хранения последовательности объектов.<br/> 
И сама сортировка выполняется в 2 прохода. За первый проход создает двунаправленный список центрального цвета (согласно правилу).<br/> 
За второй проход добавляем к созданному списку слева младший цвет и справа старший цвет.<br/>

Дефекты

Программа протестирована по тест-кейсам (файл «Тест-кейсы Сортировка цветов» прилагается). Все тесты пройдены успешно.<br/>
Возможно улучшение – создание строки состояния программы.<br/>

