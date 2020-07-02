using Microsoft.Office.Interop.Word;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Animation;
using Numerics;

namespace Генератор_вариантов
{
    //Класс, хранящий текст заданий варианта и ответы к заданиям
    class TestVersion
    {
        private decimal _versionNum; //Номер варианта
        private string[] _tasks; //Тексты заданий
        private List<double>[] _solutions;
        private string[] _stringSolutions;
        private string _versionText;
        private string _answersText;


        public string VersionText
        {
            get 
            {
                return _versionText;
            }
        }

        public string AnswersText
        {
            get { return _answersText; }
        }

        public decimal VersionNum
        {
            get { return _versionNum; }
        }

        public TestVersion(decimal numOfVersion)
        {
            _versionNum = numOfVersion;
            _tasks = new string[18];
            _solutions = new List<double>[18];
            _stringSolutions = new string[18];
        }

        //Метод, генерирующий тексты заданий и ответы к ним
        public void generateTasks()
        {
            //Генерируем каждое задание в отдельном методе
            generateFirstTask();
            generateSecondTask();
            generateFirdTask();
            generateFourthTask();
            generateFifthTask();
            generateSixthTask();
            generateSeventhTask();
            generateEighthTask();
            generateNinthTask();
            generateTenthTask();
            generateEleventhTask();
            generateTwelfthTask();
            generateThirteenthTask();
            generateFourteenthTask();
            generateFifteenthTask();
            generateSixsteenthTask();
            generateSeventeenthTask();
            generateEighteenthTask();

            //Собираем весь текст в одну переменную
            _versionText = string.Empty;
            for (int i = 0; i < 18; ++i)
                _versionText += _tasks[i];

            //Собираем ответы в одну переменную
            _answersText = string.Empty;
            for (int i = 0; i < 18; ++i)
            {
                if (_stringSolutions[i] == null)
                    for (int j = 0; j < _solutions.ElementAt(i).Count; ++j)
                    {
                        if (_solutions.ElementAt(i).Count == 1)
                            _answersText += (i + 1) + ". " + Math.Round(_solutions.ElementAt(i).ElementAt(j), 4) + "\n\n";
                        else //Делим ответ на подпункты, если это необходимо
                        {
                            _answersText += (i + 1) + "." + (char)(97 + j) + ". " + Math.Round(_solutions.ElementAt(i).ElementAt(j), 4)
                                + "\n\n";
                        }
                    }
                else
                    _answersText += (i + 1) + ". " + _stringSolutions.ElementAt(i) + "\n\n";
            }
        }


//----------------------------------Генерация заданий----------------------------------------------
        private void generateFirstTask()
        {
            int[] int_params = new int[4];
            Random rand_generator = new Random();
            //Первое задание
            _tasks[0] = _versionNum + " ВАРИАНТ";
            _tasks[0] += "\n\n" + _versionNum + ".1. На завод привезли партию из ";
            int_params[0] = rand_generator.Next(40, 81);
            int_params[0] -= int_params[0] % 10;
            _tasks[0] += int_params[0] + " подшипников, в которою попали ";
            int_params[1] = rand_generator.Next(5, 5 + int_params[0] / 3);
            _tasks[0] += int_params[1] + " бракованных. Определить вероятность того, что из ";
            int_params[2] = rand_generator.Next(4, 4 + int_params[1] / 3);
            _tasks[0] += int_params[2] + " взятых наугад подшипников окажется: а) по крайней мере один годный, б) ";
            int_params[3] += rand_generator.Next(1, int_params[2] - 2);
            _tasks[0] += int_params[3] + " годны";   
                if (int_params[3] % 10 == 1) _tasks[0] += "й ";
            else _tasks[0] += "х ";
            _tasks[0] += "и " + (int_params[2] - int_params[3]) + " бракованных.";

            _solutions[0] = firstSolution(int_params[0], int_params[1], int_params[2], int_params[3]);
        }

