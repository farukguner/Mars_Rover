using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mars_Rover
{
    public enum Directly { W, N, E, S };

    public abstract class Helper
    {
        public abstract bool CoordinateValidControl(string[] coordinate, int lenght);

        public abstract bool MoveFormatControl(List<string> roverMoves);

        public abstract List<DirectionInfo> ToDirectionInfoList(List<string> roverStartingLocations, List<string> roverMoves);

        public abstract void CalculateResult(DirectionInfo directionInfo, string[] coordinateArray);

        public abstract string CalculateResultForTestMethod(DirectionInfo directionInfo, string[] coordinateArray);
    }

    public class Controls : Helper
    {

        string allowableMoves = "LRM", allowableDirection = "EWNS";

        Directly directly;

        /// <summary>
        /// Gönderilen koordinat ve uzunluk parametrelerine göre verilerin düzgünlüğü kontrol ediliyor.
        /// </summary>
        /// <param name="coordinate">Koordinat bilgisi</param>
        /// <param name="lenght">gerekli olan uzunluk</param>
        /// <returns></returns>
        public override bool CoordinateValidControl(string[] coordinate, int lenght)
        {
            if (coordinate.Length != lenght)
            {
                return false;
            }

            if (lenght == 3 &&
               !String.IsNullOrEmpty(coordinate[2]) &&
               !allowableDirection.Contains(coordinate[2]))
            {
                return false;
            }

            return coordinate.All(x => !String.IsNullOrEmpty(x));
        }

        /// <summary>
        /// Hareketler L, R, M olmalı, farklı değer girilmiş mi kontrolü yapılacak.
        /// </summary>
        /// <param name="roverMoves">Gezici hareketleri</param>
        /// <returns></returns>
        public override bool MoveFormatControl(List<string> roverMoves)
        {
            return roverMoves.All(x => x.All(c => allowableMoves.Contains(c)));
        }

        /// <summary>
        /// String veriyi DirectionInfo tipine çevirir
        /// FormatException için engel konulmadı Unit test'de görmek için Örn: 1 A N gezici başlangıç formatı
        /// </summary>
        /// <param name="roverStartingLocations">Kullanıcıdan alınan gezici bilgileri</param>
        /// <returns></returns>
        public override List<DirectionInfo> ToDirectionInfoList(List<string> roverStartingLocations, List<string> roverMoves)
        {
            List<DirectionInfo> roverDirectionInfo = new List<DirectionInfo>();
            int index = 0;
            foreach (string item in roverStartingLocations)
            {
                string[] roverStartinglocationArray = item.Split(' ');//x, y ve Yön bilgileri ayrıldı.
                roverDirectionInfo.Add(new DirectionInfo()
                {
                    XCoordinate = int.Parse(roverStartinglocationArray[0]),
                    YCoordinate = int.Parse(roverStartinglocationArray[1]),
                    DirectMove = roverStartinglocationArray[2],
                    Moves = roverMoves[index++]
                });
            }

            return roverDirectionInfo;
        }

        /// <summary>
        /// Gezicinin rotası sonucu ulaştığı yeri hesaplıyoruz.
        /// </summary>
        /// <param name="directionInfo"></param>
        /// <param name="coordinateArray"></param>
        public override void CalculateResult(DirectionInfo directionInfo, string[] coordinateArray)
        {
            Enum.TryParse(directionInfo.DirectMove, out directly);
            directionInfo.Direct = directly;
            int direction = (int)directionInfo.Direct;

            foreach (char move in directionInfo.Moves)
            {
                #region Koordinat Hesaplama
                switch (move)
                {
                    case 'L':
                        direction -= 1;
                        break;
                    case 'R':
                        direction += 1;
                        break;
                    default:
                        if (IsInsidePlateau(directionInfo, coordinateArray))
                        {
                            if (directionInfo.Direct == Directly.W)
                            {
                                directionInfo.XCoordinate -= 1;
                            }
                            else if (directionInfo.Direct == Directly.E)
                            {
                                directionInfo.XCoordinate += 1;
                            }
                            else if (directionInfo.Direct == Directly.S)
                            {
                                directionInfo.YCoordinate -= 1;
                            }
                            else
                            {
                                directionInfo.YCoordinate += 1;
                            }
                        }
                        break;
                }
                #endregion Koordinat Hesaplama

                if (direction < 0)
                {
                    direction = 3;
                }
                else if (direction > 3)
                {
                    direction = 0;
                }

                directionInfo.Direct = (Directly)direction;
            }

            directionInfo.ResultLocation = $"{directionInfo.XCoordinate.ToString()} {directionInfo.YCoordinate.ToString()} {directionInfo.Direct}";

            Console.WriteLine(directionInfo.ResultLocation);
        }

        /// <summary>
        /// Gezicinin rotası sonucu ulaştığı yeri hesaplıyoruz. test metot için yazıldı sadece geriye sonucu dönüyor
        /// </summary>
        /// <param name="directionInfo"></param>
        /// <param name="coordinateArray"></param>
        public override string CalculateResultForTestMethod(DirectionInfo directionInfo, string[] coordinateArray)
        {
            Enum.TryParse(directionInfo.DirectMove, out directly);
            directionInfo.Direct = directly;
            int direction = (int)directionInfo.Direct;

            foreach (char move in directionInfo.Moves)
            {
                #region Koordinat Hesaplama
                switch (move)
                {
                    case 'L':
                        direction -= 1;
                        break;
                    case 'R':
                        direction += 1;
                        break;
                    default:
                        if (IsInsidePlateau(directionInfo, coordinateArray))
                        {
                            if (directionInfo.Direct == Directly.W)
                            {
                                directionInfo.XCoordinate -= 1;
                            }
                            else if (directionInfo.Direct == Directly.E)
                            {
                                directionInfo.XCoordinate += 1;
                            }
                            else if (directionInfo.Direct == Directly.S)
                            {
                                directionInfo.YCoordinate -= 1;
                            }
                            else
                            {
                                directionInfo.YCoordinate += 1;
                            }
                        }
                        break;
                }
                #endregion Koordinat Hesaplama

                if (direction < 0)
                {
                    direction = 3;
                }
                else if (direction > 3)
                {
                    direction = 0;
                }

                directionInfo.Direct = (Directly)direction;
            }

            return $"{directionInfo.XCoordinate.ToString()} {directionInfo.YCoordinate.ToString()} {directionInfo.Direct}";
           
        }

        /// <summary>
        /// Gezicinin hareketleri plato içerisinde olup olmadığını kontrol ediyoruz.
        /// </summary>
        /// <param name="directionInfo"></param>
        /// <param name="coordinateArray"></param>
        /// <returns></returns>
        private bool IsInsidePlateau(DirectionInfo directionInfo, string[] coordinateArray)
        {
            if (directionInfo.Direct == Directly.S)
            {
                return directionInfo.YCoordinate != 0;
            }
            else if (directionInfo.Direct == Directly.N)
            {
                return directionInfo.YCoordinate != int.Parse(coordinateArray[1]);
            }
            else if (directionInfo.Direct == Directly.W)
            {
                return directionInfo.XCoordinate != 0;
            }
            else
            {
                return directionInfo.XCoordinate != int.Parse(coordinateArray[0]);
            }
        }

    }
}
