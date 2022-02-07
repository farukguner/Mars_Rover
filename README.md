# Mars Rover

A squad of robotic rovers are to be landed by NASA on a plateau on Mars. 
This plateau, which is curiously rectangular, must be navigated by the rovers so that 
their on board cameras can get a complete view of the surrounding terrain to send back to Earth.

A rover's position and location is represented by a combination of x and y co-ordinates and a letter
representing one of the four cardinal compass points. The plateau is divided up into a grid to
simplify navigation. An example position might be 0, 0, N, which means the rover is in the bottom
left corner and facing North.

In order to control a rover, NASA sends a simple string of letters. The possible letters are 'L', 'R' and
'M'. 'L' and 'R' makes the rover spin 90 degrees left or right respectively, without moving from its
current spot. 'M' means move forward one grid point, and maintain the same heading.
Assume that the square directly North from (x, y) is (x, y+1).

Test Input:

5 5

1 2 N

LMLMLMLMM

3 3 E

MMRMMRMRRM

Expected Output:

1 3 N

5 1 E

# İçindekiler
- [Commit Standartı](#commit)
- [Teknolojiler](#Teknolojiler)
- [To-Do Task](#To-Do-Tasks)
- [Done Task](#Gerceklestirilenler)


#### Commit
`[Commit Id][commit message]` şeklindedir. 


#### Teknolojiler;
- C#(.Net Core)

#### To-Do Tasks




#### Gerceklestirilenler
- Proje Oluşturulması(C'in-1)
- Readme.md dosyasının güncellenmesi(C'in-1)
- Her C'in öncesi clean code refactoring yapılacak
- Plato grid bilgisi alınacak
- Plato grid bilgisi format kontrolü yapılacak
- Başlangıç konum bilgileri alınacak
- Başlangıç konum bilgisi format kontrolü yapılacak
- Yönlendirmeler alınacak
- Yönlendirme format kontrolü yapılacak
- Sonlandurma komutu boş tuş olarak algılanacak
- Yönlendirmeler Enum'a çevirilecek.
- Yönlendirmeler sayısal olarak işlenecek
- Helper sınıfı eklenerek ortak işlemler ve kod okunurluğunu arttırmak için metodlar oraya taşındı.
- UnitTest eklenecek.

