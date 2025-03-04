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
                if (array == null) return;

                Array.Sort(array, (x, y) =>
                {
                    if (x.Marks[judgeIndex] < y.Marks[judgeIndex]) return -1;
                    else if (x.Marks[judgeIndex] > y.Marks[judgeIndex]) return 1;
                    else return 0;
                });
            }

            public static void Sort(Participant[] array)
            {
                if (array == null) return;

                Array.Sort(array, (x, y) =>
                {
                    if (x.Score > y.Score) return -1;
                    if (x.Score < y.Score) return 1;
                    if (x.Places.Min() < y.Places.Min()) return 1;
                    if (x.Places.Min() > y.Places.Min()) return -1;
                    if (x.Marks.Sum() > y.Marks.Sum()) return -1;
                    if (x.Marks.Sum() < y.Marks.Sum()) return 1;
                    else return 0;
                });
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
