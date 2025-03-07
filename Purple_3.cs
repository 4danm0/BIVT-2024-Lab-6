using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Lab_6.Purple_4;

namespace Lab_6
{
    public class Purple_3
    {
        public struct Participant
        {
            private string _name;
            private string _surname;
            private double[] _marks;
            private int[] _places;

            private int _marksCount;
            public string Name => _name;
            public string Surname => _surname;
            public double[] Marks
            {
                get
                {
                    if (_marks == null) return default(double[]);

                    var newArray = new double[_marks.Length];
                    Array.Copy(_marks, newArray, _marks.Length);
                    return newArray;
                }
            }
            public int[] Places => _places;

            public int Score => _places == null ? 0 : _places.Sum();
            public Participant(string name, string surname)
            {
                _name = name;
                _surname = surname;
                _marks = new double[7];
                _places = new int[7];
                _marksCount = 0;
            }

            public void Evaluate(double result)
            {
                if (_marksCount == 7 || _marks == null) return;
                _marks[_marksCount++]  = result;
            }

            public static void SetPlaces(Participant[] participants)
            {
                if (participants == null) return;
                for (int judgeIndex = 0; judgeIndex < 7; judgeIndex++)
                {
                    SortJudge(participants, judgeIndex);
                    for (int i = 0; i < participants.Length; i++)
                    {
                        participants[i]._places[judgeIndex] = i + 1;
                    }
                }
            }
            private static void SortJudge(Participant[] array, int judgeIndex)
            {
                foreach (var part in array)
                {
                    if (part.Marks == null) return;
                }
                for (int i = 0; i < array.Length; i++)
                {
                    Participant key = array[i];
                    int j = i - 1;

                    while (j >= 0 && array[j].Marks[judgeIndex] < key.Marks[judgeIndex])
                    {
                        array[j + 1] = array[j];
                        j = j - 1;
                    }
                    array[j + 1] = key;
                }
            }

            public static void Sort(Participant[] array)
            {
                if (array == null) return;
                foreach (var part in array)
                {
                    if (part.Places == null) return;
                }
                for (int i = 0; i < array.Length; i++)
                {
                    Participant key = array[i];
                    int j = i - 1;

                    while (j >= 0 && Compare(array[j], key))
                    {
                        array[j + 1] = array[j];
                        j = j - 1;
                    }
                    array[j + 1] = key;
                }
            }
            private static bool Compare(Participant p1, Participant p2)
            {

                if (p1.Score != p2.Score) return p1.Score > p2.Score;
                if (p1.Places.Min() != p2.Places.Min()) return p1.Places.Min() < p2.Places.Min(); //Parity by sum of places
                return p1.Marks.Sum() > p2.Marks.Sum(); //Parity by max judge place
            }
            public void Print()
            {
                Console.WriteLine(_name + " " + _surname);
                Console.Write("Marks: ");
                foreach (double var in _marks)
                {
                    Console.Write(var + "  ");
                }
                Console.Write("Places: ");
                foreach (double var in _places)
                {
                    Console.Write(var + "  ");
                }
                Console.WriteLine($"Score: {Score}");
                Console.WriteLine();
            }
        }
    }
}
