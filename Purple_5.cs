using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab_6
{
    public class Purple_5
    {
        public struct Response
        {
            private string _animal;
            private string _characterTrait;
            private string _concept;

            public string Animal => _animal;
            public string CharacterTrait => _characterTrait;
            public string Concept => _concept;

            public Response(string animal, string characterTrait, string concept)
            {
                _animal = animal;
                _characterTrait = characterTrait;
                _concept = concept;
            }

            public int CountVotes(Response[] responses, int questionNumber)
            {
                if (responses == null) return 0;
                int answered = 0;
                for (int i = 0; i < responses.Length; i++)
                {
                    switch (questionNumber)
                    {
                        case 1:
                            if (responses[i].Animal != null && responses[i].Animal != "") answered++;
                            break;
                        case 2:
                            if (responses[i].CharacterTrait != null && responses[i].CharacterTrait != "") answered++;
                            break;
                        case 3:
                            if (responses[i].Concept != null && responses[i].Concept != "") answered++;
                            break;
                        default:
                            return 0;
                    }
                }
                return answered;
            }

            public void Print()
            {
                Console.WriteLine(_animal + " " + _characterTrait + " " + _concept);
            }
        }

        public struct Research
        {
            private string _name;
            private Response[] _responses;

            public string Name => _name;
            public Response[] Responses => _responses;

            public Research(string name)
            {
                _name = name;
                _responses = new Response[0];
            }

            public void Add(string[] answers)
            {
                if (answers == null || _responses == null) return;
                Array.Resize(ref _responses, _responses.Length + 1);
                _responses[_responses.Length - 1] = new Response(answers[0], answers[1], answers[2]);
            }

            public string[] GetTopResponses(int question)
            {
                if (_responses == null) { return null; }
                switch (question)
                {
                    case 1:return _responses.GroupBy(x => x.Animal).Where(x => x.Key != null && x.Key.Length != 0).OrderByDescending(x => x.Count()).Take(5).Select(x => x.Key).ToArray();

                    case 2: return _responses.GroupBy(x => x.CharacterTrait).Where(x => x.Key != null && x.Key.Length != 0).OrderByDescending(x => x.Count()).Take(5).Select(x => x.Key).ToArray();

                    case 3: return _responses.GroupBy(x => x.Concept).Where(x => x.Key != null && x.Key.Length != 0).OrderByDescending(x => x.Count()).Take(5).Select(x => x.Key).ToArray();

                }
                
                return null;    
                
            }

            public void Print()
            {
                Console.WriteLine(_name);
                foreach (var response in _responses)
                {
                    response.Print();
                }
            }
        }
    }
}
