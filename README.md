# tetris
bu wdf projemizde tetris oyununu yapıcaz
<br>
c# kodlarıyla birlikte oyunun temel dinamiklerini oluşturcaz
<br>
tetris oyununu açıklamak gerekirse, belirli bir çerçeve içinde yukardan tuşlarla yönetebildiğimiz bloklar gelir ve bu bloklarda zeminde boşluk bırakmadan doldurduğumuzda skor elde edebildiğimiz bir oyundur.
<br>
Kodların içindeki sınıflar,özelikleri,pencereler gibi bazı işlevlerin sözel özeti:


1. `board` Sınıfı:
   - `Rows`, `Cols`, `Score`, `LinesFilled`, `currTetramina` ve `BlockControls` gibi özel değişkenlere sahiptir.
   - `TetrisGrid` parametresi alarak bir oyun tahtası oluşturur.
   - `getScore()` ve `getLines()` metotları, puanı ve dolu satır sayısını döndürür.
   - `currTetraminaDraw()`, mevcut tetraminayı tahtaya çizer.
   - `currTetraminaErase()`, mevcut tetraminayı tahtadan siler.
   - `CheckRows()`, dolu satırları kontrol eder ve gerektiğinde siler.
   - `RemoveRow(int row)`, belirli bir satırı siler.
   - `CurrtetraminamoveLeft()`, mevcut tetraminayı sola hareket ettirir.
   - `CurrtetraminamoveRight()`, mevcut tetraminayı sağa hareket ettirir.
   - `CurrtetraminamoveDown()`, mevcut tetraminayı aşağı hareket ettirir ve dolu satırları kontrol eder.
   - `CurrtetraminamoveRotate()`, mevcut tetraminayı döndürür.

2. `Tetramina` Sınıfı:
   - `currPosition`, `currShape`, `currColor` ve `rotate` gibi özel değişkenlere sahiptir.
   - `getcurrColor()`, `getCurrPosition()` ve `getCurrShape()` metotları, sırasıyla mevcut rengi, pozisyonu ve şekli döndürür.
   - `movleft()`, `movright()`, `movdown()` ve `movrotate()`, tetraminayı sırasıyla sola, sağa, aşağıya hareket ettirir ve döndürür.
   - `SetRandomShape()`, rastgele bir şekil oluşturur ve onu döndürür.

3. `MainWindow` Sınıfı:
   - Tetris oyununun ana penceresini temsil eder.
   - `Timer` ve `myBoard` gibi özel değişkenlere sahiptir.
   - `GameStart()`, oyuna başlamak için tahtayı temizler ve yeni bir oyun tahtası oluşturur.
   - `GameTick()`, oyun süresi ilerledikçe çağrılan bir olay işleyicisidir. Tahtadaki skoru ve dolu satır sayısını günceller ve mevcut tetraminayı aşağı hareket ettirir.
   - `GamePause()`, oyunu duraklatır veya devam ettirir.
   - `HandleKeyDown()`, klavye tuşlarına basıldığında çağrılan bir olay işleyicisidir. Tuşlara göre tetraminayı hareket


projemizi wdf platformunda yapcaz visual stiduo da bulması biraz "wdf uygulaması",etiketinde ise "c#","windows","masaüstü" olacaktır.
doğru yerde projemizi oluşturduğumuzan emin olduktan sonra ,Mainwindow.Xaml penceresine geliyoruz burası işin tasarım kısmı tasarımı yapıcaz öncelikle,
xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"
xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"


<frameset>
 asdasdasdas
</frameset>