        private void generateSecondTask()
        {
            int[] int_params = new int[4];
            Random rand_generator = new Random();
            //Второе задание
            _tasks[1] = "\n\n" + _versionNum + ".2. В урне ";
            int_params[0] = rand_generator.Next(4, 24);
            _tasks[1] += int_params[0] + " белых и ";
            int_params[1] = rand_generator.Next(4, 24);
            _tasks[1] += int_params[1] + " черных шаров. Вынимают сразу ";
            int_params[2] = rand_generator.Next(3, 8);
            _tasks[1] += int_params[2]; 
            if (int_params[2] < 5) _tasks[1] += " шара. ";
            else _tasks[1] += " шаров. ";
            _tasks[1] += "Найти вероятность того, что среди них окажется ровно ";
            int_params[3] = rand_generator.Next(1, 4);
            _tasks[1] += int_params[3];

            if (int_params[3] % 10 == 1) _tasks[1] += " белый шар.";
            else _tasks[1] += " белых шара";

            _solutions[1] = secondSolution(int_params[0], int_params[1], int_params[2], int_params[3]);
        }

        private void generateFirdTask()
        {
            int[] int_params = new int[2];
            Random rand_generator = new Random();
            //Третье задание
            _tasks[2] = "\n\n" + _versionNum + ".3. В колоде ";
            int_params[0] = (rand_generator.Next(0, 2) == 0) ? 36 : 52;
            _tasks[2] += int_params[0] + " карт. Наугад вынимают ";
            int_params[1] = rand_generator.Next(1, 11);
            _tasks[2] += int_params[1];

            if (int_params[1] < 5) _tasks[2] += " карты.";
            else _tasks[2] += " карт.";

            _tasks[2] += " Найти вероятность того, что среди них окажется хотя бы один туз.";

            _solutions[2] = thirdSolution(int_params[0], int_params[1]);
        }

        private void generateFourthTask()
        {
            double double_param;
            Random rand_generator = new Random();
            //Четвертое задание
            _tasks[3] = "\n\n" + _versionNum + ".4. Вероятности появления каждого из двух независимых событий А и В равны ";
            double_param = Math.Round(rand_generator.NextDouble(), 2);
            _tasks[3] += double_param + " и " + (1 - double_param) + " соответственно. Найти вероятность появления только " +
                "одного из них. ";
            _solutions[3] = fourthSolution(double_param);
        }

        private void generateFifthTask()
        {
            int int_param;
            double[] double_params = new double[7];
            Random rand_generator = new Random();
            //Пятое задание
            _tasks[4] = "\n\n" + _versionNum + ".5.  Узел содержит ";
            int_param = rand_generator.Next(2, 7);
            _tasks[4] += int_param + "  независимо ";
            if (int_param < 5) _tasks[4] += "работающие детали. ";
            else _tasks[4] += "работающих деталей. ";
            _tasks[4] += "Вероятности отказа деталей соответственно равны p1 = ";
            double_params[0] = rand_generator.Next(1, 11) * 0.01;
            _tasks[4] += double_params[0] + ", p";
            for (int i = 1; i < int_param; ++i)
            {
                double_params[i] = rand_generator.Next(1, 11) * 0.01;
                _tasks[4] += (i + 1) + " = " + double_params[i];
                if (i < int_param - 1) _tasks[4] += ", p";
            }
            _tasks[4] += ". Найти вероятность отказа узла, если для этого достаточно, чтобы отказала хотя бы одна деталь.";

            _solutions[4] = fifthSolution(int_param, double_params);
        }

