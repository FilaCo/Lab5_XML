﻿using System;
using System.Text.RegularExpressions;

namespace ParsingStructs
{
    /// <summary>
    /// Класс, представляющий собой идентификатор типа "метод"
    /// </summary>
    public class TMethod : Id
    {
        /// <summary>
        /// Регулярное выражение для проверки, описывает ли строка какой-то метод
        /// </summary>
        private static string PATTERN_METHOD = @"^\w+\s+(?!(ref|out|int|char|bool|string|float)\s*\()[^\d\s]\w*\s*\(.*\)\s*;$";
        private static Regex reg = new Regex(PATTERN_METHOD);
        private TListParams listParams;
        /// <summary>
        /// Инициализирует объект класса <see cref="TMethod"/> на основе информации из переданной строки
        /// </summary>
        /// <param name="source">Строка с информацией о новом объекте класса <see cref="TMethod"/></param>
        public TMethod(string source)
        {
            typeId = TypeIdent.METHODS;            
            Parse(source);
        }
        /// <summary>
        /// Функция, которая делит строку, содержащую информацию о методе на 2 части: общая информация о методе 
        /// и информация об аргументе
        /// </summary>
        /// <param name="source"></param>
        /// <param name="mainPart"></param>
        /// <param name="argumentsPart"></param>
        private void SeparateString(string source, out string mainPart, out string argumentsPart)
        {
            mainPart = argumentsPart = "";
            for (int i = 0; i < source.Length; ++i)
            {
                if(source[i] == '(')
                {
                    argumentsPart = source.Substring(i);
                    break;
                }
                mainPart += source[i];
            }
        }
        /// <summary>
        /// Возвращает строку с информацией об идентификаторе и о списке его параметров
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return base.ToString() + listParams;
        }
        protected override void Parse(string source)
        {
            if (!reg.IsMatch(source))
                throw new Exception("Input string has wrong format.");
            source = source.TrimEnd(';');
            SeparateString(source, out string mainPart, out string argsPart);
            string[] inp = mainPart.Split(' ');
            // Определение типа значения метода
            switch (inp[0])
            {
                case "int":
                    typeVal = TypeValue.int_type;
                    break;
                case "float":
                    typeVal = TypeValue.float_type;
                    break;
                case "bool":
                    typeVal = TypeValue.bool_type;
                    break;
                case "char":
                    typeVal = TypeValue.char_type;
                    break;
                default:
                    throw new Exception("Undefined value type.");
            }
            Name = inp[1];
            listParams = new TListParams(argsPart);
        }
        /// <summary>
        /// Проверяет, является ли поданная на вход строка корректной для данного типа идентификатора
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>  
        public static bool IsCorrectSourceString(string source)
        {
            return reg.IsMatch(source);
        }
    }
}
