﻿using System;
using System.Text.RegularExpressions;

namespace ParsingStructs
{
    /// <summary>
    /// Класс, представляющий собой список параметров для объекта-идентификатора <see cref="TMethod"/>
    /// </summary>
    class TListParams
    {
        /// <summary>
        /// Регулярное выражение для проверки, описывает ли строка какой-то список аргументов функции
        /// </summary>
        private const string PATTERN_LISTPARAMS = @"^\(\s*((\s*(\w+\s+){1,2}\w+\s*,)*\s*(\w+\s+){1,2}\w+)?\s*\)$";
        private static Regex reg = new Regex(PATTERN_LISTPARAMS);
        /// <summary>
        /// Следующий элемент в списке параметров
        /// </summary>
        private TListParams next;
        /// <summary>
        /// Объект, описывающий текущий параметр
        /// </summary>
        private TParam data;
        public TListParams()
        {

        }
        /// <summary>
        /// Инициализирует новую коллекцию <see cref="TListParams"/> из одного корневого элемента
        /// </summary>
        /// <param name="elem"></param>
        public TListParams (TParam elem)
        {
            data = elem;
            next = null;
        }
        /// <summary>
        /// Инициализирует новую коллекцию <see cref="TListParams"/> из строки, содержащей информацию о параметрах метода
        /// </summary>
        /// <param name="source">Строка с информацией о новом объекте класса <see cref="TListParams"/></param>
        public TListParams(string source)
        {
            Parse(source);
        }
        /// <summary>
        /// Выделение информации об объекте класса из строки ввода
        /// </summary>
        /// <param name="source"></param>
        private void Parse(string source)
        {
            if (!reg.IsMatch(source))
                throw new Exception("Input string has syntax error: it's not the arguments string");
            source = source.Trim('(', ')');
            string[] inp = source.Split(',');
            data = new TParam(inp[0]);
            TListParams cur = this;
            for (int i = 1; i < inp.Length; ++i)
            {
                TParam elem = new TParam(inp[i]);
                cur.next = new TListParams(elem);
                cur = cur.next;
            }
        }
        /// <summary>
        /// Добавление нового элемента в коллекцию в конец списка
        /// </summary>
        /// <param name="elem"></param>
        public void AddEnd(TParam elem)
        {
            TListParams cur = this;
            while (cur.next != null)
                cur = cur.next;
            cur.next = new TListParams(elem);
        }
        /// <summary>
        /// Возвращает строку с информацией обо всех элементах коллекции
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            string cur = "";
            TListParams pntr = this;
            while (pntr != null)
            {
                cur += pntr.data;
                pntr = pntr.next;
            }
            return cur;
        }
    }
}
