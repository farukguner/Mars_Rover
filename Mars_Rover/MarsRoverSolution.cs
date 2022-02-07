using System;
using System.Collections.Generic;
using System.Linq;

namespace Mars_Rover
{
    class MarsRoverSolution
    {
        static void Main(string[] args)
        {
            #region Properties
            Controls CommonControl;
            string plateauGrid, roverStartingLocation, roverMove;
            string[] coordinateArray, roverStartinglocationArray;
            List<DirectionInfo> roverDirectionInfo;
            List<string> roverMoves, roverStartingLocations;
            #endregion

            #region Initialize
            CommonControl = new Controls();
            roverDirectionInfo = new List<DirectionInfo>();
            roverMoves = new List<string>();
            roverStartingLocations = new List<string>();
            #endregion Initialize

            //Plato bölme sayısı alınıyor. Örn. 5 5
            plateauGrid = Console.ReadLine();
            coordinateArray = plateauGrid.Split(' ');//girilen değer x ve y olarak işlem yapmak için split ediliyor.

            //Koordinat format ve içerik kontrolü yapılıyor.
            if (!CommonControl.CoordinateValidControl(coordinateArray, 2))
            {
                Console.WriteLine("Plato hatalı format");
                return;
            }

            //Gezicinin başlangıç koordinatı alınıyor. Örn.: 1 2 N
            roverStartingLocation = Console.ReadLine();

            //çıkış komutu olarak veri girmeden enter'e basılması bekleniyor.
            while (roverStartingLocation != "")
            {
                roverStartinglocationArray = roverStartingLocation.Split(' ');
                if (!CommonControl.CoordinateValidControl(roverStartinglocationArray, 3))
                {
                    Console.WriteLine("Başlangıç konum format hatası");
                    return;
                }
                roverStartingLocations.Add(roverStartingLocation);
                roverMove = Console.ReadLine();//Gezicinin hareketleri alınıyor.
                roverMoves.Add(roverMove);
                roverStartingLocation = Console.ReadLine();
            }//Başlangıç konumları ve yönlendirmeler alındı

            //Eğer hiç veri girilmemişse hata mesajı gösterilir.
            if (roverStartingLocations.Count() == 0)
            {
                Console.WriteLine("Gezici bulunamadı.");
                return;
            }

            //Farklı formatta yön girilmiş mi kontrolü
            if (!CommonControl.MoveFormatControl(roverMoves))
            {
                Console.WriteLine("Yönlendirme format hatası");
                return;
            }

            //String halinde olan veriler işlem yapabilmek için DirectionInfo listesine çevirildi.
            roverDirectionInfo = CommonControl.ToDirectionInfoList(roverStartingLocations, roverMoves);

            //Sırayla tüm gezici bilgileri yollanıp sonuç izlenir
            foreach (DirectionInfo direction in roverDirectionInfo)
            {
                CommonControl.CalculateResult(direction, coordinateArray);
            }

        }
    }
}