        private void generateSixthTask()
        {
            double[] double_params = new double[3];
            Random rand_generator = new Random();
            //Шестое задание
            _tasks[5] = "\n\n" + _versionNum + ".6.  Радист трижды вызывает корреспондента. Вероятность того, что будет принят первый вызов, равна ";
            double_params[0] = rand_generator.Next(1, 6) * 0.1;
            _tasks[5] += double_params[0] + ", второй - ";
            double_params[1] = rand_generator.Next(1, 6) * 0.1;
            _tasks[5] += double_params[1] + ", третий - ";
            double_params[2] = rand_generator.Next(1, 6) * 0.1;
            _tasks[5] += double_params[2] + ". События, состоящие в том, что данный вызов будет услышан, независимы. Найти вероятность того, "
                + "что корреспондент услышит вызов.";

            _solutions[5] = sixthSolution(double_params[0], double_params[1], double_params[2]);
        }

        private void generateSeventhTask()
        {
            double[] double_params = new double[2];
            Random rand_generator = new Random();

            //Седьмое задание
            _tasks[6] = "\n\n" + _versionNum + ".7.  Два автомата производят детали, поступающие в сборочный цех. " +
                "Вероятность получения брака на первом автомате ";
            double_params[0] = rand_generator.Next(1, 11) * 0.01;
            _tasks[6] += double_params[0] + ", на втором - ";
            double_params[1] = rand_generator.Next(1, 11) * 0.01;
            _tasks[6] += double_params[1] + " Производительность второго автомата вдвое больше производительности первого. Найти вероятность того, "
                + "что наудачу взятая деталь будет бракованная.";

            _solutions[6] = seventhSolution(double_params[0], double_params[1]);
        }

        private void generateEighthTask()
        {
            double[] double_params = new double[2];
            Random rand_generator = new Random();
            //Восьмое задание
            _tasks[7] = "\n\n" + _versionNum + ".8.  Для сигнализации о пожаре установлены два независимо работающих сигнализатора. Вероятность того, "
                + "что при пожаре сигнализатор сработает, равна ";
            double_params[0] = rand_generator.Next(84, 99) * 0.01;
            _tasks[7] += double_params[0] + " для первого сигнализатора и ";
            double_params[1] = rand_generator.Next(75, 99) * 0.01;
            _tasks[7] += double_params[1] + " для второго. Найти вероятность того, что при пожаре сработает только один сигнализатор.";

            _solutions[7] = eighthSolution(double_params[0], double_params[1]);
        }

        private void generateNinthTask()
        {
            int[] int_params = new int[3];
            double[] double_params = new double[3];
            Random rand_generator = new Random();
            //Девятое задание
            _tasks[8] = "\n\n" + _versionNum + ".9. В больницу поступает в среднем ";
            int_params[0] = rand_generator.Next(1, 6) * 10;
            _tasks[8] += int_params[0] + "% больных с заболеванием А, ";
            int_params[1] = rand_generator.Next(1, 5) * 10;
            _tasks[8] += int_params[1] + "% с заболеванием В, ";
            int_params[2] = 100 - int_params[0] - int_params[1];
            _tasks[8] += int_params[2] + "% с заболеванием С.  Вероятность полного выздоровления для каждого заболевания соответственно " +
                "равны ";
            double_params[0] = rand_generator.Next(5, 9) * 0.1;
            double_params[1] = rand_generator.Next(5, 9) * 0.1;
            double_params[2] = rand_generator.Next(5, 9) * 0.1;
            _tasks[8] += double_params[0] + "; " + double_params[1] + "; " + +double_params[2] + ". Больной был выписан из больницы " +
                "здоровым. Найти вероятность того, что он страдал заболеванием А. ";

            _solutions[8] = ninthSolution(int_params[0], int_params[1], int_params[2], double_params[0], double_params[1],
                double_params[2]);
        }

        private void generateTenthTask()
        {
            int[] int_params = new int[2];
            double double_param;
            Random rand_generator = new Random();
            //Десятое задание
            _tasks[9] = "\n\n" + _versionNum + ".10. В семье ";
            int_params[0] = rand_generator.Next(4, 10);
            _tasks[9] += int_params[0] + " детей. Найти вероятность того, что среди них ";
            int_params[1] = rand_generator.Next(1, int_params[0]);
            _tasks[9] += int_params[1]; 
            if (int_params[1] == 1) _tasks[9] += " девочка. ";
            else if (int_params[1] > 1 && int_params[1] < 5) _tasks[9] += " девочки. ";
            else _tasks[9] += " девочек. ";
            _tasks[9] += "Вероятность рождения девочки равна ";
            double_param = rand_generator.Next(20, 60) * 0.01;
            _tasks[9] += double_param + ".";

            _solutions[9] = tenthSolution(int_params[0], int_params[1], double_param);
        }

