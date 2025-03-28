using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab_6
{
    public class Purple_4
    {
        public struct Sportsman
        {
            private string _name;
            private string _surname;
            private double _time;

            public string Name => _name;
            public string Surname => _surname;
            public double Time => _time;

            public Sportsman(string name, string surname)
            {
                _name = name;
                _surname = surname;
                _time = 0.0;
            }

            public void Run(double time)
            {
                if (_time != 0.0) return;
                _time = time;
            }

            public void Print()
            {
                Console.WriteLine(_name + " " + _surname);
                Console.WriteLine($"Time: {_time}");
                Console.WriteLine();
            }
        }
        public struct Group
        {
            private string _name;
            private Sportsman[] _sportsmen;

            public string Name => _name;
            public Sportsman[] Sportsmen => _sportsmen;
            
            

            public Group(string name)
            {
                _name = name;
                _sportsmen = new Sportsman[0]; 
            }
            public Group(Group group)
            {
                _name = group.Name;
                if (group.Sportsmen == null)
                {
                    _sportsmen = new Sportsman[0];
                    return;
                }
                _sportsmen = new Sportsman[group.Sportsmen.Length];
                Array.Copy(group.Sportsmen, _sportsmen, group.Sportsmen.Length);
            }

            public void Add(Sportsman newSportsman)
            {
                if (_sportsmen == null) return;
                Array.Resize(ref _sportsmen, _sportsmen.Length + 1);
                _sportsmen[_sportsmen.Length - 1] = newSportsman;
            }

            public void Add(Sportsman[] newSportsmen)
            {
                if (newSportsmen == null || _sportsmen == null) return;
                int oldLength = _sportsmen.Length;
                Array.Resize(ref _sportsmen, _sportsmen.Length + newSportsmen.Length);
                Array.Copy(newSportsmen, 0, _sportsmen, oldLength, newSportsmen.Length); 
            }

            public void Add(Group group) 
            {
                if (group.Sportsmen == null || _sportsmen == null) return;
                int oldLength = _sportsmen.Length;
                Array.Resize(ref _sportsmen, _sportsmen.Length + group.Sportsmen.Length); 
                Array.Copy(group.Sportsmen, 0, _sportsmen, oldLength, group.Sportsmen.Length);
            }

            public void Sort()
            {
                if (_sportsmen == null) return;
                for (int i = 0; i < _sportsmen.Length; i++)
                {
                    Sportsman key = _sportsmen[i];
                    int j = i - 1;

                    while (j >= 0 && _sportsmen[j].Time > key.Time) //ascending
                    {
                        _sportsmen[j + 1] = _sportsmen[j];
                        j = j - 1;
                    }
                    _sportsmen[j + 1] = key;
                }
            }

            public static Group Merge(Group group1, Group group2)
            {
                if (group1.Sportsmen == null || group2.Sportsmen == null) return default(Group); 
                Group finalists = new Group("Финалисты");

                group1.Sort();
                group2.Sort();

                int i = 0, j = 0;
                while (i < group1.Sportsmen.Length && j < group2.Sportsmen.Length)
                {
                    if (group1.Sportsmen[i].Time <= group2.Sportsmen[j].Time)
                        finalists.Add(group1.Sportsmen[i++]);

                    else
                        finalists.Add(group2.Sportsmen[j++]);
                }

                while (i < group1.Sportsmen.Length)
                    finalists.Add(group1.Sportsmen[i++]);

                while (j < group2.Sportsmen.Length)
                    finalists.Add(group2.Sportsmen[j++]);

                return finalists;
            }

            public void Print()
            {
                Console.WriteLine($"Group name: {_name}");
                foreach (Sportsman sportsman in _sportsmen)
                {
                    sportsman.Print();
                }
            }
        }
    }
}
