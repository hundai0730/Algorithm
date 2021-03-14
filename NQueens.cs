using System;
using System.Collections.Generic;
using NUnit.Framework;

namespace Algorithm
{
    public class NQueens
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void Process()
        {
            var n           = 8;
            var x           = 1;
            var y           = 1;
            var counter     = 0;
            var queens      = new int[n];

            while (y <= n)
            {
                while (x <= n)
                {
                    if (Check(queens, n, x, y))
                    {
                        // 記錄每列 (y 座標) 皇后所在行位置 (x 座標)
                        queens[y-1] = x;

                        // 檢查下一列前重置 x 座標
                        x = 1;
                        break;
                    }
                    else
                    {
                        x++;
                    }
                }

                // 該列無法放置皇后則 Rollback
                if (queens[y-1] == 0)
                {  
                    // Rollback 至第一列，表示無解
                    if (y == 1)
                    {
                        break;
                    }
                    
                    // Rollback 至上一列，x 座標往後移動及重置放置紀錄
                    y--;
                    x = queens[y-1] + 1;
                    queens[y-1] = 0;
                    continue;
                }
                
                if (y == n)
                {
                    // 加入解法
                    Output(queens);
                    counter++;

                    x = queens[y-1] + 1;
                    queens[y-1] = 0;
                    continue;
                }

                y++;
            }

            TestContext.WriteLine($"\nTotal solutions = {counter}\n");
            Assert.Pass();
        }

        private bool Check(int[] queens, int n, int x, int y)
        {
            for (int i = 1; i <= y; i++)
            {
                // 排除同行及對角線
                if (queens[i-1] == x || Math.Abs(y-i) == Math.Abs((queens[i-1] - x)))
                {
                    return false;
                }
            }

            return true;
        }

        private void Output(int[] solution)
        {
            TestContext.WriteLine("\n");
            TestContext.WriteLine("================================");

            var res = string.Empty;
            for (int y = 0; y < solution.Length; y++)
            {
                for (int x = 1; x <= solution.Length; x++)
                {
                    res += solution[y] == x ? "Q " : "* ";
                }
                
                TestContext.WriteLine($"\t{res}");
                
                res = string.Empty;
            }

            TestContext.WriteLine("================================");
        }
    }
}