        private void generateEleventhTask()
        {
            double[] double_params = new double[5];
            Random rand_generator = new Random();
            //Одиннадцатое задание
            _tasks[10] = "\n\n" + _versionNum + ".11. Случайная величина ξ имеет распределения вероятностей, представленное таблицей:"
                + "\nξ     | 0,1 | 0,2  | 0,3  | 0,4  | 0,5 |" + "\nР(х) | ";
            double_params[4] = 1;
            for (int i = 0; i < 4; ++i)
            {
                double_params[i] = rand_generator.Next(1, 26);
                double_params[i] -= double_params[i] % 5;
                double_params[i] *= 0.01;
                _tasks[10] += double_params[i] + " | ";
                double_params[4] -= double_params[i];
            }
            _tasks[10] += double_params[4] + " | " + "\nПостроить многоугольник распределения и найти функцию распределения F(x). ";

            _stringSolutions[10] = eleventhSolution(double_params[0], double_params[1], double_params[2], double_params[3],
                double_params[4]);
            _solutions[11] = twelfthSolution(double_params[0], double_params[1], double_params[2], double_params[3], double_params[4]);
        }

        private void generateTwelfthTask()
        {
            //Двенадцатое задание
            _tasks[11] = "\n\n" + _versionNum + ".12. Найти М(ξ), D(ξ), σ(ξ) случайной величины ξ примера 11.";
        }

        private void generateThirteenthTask()
        {
            int[] int_params = new int[3];
            Random rand_generator = new Random();
            //Тринадцатое задание
            _tasks[12] = "\n\n" + _versionNum + ".13. Задана плотность распределения непрерывной случайной величины:"
                    + "\n φ(х) = Ax^";
            int_params[0] = rand_generator.Next(2, 7);
            _tasks[12] += int_params[0] + ", ∀x ∈ (0;1]\n φ(х) = 0, ∀x ∉ (0;1]. \nНайти А и функцию распределения F(x).";
            _stringSolutions[12] = thirteenthSolution(int_params[0]);
        }

        private void generateFourteenthTask()
        {
            //Четырнадцатое задание
            _tasks[13] = "\n\n" + _versionNum + ".14.  ξ - непрерывная случайная величина примера 13. Найти М(ξ), D(ξ), σ(ξ) ";
        }

        private void generateFifteenthTask()
        {
            int[] int_params = new int[2];
            double double_param;
            Random rand_generator = new Random();

            //Пятнадцатое задание
            _tasks[14] = "\n\n" + _versionNum + ".15.  Вероятность наступления события А в каждом опыте равна ";
            double_param = rand_generator.Next(1, 91) * 0.01;
            _tasks[14] += double_param + ". Найти вероятность того, что событие А в ";
            int_params[0] = rand_generator.Next(200, 3200);
            int_params[0] -= int_params[0] % 100;
            int_params[1] = rand_generator.Next(100, 100 + (int)(0.4 * int_params[0]));
            _tasks[14] += int_params[0] + " опытах произойдет " + int_params[1];
            if (int_params[1] % 10 > 1 && int_params[1] % 10 < 5) _tasks[14] += " раза.";
            else _tasks[14] += " раз.";

            _solutions[14] = fifteenthSolution(double_param, int_params[0], int_params[1]);
        }

