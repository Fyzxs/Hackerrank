using System;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CompareTheTriplets
{
    [TestClass]
    public class CompareTheTripletsTests
    {
        [TestMethod]
        public void BruteForceAllSame()
        {
            // Arrange
            int a1 = 0;
            int a2 = 0;
            int a3 = 0;
            int b1 = 0;
            int b2 = 0;
            int b3 = 0;
            string expected = "0 0";

            // Act
            string actual = BruteForce(a1, a2, a3, b1, b2, b3);

            // Assert
            actual.Should().Be(expected);
        }
        [TestMethod]
        public void BruteForceAliceWinsAll()
        {
            // Arrange
            int a1 = 1;
            int a2 = 1;
            int a3 = 1;
            int b1 = 0;
            int b2 = 0;
            int b3 = 0;
            string expected = "3 0";

            // Act
            string actual = BruteForce(a1, a2, a3, b1, b2, b3);

            // Assert
            actual.Should().Be(expected);
        }

        [TestMethod]
        public void BruteForceAliceWinsTwoAndTies()
        {
            // Arrange
            int a1 = 1;
            int a2 = 1;
            int a3 = 0;
            int b1 = 0;
            int b2 = 0;
            int b3 = 0;
            string expected = "2 0";

            // Act
            string actual = BruteForce(a1, a2, a3, b1, b2, b3);

            // Assert
            actual.Should().Be(expected);
        }

        [TestMethod]
        public void BruteForceBobWinsAll()
        {
            // Arrange
            int a1 = 1;
            int a2 = 1;
            int a3 = 1;
            int b1 = 0;
            int b2 = 0;
            int b3 = 0;
            string expected = "3 0";

            // Act
            string actual = BruteForce(a1, a2, a3, b1, b2, b3);

            // Assert
            actual.Should().Be(expected);
        }
        private string BruteForce(int a1, int a2, int a3, int b1, int b2, int b3)
        {
            int aSum = 0;
            aSum += BruteForceScore(a1, b1);
            aSum += BruteForceScore(a2, b2);
            aSum += BruteForceScore(a3, b3);

            int bSum = 0;
            bSum += BruteForceScore(b1, a1);
            bSum += BruteForceScore(b2, a2);
            bSum += BruteForceScore(b3, a3);

            return $"{aSum} {bSum}";
        }

        private int BruteForceScore(int lh, int rh)
        {
            return lh > rh ? 1 : 0;
        }

        [TestMethod]
        public void FirstClassAliceWinsAll()
        {
            // Arrange
            FirstClass alice = new FirstClass(1, 0, 1);
            FirstClass bob = new FirstClass(0, 0, 0);


            // Act
            string actualAlice = alice.CompareTo(bob);
            string actualBob = bob.CompareTo(alice);


            // Assert
            string expectedAlice = "2 0";
            string expectedBob = "0 2";

            actualAlice.Should().Be(expectedAlice);
            actualBob.Should().Be(expectedBob);


        }

        [TestMethod]
        public void SecondClassAliceWinsAll()
        {
            // Arrange
            SecondClass alice = new SecondClass(1, 0, 1);
            SecondClass bob = new SecondClass(0, 0, 0);


            // Act
            string actualAlice = alice.CompareTo(bob);
            string actualBob = bob.CompareTo(alice);


            // Assert
            string expectedAlice = "2 0";
            string expectedBob = "0 2";

            actualAlice.Should().Be(expectedAlice);
            actualBob.Should().Be(expectedBob);


        }

        [TestMethod]
        public void SecondClassAliceTiesTwo()
        {
            // Arrange
            SecondClass alice = new SecondClass(0, 0, 1);
            SecondClass bob = new SecondClass(0, 0, 3);


            // Act
            string actualAlice = alice.CompareTo(bob);
            string actualBob = bob.CompareTo(alice);


            // Assert
            string expectedAlice = "0 1";
            string expectedBob = "1 0";

            actualAlice.Should().Be(expectedAlice);
            actualBob.Should().Be(expectedBob);
        }
    }

    public class FirstClass 
    {
        private readonly int _clarity;
        private readonly int _originality;
        private readonly int _difficulty;

        public FirstClass(int clarity, int originality, int difficulty)
        {
            _clarity = clarity;
            _originality = originality;
            _difficulty = difficulty;
        }

        public string CompareTo(FirstClass other)
        {

            (int selfClarity, int tieClarity) = CompareScore(this._clarity, other._clarity);
            (int selfOriginality, int tieOriginality) = CompareScore(this._originality, other._originality);
            (int selfDifficulty, int tieDifficulty) = CompareScore(this._difficulty, other._difficulty);

            int self = selfClarity + selfOriginality + selfDifficulty;
            int tie = tieClarity + tieOriginality + tieDifficulty;
            int that = 3 - (self + tie);
            return $"{self} {that}";
        }

        private (int, int) CompareScore(int selfScore, int otherScore)
        {
            int score = selfScore.CompareTo(otherScore);
            return (score > 0 ? 1 : 0, score == 0 ? 1 : 0);
        }
    }
    public class SecondClass
    {
        private readonly int _clarity;
        private readonly int _originality;
        private readonly int _difficulty;

        public SecondClass(int clarity, int originality, int difficulty)
        {
            _clarity = clarity;
            _originality = originality;
            _difficulty = difficulty;
        }

        public string CompareTo(SecondClass other)
        {
            return $"{this.ScoreAgainst(other)} {other.ScoreAgainst(this)}";
        }

        private int ScoreAgainst(SecondClass other)
        {
            int self = this._clarity > other._clarity ? 1 : 0;
            self += this._originality > other._originality ? 1 : 0;
            self += this._difficulty > other._difficulty ? 1 : 0;

            return self;
        }
    }
}
