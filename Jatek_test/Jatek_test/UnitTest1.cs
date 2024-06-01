using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Jatek_test
{
    [TestClass]
    public class JátékTeszt
    {
        private int[,] matrix;
        private int elet;
        private string irany;
        private DateTime startTime;
        private TimeSpan bestTime;

        [TestInitialize]
        public void Inialize()
        {
            matrix = new int[,]
            {

            {1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 2, 1, 1, 1},
            {1, 0, 0, 1, 0, 0, 0, 1, 0, 0, 0, 4, 0, 4, 0, 1, 0, 0, 0, 1},
            {1, 1, 0, 1, 1, 1, 0, 1, 1, 1, 1, 1, 0, 1, 0, 1, 1, 1, 0, 1},
            {1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 1, 0, 0, 0, 1, 0, 1},
            {1, 1, 1, 0, 1, 1, 1, 1, 1, 1, 0, 1, 0, 1, 1, 1, 1, 1, 0, 1},
            {1, 0, 0, 0, 1, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1},
            {1, 1, 1, 0, 1, 0, 1, 1, 0, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1},
            {1, 0, 1, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1},
            {1, 0, 1, 4, 1, 0, 1, 1, 1, 1, 1, 1, 0, 1, 1, 1, 1, 1, 4, 1},
            {1, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 1},
            {1, 1, 1, 0, 1, 1, 1, 1, 1, 1, 0, 1, 0, 1, 1, 1, 1, 1, 0, 1},
            {1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 1, 0, 1},
            {1, 0, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 0, 1},
            {1, 0, 0, 0, 0, 0, 4, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 1},
            {1, 1, 1, 1, 1, 1, 0, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 0, 1},
            {1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1},
            {1, 3, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1},
        };

            elet = 3;
            irany = "down";
            startTime = DateTime.Now;
            bestTime = TimeSpan.MaxValue;

            if (File.Exists(""))
            {
                var timeString = File.ReadAllText("");
                TimeSpan.TryParse(timeString, out bestTime);
            }
        }

        private (int row, int col) KocsiMegtalal()
        {
            for (int rowIndex = 0; rowIndex < matrix.GetLength(0); rowIndex++)
            {
                for (int colIndex = 0; colIndex < matrix.GetLength(1); colIndex++)
                {
                    if (matrix[rowIndex, colIndex] == 2)
                    {
                        return (rowIndex, colIndex);
                    }
                }
            }
            return (-1, -1);
        }

        private bool KocsiMozgat(int dx, int dy)
        {
            var (rowIndex, colIndex) = KocsiMegtalal();
            int newRowIndex = rowIndex + dy;
            int newColIndex = colIndex + dx;

            if (newRowIndex >= 0 && newRowIndex < matrix.GetLength(0) &&
                newColIndex >= 0 && newColIndex < matrix.GetLength(1))
            {
                if (matrix[newRowIndex, newColIndex] == 1)
                {
                    elet--;
                    return false; 
                }

                if (matrix[newRowIndex, newColIndex] == 3)
                {
                    matrix[rowIndex, colIndex] = 0;
                    matrix[newRowIndex, newColIndex] = 2;
                    TimeSpan elapsedTime = DateTime.Now - startTime;

                    if (elapsedTime < bestTime)
                    {
                        bestTime = elapsedTime;
                        File.WriteAllText("", bestTime.ToString());
                    }

                    return true; 
                }

                if (matrix[newRowIndex, newColIndex] == 4)
                {
                    matrix[newRowIndex, newColIndex] = 5;
                    elet--;
                    return false; 
                }

                matrix[rowIndex, colIndex] = 0;
                matrix[newRowIndex, newColIndex] = 2;
                return true; 
            }
            return false; 
        }

        private void IranyValtas(string ujIrany)
        {
            irany = ujIrany;
        }

        [TestMethod]
        public void TestInitialLifeCount()
        {
            Assert.AreEqual(3, elet);
        }

        [TestMethod]
        public void TestMoveUp()
        {
            IranyValtas("up");
            Assert.IsFalse(KocsiMozgat(0, -1));
            Assert.AreEqual(2, elet); 
        }

        [TestMethod]
        public void TestMoveRight()
        {
            IranyValtas("right");
            Assert.IsTrue(KocsiMozgat(1, 0)); 
        }

        [TestMethod]
        public void TestMoveToSpike()
        {
            IranyValtas("right");
            KocsiMozgat(1, 0);
            Assert.IsFalse(KocsiMozgat(1, 0)); 
            Assert.AreEqual(2, elet); 
        }

        [TestMethod]
        public void TestGameOver()
        {
            IranyValtas("right");
            KocsiMozgat(1, 0); 
            KocsiMozgat(1, 0); 
            KocsiMozgat(1, 0); 
            KocsiMozgat(1, 0); 
            Assert.AreEqual(0, elet);
        }

        [TestMethod]
        public void TestReachGoal()
        {
            Initialize();

            var (row, col) = KocsiMegtalal();
            int goalRow = 16, goalCol = 1;

            while (row != goalRow || col != goalCol)
            {
                if (col < goalCol) KocsiMozgat(1, 0);
                else if (col > goalCol) KocsiMozgat(-1, 0);
                else if (row < goalRow) KocsiMozgat(0, 1);
                else if (row > goalRow) KocsiMozgat(0, -1);

                (row, col) = KocsiMegtalal();
            }

            Assert.AreEqual(goalRow, row);
            Assert.AreEqual(goalCol, col);
        }
    }
}
        