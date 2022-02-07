using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Mars_Rover.UnitTest
{
    [TestClass]
    public class MarsRoverSolutionTests
    {
        Controls CommonControl;

        [TestMethod]
        public void CoordinateValidControl_ValidCoordinate_ReturnTrue()
        {
            CommonControl = new Controls();
            List<string[]> testValues = new List<string[]>();

            #region Test Values Create
            string[] coordinate = new string[2] { "5", "5" };
            string[] coordinate1 = new string[2] { "3", "8" };
            string[] coordinate2 = new string[2] { "4", "10" };
            string[] coordinate3 = new string[2] { "2", "9" };
            string[] coordinate4 = new string[3] { "1", "3", "N" };
            string[] coordinate5 = new string[3] { "5", "10", "W" };
            string[] coordinate6 = new string[3] { "3", "2", "S" };
            string[] coordinate7 = new string[3] { "8", "1", "E" };
            #endregion Test Values Create

            #region Test Values Add
            testValues.Add(coordinate);
            testValues.Add(coordinate1);
            testValues.Add(coordinate2);
            testValues.Add(coordinate3);
            testValues.Add(coordinate4);
            testValues.Add(coordinate5);
            testValues.Add(coordinate6);
            testValues.Add(coordinate7);
            #endregion Test Values Add

            foreach (var testValue in testValues)
            {
                bool result = CommonControl.CoordinateValidControl(testValue, testValue.Length);

                Assert.IsTrue(result);
            }

        }

        [TestMethod]
        public void CoordinateValidControl_ValidCoordinate_ReturnFalse()
        {
            CommonControl = new Controls();
            List<string[]> testValues = new List<string[]>();

            #region Test Values Create
            string[] coordinate = new string[2] { "5", "" };
            string[] coordinate1 = new string[2] { "", "2" };
            string[] coordinate2 = new string[2] { "", "" };
            string[] coordinate3 = new string[3] { "1", "3", "" };
            string[] coordinate4 = new string[3] { "5", "", "W" };
            string[] coordinate5 = new string[3] { "", "2", "S" };
            string[] coordinate6 = new string[3] { "", "", "E" };
            #endregion Test Values Create

            #region Test Values Add
            testValues.Add(coordinate);
            testValues.Add(coordinate1);
            testValues.Add(coordinate2);
            testValues.Add(coordinate3);
            testValues.Add(coordinate4);
            testValues.Add(coordinate5);
            testValues.Add(coordinate6);
            #endregion Test Values Add

            foreach (var testValue in testValues)
            {
                bool result = CommonControl.CoordinateValidControl(testValue, testValue.Length);

                Assert.IsFalse(result);
            }

        }

        [TestMethod]
        public void MoveFormatControl_ValidCharacter_ReturnTrue()
        {
            CommonControl = new Controls();
            List<string> roverMoves = new List<string>();
            roverMoves.Add("LMLMLMLMM");
            roverMoves.Add("MMRMMRMRRM");
            roverMoves.Add("MLMRMLMRR");
            roverMoves.Add("RMMLMMLMR");
            roverMoves.Add("LLMMRRMM");

            bool result = CommonControl.MoveFormatControl(roverMoves);

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void MoveFormatControl_NotValidCharacter_ReturnFalse()
        {
            CommonControl = new Controls();
            List<string> roverMoves = new List<string>();
            roverMoves.Add("LMLMASDMLMM");
            roverMoves.Add("a");
            roverMoves.Add("llm");
            roverMoves.Add("DDGO");
            roverMoves.Add("BACK");

            bool result = CommonControl.MoveFormatControl(roverMoves);

            Assert.IsFalse(result);
        }

        [TestMethod]
        public void ToDirectionInfoList_ValidCharacter_ReturnCorrectDirectionInfoList()
        {
            CommonControl = new Controls();
            List<string> roverStartingLocations = new List<string>();
            List<string> roverMoves = new List<string>();
            List<DirectionInfo> directionInfos = new List<DirectionInfo>();

            #region roverStartingLocations values
            roverStartingLocations.Add("1 2 N");
            roverStartingLocations.Add("3 3 E");
            #endregion roverStartingLocations values

            #region roverMoves values
            roverMoves.Add("LMLMLMLMM");
            roverMoves.Add("MMRMMRMRRM");
            #endregion roverMoves values

            #region DirectionInfoList Values
            directionInfos.Add(new DirectionInfo()
            {
                XCoordinate = 1,
                YCoordinate = 2,
                DirectMove = "N",
                Moves = "LMLMLMLMM"
            });
            directionInfos.Add(new DirectionInfo()
            {
                XCoordinate = 3,
                YCoordinate = 3,
                DirectMove = "E",
                Moves = "MMRMMRMRRM"
            });
            #endregion DirectionInfoList Values

            List<DirectionInfo> results = CommonControl.ToDirectionInfoList(roverStartingLocations, roverMoves);

            foreach (DirectionInfo result in results)
            {
                Assert.IsTrue(directionInfos.Where(x => (x.Moves == result.Moves) &&
                                                        (x.XCoordinate == result.XCoordinate) &&
                                                        (x.YCoordinate == result.YCoordinate)).Count() > 0);
            }
        }

        [TestMethod]
        public void ToDirectionInfoList_ValidCharacter_ReturnIncorrectList()
        {
            CommonControl = new Controls();
            List<string> roverStartingLocations = new List<string>();
            List<string> roverMoves = new List<string>();
            List<DirectionInfo> directionInfos = new List<DirectionInfo>();

            #region roverStartingLocations values
            roverStartingLocations.Add("1 2 N");
            roverStartingLocations.Add("3 3 E");
            #endregion roverStartingLocations values

            #region roverMoves values
            roverMoves.Add("LMLMLMLMM");
            roverMoves.Add("MMRMMRMRRM");
            #endregion roverMoves values

            #region DirectionInfoList Values
            directionInfos.Add(new DirectionInfo()
            {
                XCoordinate = 1,
                YCoordinate = 3,
                DirectMove = "N",
                Moves = "LMLMLMLMM"
            });
            directionInfos.Add(new DirectionInfo()
            {
                XCoordinate = 3,
                YCoordinate = 3,
                DirectMove = "E",
                Moves = "MMMMM"
            });
            #endregion DirectionInfoList Values

            List<DirectionInfo> results = CommonControl.ToDirectionInfoList(roverStartingLocations, roverMoves);

            foreach (DirectionInfo result in results)
            {
                Assert.IsFalse(directionInfos.Where(x => (x.Moves == result.Moves) &&
                                                        (x.XCoordinate == result.XCoordinate) &&
                                                        (x.YCoordinate == result.YCoordinate)).Count() > 0);
            }
        }

        [TestMethod]
        [ExpectedException(typeof(FormatException))]
        public void ToDirectionInfoList_NotValidInfo_ReturnFormatException()
        {
            CommonControl = new Controls();
            List<string> roverStartingLocations = new List<string>();
            List<string> roverMoves = new List<string>();

            #region roverStartingLocations values
            roverStartingLocations.Add("1 A N");
            roverStartingLocations.Add("3 3 E");
            #endregion roverStartingLocations values

            #region roverMoves values
            roverMoves.Add("LMLMLMLMM");
            roverMoves.Add("MMRMMRMRRM");
            #endregion roverMoves values

            CommonControl.ToDirectionInfoList(roverStartingLocations, roverMoves);
        }

        [TestMethod]
        public void CalculateResult_ValidInfo_ReturnCorrectValue()
        {
            CommonControl = new Controls();
            DirectionInfo directionInfo = new DirectionInfo();
            string[] coordinate = new string[2] { "5", "5" };
            string expectedResult = "1 3 N";

            directionInfo.XCoordinate = 1;
            directionInfo.YCoordinate = 2;
            directionInfo.DirectMove = "N";
            directionInfo.Moves = "LMLMLMLMM";

            string result = CommonControl.CalculateResultForTestMethod(directionInfo, coordinate);

            Assert.AreEqual(expectedResult, result);

        }

        [TestMethod]
        public void CalculateResult_ValidInfo_ReturnIncorrectValue()
        {
            CommonControl = new Controls();
            DirectionInfo directionInfo = new DirectionInfo();
            string[] coordinate = new string[2] { "5", "5" };
            string expectedResult = "5 1 W";

            directionInfo.XCoordinate = 3;
            directionInfo.YCoordinate = 3;
            directionInfo.DirectMove = "E";
            directionInfo.Moves = "MMRMMRMRRM";

            string result = CommonControl.CalculateResultForTestMethod(directionInfo, coordinate);

            Assert.AreNotEqual(expectedResult, result);

        }

    }
}
