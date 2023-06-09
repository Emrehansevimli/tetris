# tetris
bu wdf projemizde tetris oyununu yapıcaz
<br>
c# kodlarıyla birlikte oyunun temel dinamiklerini oluşturcaz
<br>
tetris oyununu açıklamak gerekirse, belirli bir çerçeve içinde yukardan tuşlarla yönetebildiğimiz bloklar gelir ve bu bloklarda zeminde boşluk bırakmadan doldurduğumuzda skor elde edebildiğimiz bir oyundur.
<br>
Kodların içindeki sınıflar,özelikleri,pencereler gibi bazı işlevlerin sözel özeti:

#ÖZET
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

#tasarım bölümü
projemizi wdf platformunda yapcaz visual stiduo da bulması biraz "wdf uygulaması",etiketinde ise `"c#","windows","masaüstü"` olacaktır.
doğru yerde projemizi oluşturduğumuzan emin olduktan sonra ,Mainwindow.Xaml penceresine geliyoruz burası işin tasarım kısmı tasarımı yapıcaz öncelikle,
`xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"`
`xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"`
yukarda verdiğim iki kaynağı ekliyoruz,Title satırında pencernin ismini istediğinizi verebilirsiniz pencerenin üstünde gözükcek ve yükselik ve genişliği ayarlıyoruz.
570 e 700 olarak ayarladım bende.
`KeyDown ="HandleKeyDown"`yazıyoruz bu kod klavyeden tuş basıldığında algılanmasına istenilen fonksiyonun çağrılmasını sağlıycak.
`'Initialized = "MainWindow_Initilized" Background ="DimGray">' `kod satırında
MainWindow_Initialized adlı bir metot, Initialized olayını işleyen bir metottur. Bu metot, pencerenin başarıyla başlatılması durumunda otomatik olarak çağrılır ve gerekli işlemleri gerçekleştirebiliriz.yanlız bu yazdıklarımız window içince olamlı '<window .....>'bu şekilde yani .
şimdi `<DockPanel LastChildFill="True"></DockPanel>`satrını oluşturalım dockpanel içine yerleştirilen elemanların yatay ve dikey hizlanmasını sağlar lastchildfill ise
son yerleşen elemanın boşluğun tamamnı doldurcak kadar büyütür.

'<StackPanel DockPanel.Dock="Right" Width="150"></StackPanel>'bu kodda  stackpanel elemanları dikey olarak yerleştirir ve sağ sabitler genişliği 150 olarak ayarladık.
stack panelin içinede '<Label Content="Label" Height="56" x:Name="Scores" FontSize="28" FontWeight="Bold"  />' Scores ve Lines adında sadece name farklı olcak şekilde 2 satır oluşturalım yazı oluşturduk kısaca.
'<Grid Name="MainGrid" Height="500" Width="250"></Grid>' grid ızgara demek kolon ve satır oluşturmak tablo yapmak için kullanılır.
'<Grid.RowDefinitions></Grid.RowDefinitions>' satır oluşturmak için bu kalıbı kullanıyoruz içine '<RowDefinition/>'tan 22 tane ekliyoruz 22 tane satır oluşucak.
'<Grid.ColumnDefinitions></Grid.ColumnDefinitions>' aynı şekilde kolon oluşturmak içinde aynı şeyler geçerli '<ColumnDefinition/>'da 10 kolon ekliyoruz tetris bloklarının gelceği kareli düzlem hazır.Artık tasarım kısmı bitti Mainwindow.xaml.cs penceresine gecebiliriz.