        private void generateSixsteenthTask()
        {
            double[] double_params = new double[4];
            Random rand_generator = new Random();
            //Шестнадцатое задание
            _tasks[15] = "\n\n" + _versionNum + ".16. ξ - нормально распределенная случайная величина с параметрами а = ";
            double_params[0] = rand_generator.Next(5, 51) * 0.1;
            _tasks[15] += double_params[0] + "; σ = ";
            double_params[1] = rand_generator.Next(2, 6) * 0.1;
            _tasks[15] += double_params[1] + ". Найти Р(|ξ-";
            double_params[2] = rand_generator.Next(3, 7) * 0.5;
            _tasks[15] += double_params[2] + "| < ";
            double_params[3] = rand_generator.Next(1, 6) * 0.1;
            _tasks[15] += double_params[3] + ").";

            _solutions[15] = sixteenthSolution(double_params[0], double_params[1], double_params[2], double_params[3]);
        }

        private void generateSeventeenthTask()
        {
            int[] int_params = new int[2];
            double double_param;
            Random rand_generator = new Random();
            //Семнадцатое задание
            _tasks[16] = "\n\n" + _versionNum + ".17. Вероятность появления события в каждом из ";
            int_params[0] = rand_generator.Next(4, 41) * 25;
            _tasks[16] += int_params[0] + " независимых испытаний постоянна и равна Р = ";
            double_param = rand_generator.Next(7, 9) * 0.1;
            _tasks[16] += double_param + ". Найти вероятность того, что событие появится не более ";
            int_params[1] = rand_generator.Next(int_params[0] / 2, 3 * int_params[0] / 4);
            if (int_params[1] % 10 == 1)
                _tasks[16] += int_params[1] + " раза.";
            else _tasks[16] += int_params[1] + " раз.";

            _solutions[16] = sevententhSolution(int_params[0], int_params[1], double_param);
        }

        private void generateEighteenthTask()
        {
            double[] double_params = new double[6];
            Random rand_generator = new Random();
            //Восемнадцатое задание
            _tasks[17] = "\n\n" + _versionNum + ".18. Дана таблица распределения вероятностей двумерной случайной величины (ξ,η)"
                + "\nξ \\ η |  -1 |  0  | 1\n0      | ";
            int zero_generated = 0; //Флаг, указывающий, был ли сгененрирован ноль (ноль нужно сгененрировать не более одного раза)
            double_params[0] = rand_generator.Next(0, 4) * 0.1;
            if (double_params[0] == 0) zero_generated = 1;

            double_params[1] = rand_generator.Next(zero_generated, 4) * 0.1;
            if (double_params[1] == 0) zero_generated = 1;
            int max_value = ((int)(10 * (1 - double_params[0] - double_params[1])) < 3) ? (int)(10 * (1 - double_params[0] -
                double_params[1])) + 1 : 4;

            double_params[2] = rand_generator.Next(zero_generated, max_value) * 0.1;
            if (double_params[2] == 0) zero_generated = 1;
            max_value = ((int)(10 * (1 - double_params[0] - double_params[1] - double_params[2])) < 3) ?
                (int)(10 * (1 - double_params[0] - double_params[1] - double_params[2])) + 1 : 4;

            double_params[3] = rand_generator.Next(zero_generated, max_value) * 0.1;
            if (double_params[3] == 0) zero_generated = 1;

            max_value = ((int)(10 * (1 - double_params[0] - double_params[1] - double_params[2] - double_params[3])) < 3) ?
               (int)(10 * (1 - double_params[0] - double_params[1] - double_params[2] - double_params[3])) + 1 : 4;
            double_params[4] = rand_generator.Next(zero_generated, max_value) * 0.1;
            if (double_params[4] == 0) zero_generated = 1;

            double_params[5] = 1;
            for (int i = 0; i < 5; ++i)
                double_params[5] -= double_params[i];

            _tasks[17] += double_params[0] + "| " + double_params[1] + " | " + double_params[2] + "\n1      |  "
                + double_params[3] + "| " + double_params[4] + " | " + double_params[5] + "\nНайти М(ξ), М(η), М(ξη), D(ξ), D(η), " +
                "D(ξη).";

            _solutions[17] = eighteenthSolution(double_params);
        }


//----------------------------------Решения заданий----------------------------------------------
        private List<double> firstSolution(int bearingNum, int defBearings, int takenBearings, int fitTakenBearings)
        {
            double firstAnswer = 1 - C(defBearings, takenBearings) / C(bearingNum, takenBearings);
            double secondAnswer = (C(bearingNum - defBearings, fitTakenBearings) * 
                C(defBearings, takenBearings - fitTakenBearings)) / C(bearingNum, takenBearings);

            List<double> resultList = new List<double>();
            resultList.Add(firstAnswer);
            resultList.Add(secondAnswer);

            return resultList;
        }

