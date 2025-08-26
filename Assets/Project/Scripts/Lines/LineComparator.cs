using System.Collections.Generic;
using System.Linq;
using Project.Scripts.TickerSystem.BallSpawningSystem;

namespace Project.Scripts.Lines
{
    public class LineComparator
    {
        private readonly Ball[,] _balls;
        
        private List<(int x, int y)[]> _lines;

        public LineComparator(Ball[,] balls)
        {
            _balls = balls;
        }

        public void GenerateLines(int size)
        {
            _lines = new List<(int, int)[]>();

            for (int y = 0; y < size; y++)
            {
                var line = new (int, int)[size];
                
                for (int x = 0; x < size; x++)
                    line[x] = (x, y);
                
                _lines.Add(line);
            }

            for (int x = 0; x < size; x++)
            {
                var line = new (int, int)[size];
                
                for (int y = 0; y < size; y++)
                    line[y] = (x, y);
                
                _lines.Add(line);
            }

            var mainDiagonal = new (int, int)[size];
            
            for (int i = 0; i < size; i++)
                mainDiagonal[i] = (i, i);
            
            _lines.Add(mainDiagonal);

            var reverseDiagonal = new (int, int)[size];
            
            for (int i = 0; i < size; i++)
                reverseDiagonal[i] = (size - 1 - i, i);
            
            _lines.Add(reverseDiagonal);
        }
        
        public List<Ball> CheckMatches()
        {
            List<Ball> matched = new();

            foreach ((int x, int y)[] line in _lines)
            {
                Ball first = _balls[line[0].x, line[0].y];
                
                if (first == null) 
                    continue;

                bool allSame = true;
                
                for (int i = 1; i < line.Length; i++)
                {
                    var cell = _balls[line[i].x, line[i].y];

                    if (cell != null && cell.ColorID == first.ColorID) 
                        continue;
                    
                    allSame = false;
                    
                    break;
                }

                if (allSame == false) 
                    continue;

                matched.AddRange(line.Select(pos => _balls[pos.x, pos.y]));
            }

            return matched.Distinct().ToList();
        }
    }
}