        private List<double> secondSolution(int whiteBalls, int blackBalls, int takenBalls, int requiredWhiteBalls)
        {
            double result = C(whiteBalls, requiredWhiteBalls) * C(blackBalls, takenBalls - requiredWhiteBalls) /
                C(whiteBalls + blackBalls, takenBalls);
            List<double> resultList = new List<double>();
            resultList.Add(result);
            return resultList;
        }

        private List<double> thirdSolution(int numOfCards, int takenCards)
        {
            double result = 1 - C(numOfCards - 4, takenCards) / C(numOfCards, takenCards);
            List<double> resultList = new List<double>();
            resultList.Add(result);
            return resultList;
        }

        private List<double> fourthSolution(double aProbability)
        {
            double result = Math.Pow(aProbability, 2) + Math.Pow(1 - aProbability, 2);
            List<double> resultList = new List<double>();
            resultList.Add(result);
            return resultList;
        }

        private List<double> fifthSolution(int details, double[] failProbabilities)
        {
            double result;
            double subtrahend = 1;
            for (int i = 0; i < details; ++i)
            {
                subtrahend *= (1 - failProbabilities[i]);
            }
            result = 1 - subtrahend;
            List<double> resultList = new List<double>();
            resultList.Add(result);
            return resultList;
        }

        private List<double> sixthSolution(double firstCallProb, double secondCallProb, double thirdCallProb)
        {
            double result = 1 - (1 - firstCallProb) * (1 - secondCallProb) * (1 - thirdCallProb);
            List<double> resultList = new List<double>();
            resultList.Add(result);
            return resultList;
        }

        private List<double> seventhSolution(double firstMachineProb, double secondMachineProb)
        {
            double result = (1.0 / 3.0) * firstMachineProb + (2.0 / 3.0) * secondMachineProb;
            List<double> resultList = new List<double>();
            resultList.Add(result);
            return resultList;
        }

        private List<double> eighthSolution(double firstSignDeviceProb, double secondSignDeviceProb)
        {
            double result = firstSignDeviceProb * (1 - secondSignDeviceProb) + secondSignDeviceProb * (1 - firstSignDeviceProb);
            List<double> resultList = new List<double>();
            resultList.Add(result);
            return resultList;
        }

        private List<double> ninthSolution(int firstDiseasePercent, int secondDiseasePercent, int thirdDiseasePercent,
            double firstDiseaseProb, double secondDiseaseProb, double thirdDiseaseProb)
        {
            double result = (firstDiseasePercent / 100.0 * firstDiseaseProb) / (firstDiseaseProb * firstDiseasePercent +
                secondDiseaseProb * secondDiseasePercent + thirdDiseaseProb * thirdDiseasePercent);
            List<double> resultList = new List<double>();
            resultList.Add(result);
            return resultList;
        }

        private List<double> tenthSolution(int childrenNum, int girlsNum, double girlsBirthProb)
        {
            double result = C(childrenNum, girlsNum) * Math.Pow(girlsBirthProb, girlsNum) *
                Math.Pow(1 - girlsBirthProb, childrenNum - girlsNum);
            List<double> resultList = new List<double>();
            resultList.Add(result);
            return resultList;
        }

        private string eleventhSolution(double prob1, double prob2, double prob3, double prob4, double prob5)
        {
            string result = "F(x < 0,1) = 0" +
                "\nF(x < 0,2) = " + prob1.ToString() +
                "\nF(x < 0,3) = " + (prob1 + prob2).ToString() +
                "\nF(x < 0,4) = " + (prob1 + prob2 + prob3).ToString() +
                "\nF(x < 0,5) = " + (prob1 + prob2 + prob3 + prob4).ToString() +
                "\nF(x ≥ 0,5) = " + (prob1 + prob2 + prob3 + prob4 + prob5).ToString();

            List<double> xList = new List<double>();
            for (double i = 0.1; i <= 0.5; i += 0.1)
                xList.Add(i);
            List<double> yList = new List<double>();
            yList.Add(prob1);
            yList.Add(prob2);
            yList.Add(prob3);
            yList.Add(prob4);
            yList.Add(prob5);

            return result;
        }

        private List<double> twelfthSolution(double prob1, double prob2, double prob3, double prob4, double prob5)
        {
            double result = 0.1 * prob1 + 0.2 * prob2 + 0.3 * prob3 + 0.4 * prob4 + 0.5 * prob5;
            List<double> resultList = new List<double>();
            resultList.Add(result);
            result = 0.01 * prob1 + 0.04 * prob2 + 0.09 * prob3 + 0.16 * prob4 + 0.25 * prob5 - Math.Pow(result, 2);
            resultList.Add(result);
            result = Math.Sqrt(result);
            resultList.Add(result);

            return resultList;
        }

        private string thirteenthSolution(int power)
        {
            //Коэффициент А
            double coef = power + 1;

            string result = "A = " + coef.ToString() + "\nF(x) = 0, при х ≤ 0" +
                "\nF(x) = x ^ " + (power + 1).ToString() + ", при 0 < x ≤ 1" +
            "\nF(x) = 1, при х > 1";

            _solutions[13] = fourteenthSolution(power, coef, 0, 1);

            return result;
        }

        private List<double> fourteenthSolution(int power, double coef, int lowLimit, int highLimit)
        {
            //Мат.ожидание
            double result = (coef / (power + 1)) * (Math.Pow(highLimit, power + 1) - Math.Pow(lowLimit, power + 1));
            List<double> resultList = new List<double>();
            resultList.Add(result);
            //Дисперсия
            result = (coef / (power + 2)) * (Math.Pow(highLimit, power + 2) - Math.Pow(lowLimit, power + 2)) - Math.Pow(result, 2);
            resultList.Add(result);
            //Кадратичное отклонение
            result = Math.Sqrt(result);
            resultList.Add(result);

            return resultList;
        }

        private List<double> fifteenthSolution(double prob, int totalExp, int eventExp)
        {
            double result = 1 / Math.Sqrt(prob * (1 - prob) * totalExp) *
                phi((eventExp - prob * totalExp) / Math.Sqrt(prob * (1 - prob) * totalExp));
            List<double> resultList = new List<double>();
            resultList.Add(result);

            return resultList;
        }

        private List<double> sixteenthSolution(double a, double sigma, double deviation, double range)
        {
            double result = Phi((range + deviation - a) / Math.Sqrt(sigma)) - Phi((deviation - range - a) / Math.Sqrt(sigma));
            List<double> resultList = new List<double>();
            resultList.Add(result);

            return resultList;
        }

        private List<double> sevententhSolution(int totalExp, int requiredExp, double prob)
        {
            double result = Phi((requiredExp - totalExp * prob) / Math.Sqrt(totalExp * prob * (1 - prob))) -
                Phi((1 - totalExp * prob) / Math.Sqrt(totalExp * prob * (1 - prob)));

            List<double> resultList = new List<double>();
            resultList.Add(result);

            return resultList;
        }

        private List<double> eighteenthSolution(double[] tableValues)
        {
            //М(ξ)
            double expValE = 0;
            for (int i = 3; i < tableValues.Length; ++i)
                expValE += tableValues[i];
            List<double> resultList = new List<double>();
            resultList.Add(expValE);

            //M(η)
            double mathExpN = -1 * (tableValues[0] + tableValues[3]) + tableValues[2] + tableValues[5];
            resultList.Add(mathExpN);

            //M(ξη)
            double mathExpEN = -1 * tableValues[3] + tableValues[5];
            resultList.Add(mathExpEN);

            //D(ξ)
            double result = 0;
            for (int i = 3; i < tableValues.Length; ++i)
                result += tableValues[i];
            result -= Math.Pow(expValE, 2);
            resultList.Add(result);

            //D(η)
            result = tableValues[0] + tableValues[3] + tableValues[2] + tableValues[5] - Math.Pow(mathExpN, 2);
            resultList.Add(result);

            //D(ξη)
            result = tableValues[3] + tableValues[5] - Math.Pow(mathExpEN, 2);
            resultList.Add(result);

            return resultList;
        }

//-------------------------------Вспомогательные методы--------------------------------------------
        //Количество сочетаний
        private double C(int n, int m)
        {
            int dividend = 1, divider = 1;
            if (n - m > m)
            {
                for (int i = n - m + 1; i <= n; ++i)
                    dividend *= i;
                for (int i = 2; i <= m; ++i)
                    divider *= i;
            }
            else
            {
                for (int i = m + 1; i <= n; ++i)
                    dividend *= i;
                for (int i = 2; i <= n-m; ++i)
                    divider *= i;
            }

            return dividend/divider;
        }

        //Настройка графика
        private void setChart(ref Chart chart, List<double> xList, List<double> yList)
        { 
            
        }

        private double phi(double arg)
        {
            return Math.Exp(-Math.Pow(arg, 2) / 2) / Math.Sqrt(2 * Math.PI);
        }

        //Функция Лапласа
        private double Phi(double arg)
        {
            return 1 / Math.Sqrt(2 * Math.PI) * integral(func, 0, arg);
        }

        //Подынтегральная функция из функции Лапласа
        private double func(double x)
        {
            return Math.Exp(-Math.Pow(x, 2) / 2);
        }

        //Определенный интеграл, значение которого вычисляется методом Симпсона
        private double integral(Func<double, double> integrand, double lowLimit, double highLimit) 
        {
            double n = 100;//Количество отрезков, на которые разбивается [a,b]
            double h; //Шаг
            List<double> x = new List<double>();
            double previous_approx, current_approx;
            const double EPS = 1e-6;

            h = (highLimit - lowLimit) / n;
            x.Add(lowLimit);
            for (int i = 1; i < n; i++)
            {
                x.Add(lowLimit + i * h);
            }
            x.Add(highLimit);
            current_approx = S(integrand, x, h);
            do
            {
                n = n * 2;//Удваиваем количество отрезков разбиения
                h = (highLimit - lowLimit) / n;

                x.Clear();
                x.Add(lowLimit);
                for (int i = 1; i < n; i++)
                {
                    x.Add(lowLimit + i * h);
                }
                x.Add(highLimit);

                previous_approx = current_approx;
                current_approx = S(integrand, x, h);//Применяем формулу Симпсона        
            } while (Math.Abs(previous_approx - current_approx) >= EPS);//Сравниваем с точностью

            return previous_approx;
        }

        //Формула Симпсона
        private double S(Func<double, double> f, List<double> x, double h)
        {
            double evenSum = 0, oddSum = 0;
            for (int i = 2; i < x.Count - 1; i += 2)//Считаем сумму значений подынтегральной функции в узлах с четными индексами
                evenSum += f(x.ElementAt(i));

            for (int i = 1; i < x.Count - 1; i += 2)//С нечетными индексами
                oddSum += f(x.ElementAt(i));

            return (h / 3) * (f(x.ElementAt(0)) + f(x.ElementAt(x.Count - 1)) + 2 * evenSum + 4 * oddSum);
        }
    }
}
